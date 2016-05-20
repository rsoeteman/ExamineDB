using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Xml.Linq;
using Examine;
using Examine.LuceneEngine;
using Examine.LuceneEngine.Providers;
using ExamineDB.Config;
using ExamineDB.TypeExtensions;
using PetaPoco;

namespace ExamineDB.Indexers
{
    public class DBIndexer : LuceneIndexer
    {
        public DBIndexer()
        {
            OptimizationCommitThreshold = 100;
            AutomaticallyOptimize = false;
        }
        public override void Initialize(string name, NameValueCollection config)
        {
            Config = new DBIndexerConfig(name, config);
            base.Initialize(name, config);
        }

        /// <summary>
        /// Rebuilds the entire index from scratch for all index types
        /// </summary>
        /// <remarks>This will completely delete the index and recreate it</remarks>
        public override void RebuildIndex()
        {

            EnsureIndex(true);

            //call abstract method
            PerformIndexAll(Config.NodeType);
        }


        protected override void PerformIndexAll(string type)
        {
            BuildIndex();
        }

        internal void IndexSingleNode(string nodeId)
        {
            //Make sure the record is deleted first
            DeleteFromIndex(nodeId);
   
            using (var db = new Database(Config.ConnectionStringName))
            {
                db.OpenSharedConnection();
                try
                {
                    using (var cmd = db.CreateCommand(db.Connection, Config.SingleRecordSQL,nodeId))
                    {
                        using (IDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                AddSingleNodeToIndex(reader.SerializeToXml(Config).Root,Config.NodeType);
                            }
                        }
                    }
                }
                catch (Exception x)
                {

                }
                finally
                {
                    db.CloseSharedConnection();
                }
            }

        }

        private void BuildIndex()
        {
            var nodes = new List<XElement>();
            
            using (var db = new Database(Config.ConnectionStringName))
            {
                db.OpenSharedConnection();
                try
                {
                    using (var cmd = db.CreateCommand(db.Connection, Config.SQL))
                    {
                        using (IDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                nodes.Add(reader.SerializeToXml(Config).Root);
                               
                            }
                        }
                    }
                    AddNodesToIndex(nodes, Config.NodeType);
                }
                catch (Exception x)
                {
                }
                finally
                {
                    db.CloseSharedConnection();
                }
            }
        }

        protected override Dictionary<string, string> GetDataToIndex(XElement node, string type)
        {
            var values = new Dictionary<string, string>();
            var nodeId = int.Parse(node.Attribute("id").Value);

            foreach (var field in node.Attributes())
            {
                string val = node.SelectExaminePropertyValue(field.Name.LocalName);
                var args = new IndexingFieldDataEventArgs(node, field.Name.LocalName, val, true, nodeId);
                OnGatheringFieldData(args);
                val = args.FieldValue;

                //don't add if the value is empty/null                
                if (!string.IsNullOrEmpty(val))
                {
                    if (values.ContainsKey(field.Name.LocalName))
                    {
                        OnDuplicateFieldWarning(-1, IndexSetName, field.Name.LocalName);
                    }
                    else
                    {
                        values.Add(field.Name.LocalName, val);
                    }
                }

            }

            //raise the event and assign the value to the returned data from the event
            var indexingNodeDataArgs = new IndexingNodeDataEventArgs(node, nodeId, values, type);
            OnGatheringNodeData(indexingNodeDataArgs);
            values = indexingNodeDataArgs.Fields;

            return values;
        }

        protected override void PerformIndexRebuild()
        {
            BuildIndex();
        }


        public  DBIndexerConfig Config { get; set; }

    }
}
