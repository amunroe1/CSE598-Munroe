using System;
using System.Xml.Schema;
using System.Xml;
using Newtonsoft.Json;
using System.IO;

namespace ConsoleApp1
{
    public class submission
    {
// These URLs will be read by the autograder,
// please keep the variable name un-changed and link to the correct xml/xsdfiles.
        public static string xmlURL = "https://raw.githubusercontent.com/amunroe1/CSE598-Munroe/refs/heads/main/Assignment_4/Hotels.xml"; //Q1.2
        public static string xmlErrorURL = "https://raw.githubusercontent.com/amunroe1/CSE598-Munroe/refs/heads/main/Assignment_4/HotelsErrors.xml"; //Q1.3
        public static string xsdURL = "https://raw.githubusercontent.com/amunroe1/CSE598-Munroe/refs/heads/main/Assignment_4/Hotels.xsd"; //Q1.1
        static void Main(string[] args)
        {
            // Q3: You can pick two of three
            string result = Verification(xmlURL, xsdURL);
            Console.WriteLine(result);

            result = Verification(xmlErrorURL, xsdURL);
            Console.WriteLine(result);

            result = Xml2Json("Hotels.xml");
            Console.WriteLine(result);
        }

        // Q2.1
        public static string Verification(string xmlUrl, string xsdUrl)
        {
            //return "No Error" if XML is valid. Otherwise, return the desired exception message.
            try
            {
                // Load the XML document
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.ValidationType = ValidationType.Schema;
                settings.Schemas.Add(null, xsdUrl);
                settings.ValidationEventHandler += (sender, args) =>
                {
                    throw new XmlSchemaValidationException(args.Message);
                };

                using (XmlReader reader = XmlReader.Create(xmlUrl, settings))
                {
                    while (reader.Read()) { }
                }

                return "No Error";
            }
            catch (XmlSchemaValidationException ex)
            {
                return $"Validation Error: {ex.Message}";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        // Q2.2
        public static string Xml2Json(string xmlUrl)
        {
            // The returned jsonText needs to be de-serializable by Newtonsoft.Json package. (JsonConvert.DeserializeXmlNode(jsonText))
            try
            {
                string xmlContent;
                using (var client = new System.Net.WebClient())
                {
                    xmlContent = client.DownloadString(xmlUrl);
                }

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlContent);
                string jsonText = JsonConvert.SerializeXmlNode(doc);
                return jsonText;
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
    }
}
