using System.Web.Http;
using ExamineDB.Helpers;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;

namespace ExamineDB.UmbracoTest.Controllers
{

    [PluginController("ExamineDB")]
    public class ExamineDBTesterApiController : UmbracoAuthorizedApiController
    {
        public void ReIndex()
        {
            IndexHelper.RebuildIndex("ExamineDBIndexer");
        }

        [HttpGet]
        public void ReIndex(string nodeId)
        {

            IndexHelper.IndexSingleNode(nodeId, "ExamineDBIndexer");
        }

        [HttpGet]
        public void Delete(string nodeId)
        {
            IndexHelper.DeleteFromIndex(nodeId, "ExamineDBIndexer");
        }

    }
}
