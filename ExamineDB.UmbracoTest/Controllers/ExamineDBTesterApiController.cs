using System.Web.Http;
using Examine;
using ExamineDB.Helpers;
using ExamineDB.Indexers;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;

namespace ExamineDB.UmbracoTest.Controllers
{

    [PluginController("ExamineDB")]
    public class ExamineDBTesterApiController : UmbracoAuthorizedApiController
    {
        [HttpGet]
        public void ReIndexAll()
        {
            IndexHelper.RebuildIndex(GetFirstDBIndexer());
        }

        [HttpGet]
        public void ReIndex(string nodeId)
        {

            IndexHelper.IndexSingleNode(nodeId, GetFirstDBIndexer());
        }

        [HttpGet]
        public void Delete(string nodeId)
        {
            IndexHelper.DeleteFromIndex(nodeId, GetFirstDBIndexer());
        }

        private string GetFirstDBIndexer()
        {
            foreach (var indexer in ExamineManager.Instance.IndexProviderCollection)
            {
                var dbindexer = indexer as DBIndexer;
                if (dbindexer != null)
                {
                    return dbindexer.Name;
                }
            }

            return null;
        }
    }
}
