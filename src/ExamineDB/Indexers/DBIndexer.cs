using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Examine.Providers;

namespace ExamineDB.Indexers
{
    public class DBIndexer : BaseIndexProvider
    {

        public override void ReIndexNode(XElement node, string type)
        {
            throw new NotImplementedException();
        }

        public override void DeleteFromIndex(string nodeId)
        {
            throw new NotImplementedException();
        }

        public override void IndexAll(string type)
        {
            throw new NotImplementedException();
        }

        public override void RebuildIndex()
        {
            throw new NotImplementedException();
        }

        public override bool IndexExists()
        {
            throw new NotImplementedException();
        }

      
    }
}
