using System;
using System.Runtime.Remoting.Contexts;
using System.Xml;
using System.Xml.Schema;

namespace XmlApplication
{
    public class Program
    {
// These URLs will be read by the autograder,
// please keep the variable name un-changed and link to the correct xml/xsdfiles.
        public static string xmlURL = "https://github.com/amunroe1/CSE598-Munroe/blob/main/Assignment_4/Hotels.xml"; //Q1.2
        public static string xmlErrorURL = "https://github.com/amunroe1/CSE598-Munroe/blob/main/Assignment_4/HotelsErrors.xml"; //Q1.3
        public static string xsdURL = "https://github.com/amunroe1/CSE598-Munroe/blob/main/Assignment_4/Hotels.xsd"; //Q1.1
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
            return "No Error";
        }

        // Q2.2
        public static string Xml2Json(string xmlUrl)
        {
            // The returned jsonText needs to be de-serializable by Newtonsoft.Json package. (JsonConvert.DeserializeXmlNode(jsonText))
            return jsonText;
        }
    }
}
