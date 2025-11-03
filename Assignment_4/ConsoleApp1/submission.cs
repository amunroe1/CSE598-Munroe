using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Xml;

namespace ConsoleApp1
{
    public class Program
    {
        // These URLs will be read by the autograder,
        // please keep the variable name un-changed and link to the correct xml/xsdfiles.
        public static string xmlURL = "https://raw.githubusercontent.com/amunroe1/CSE598-Munroe/refs/heads/main/Assignment_4/Hotels.xml"; //Q1.2
        public static string xmlErrorURL = "https://raw.githubusercontent.com/amunroe1/CSE598-Munroe/refs/heads/main/Assignment_4/HotelsErrors.xml"; //Q1.3
        public static string xsdURL = "https://raw.githubusercontent.com/amunroe1/CSE598-Munroe/refs/heads/main/Assignment_4/Hotels.xsd"; //Q1.1
        public static void Main(string[] args)
        {
            // Q3: You can pick two of three
            string result = Verification(xmlURL, xsdURL);
            Console.WriteLine(result);



            result = Xml2Json(xmlURL);
            Console.WriteLine(result);

            result = Verification(xmlErrorURL, xsdURL);
            Console.WriteLine(result);
        }

        // Q2.1
        public static string Verification(string xmlUrl, string xsdUrl)
        {
            //return "No Error" if XML is valid. Otherwise, return the desired exception message.
            List<string> validationErrors = new List<string>();
            try
            {
                // Load the XML document
                XmlReaderSettings settings = new XmlReaderSettings
                {
                    ValidationType = ValidationType.Schema
                };
                settings.Schemas.Add(null, xsdUrl);

                settings.ValidationEventHandler += (sender, args) =>
                {
                    validationErrors.Add($"{args.Message} Line {args.Exception.LineNumber}, position {args.Exception.LinePosition}.");
                };

                try
                {
                    using (XmlReader reader = XmlReader.Create(xmlUrl, settings))
                    {
                        while (reader.Read()) { }
                    }
            
                }
                catch (XmlException ex)
                {
                    validationErrors.Add(ex.Message); ;
                }

 
                if (validationErrors.Count > 0)
                {
                    return "Validation Errors:\n" + string.Join("\n", validationErrors); ;
                }
                else
                {
                    return "No Error";
                }
            }
            catch (Exception ex)
            {
                return $"Unexpected error: {ex.Message}";
            }

        }

        // Q2.2
        public static string Xml2Json(string xmlUrl)
        {
            // The returned jsonText needs to be de-serializable by Newtonsoft.Json package. (JsonConvert.DeserializeXmlNode(jsonText))
            try
            {
                string xmlContent;
                using (WebClient client = new WebClient())
                {
                    xmlContent = client.DownloadString(xmlUrl);
                }
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlContent);
                string jsonText = JsonConvert.SerializeXmlNode(doc);
                try
                {                     // Test deserialization}

                    JsonConvert.DeserializeXmlNode(jsonText);
                    Console.WriteLine("Deserialization successful.");
                }
                catch (Exception ex)
                {
                    return $"Deserialization Error: {ex.Message}";
                }
                    return jsonText;
            }
            catch (Exception ex)
            {
                return $"Xml2Json Error: {ex.Message}";
            }
        }
    }
}

