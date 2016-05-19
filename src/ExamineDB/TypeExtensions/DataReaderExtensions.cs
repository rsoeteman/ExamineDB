using System.Data;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using ExamineDB.Config;

namespace ExamineDB.TypeExtensions
{
    /// <summary>
    /// Extension for datareader
    /// </summary>
    internal static class DataReaderExtensions
    {
        /// <summary>
        /// Extension to serialize the reader values to xml
        /// </summary>
        public static XDocument SerializeToXml(this IDataReader reader, DBIndexerConfig config)
        {
            string xml;
            using (var sw = new StringWriter())
            {
                using (var writer = new XmlTextWriter(sw))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement(config.NodeType);

                    //LuceneIndexer requires an id field
                    var idFieldExists =  false;

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        if (reader.GetName(i).Equals("id"))
                        {
                            idFieldExists = true;
                        }
                    }

                    writer.WriteAttributeString("id",  idFieldExists ? reader["id"].ToString() : reader[config.PrimaryKeyField].ToString());

                    //add all fields from the reader
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        writer.WriteAttributeString(reader.GetName(i), reader.GetValue(i).ToString());
                    }

                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                    xml = sw.ToString();
                }
            }

            return XDocument.Parse(xml);
        }
    }
}
