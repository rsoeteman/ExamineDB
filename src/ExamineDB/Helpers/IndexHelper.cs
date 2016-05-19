using System;
using Examine;
using ExamineDB.Indexers;

namespace ExamineDB.Helpers
{
    /// <summary>
    /// Simple helper to work with indexing the dat
    /// </summary>
    public static class IndexHelper
    {
        /// <summary>
        /// Rebuilds the complete index.
        /// </summary>
        /// <param name="indexerName">Name of the indexer.</param>
        /// <exception cref="System.ArgumentException"></exception>
        public static void RebuildIndex(string indexerName)
        {
            var indexer = ExamineManager.Instance.IndexProviderCollection[indexerName] as DBIndexer;

            if (indexer == null)
            {
                throw new ArgumentException(string.Format("Indexer {0} is not a valid DBIndexer", indexerName));
            }

            indexer.RebuildIndex();
        }

        /// <summary>
        /// (re)Indexes a single node.
        /// </summary>
        public static void IndexSingleNode(string nodeId, string indexerName)
        {
            var indexer = ExamineManager.Instance.IndexProviderCollection[indexerName] as DBIndexer;
            
            if (indexer == null)
            {
                throw new ArgumentException(string.Format("Indexer {0} is not a valid DBIndexer", indexerName));
            }
            
            indexer.IndexSingleNode(nodeId);
        }

        /// <summary>
        /// Deletes a node from index.
        /// </summary>
        public static void DeleteFromIndex(string nodeId, string indexerName)
        {
            var indexer = ExamineManager.Instance.IndexProviderCollection[indexerName] as DBIndexer;
            
            if (indexer == null)
            {
                throw new ArgumentException(string.Format("Indexer {0} is not a valid DBIndexer", indexerName));
            }

            indexer.DeleteFromIndex(nodeId);
        }

        
    }
}
