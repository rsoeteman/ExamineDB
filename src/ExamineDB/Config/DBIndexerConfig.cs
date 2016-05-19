using System.Collections.Specialized;
using System.Configuration;

namespace ExamineDB.Config
{
    /// <summary>
    /// Configuration for indexing
    /// </summary>
    public class DBIndexerConfig
    {
        internal DBIndexerConfig(string name, NameValueCollection config)
        {
            ConfigName = name;
            NodeType = config["nodeType"];
            ConnectionStringName = config["connectionStringName"];
            SQL = config["sql"];
            SingleRecordSQL = config["singleRecordSQL"];
            PrimaryKeyField = config["primaryKeyField"];
        }

        /// <summary>
        /// Gets the name of the configuration.
        /// </summary>
        public string ConfigName { get; private set; }

        /// <summary>
        /// The nodetype that gets stored in examine so you can query on a type
        /// </summary>
        public string NodeType { get; private set; }

        /// <summary>
        /// Gets the name of the primary key field
        /// </summary>
        public string PrimaryKeyField { get; private set; }

        /// <summary>
        /// Gets the connection string for the database.
        /// </summary>
        public string ConnectionStringName { get; private set; }

        /// <summary>
        /// Gets the SQL to query for all data 
        /// </summary>
        public string SQL { get; private set; }
        
        /// <summary>
        /// Gets the SQL statement for a single record 
        /// Make sure to use @0 parameter for the primary key.
        /// </summary>
        public string SingleRecordSQL { get; private set; }
    }
}
