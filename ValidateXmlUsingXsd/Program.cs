using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Xml;
using System.Xml.Schema;

namespace ValidateXmlUsingXsd
{
    class Program
    {
        static void Main(string[] args)
        {
            //Get Xml            
            XmlDocument xmlReport = new XmlDocument();
            xmlReport = GetXmlDoc(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\test_xml.xml");

            //Get Xsd
            XmlDocument xDoc = new XmlDocument();
            xDoc = GetXmlDoc(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\test_xsd.xsd");

            //Load the XmlSchemaSet.
            XmlSchemaSet schemaSet = new XmlSchemaSet();

            schemaSet.Add("", XmlReader.Create(new StringReader(xDoc.InnerXml)));

            ValidateXml objVal = new ValidateXml();
            objVal.Validate(xmlReport, schemaSet);

            if (String.IsNullOrEmpty(objVal.Errors))
                Console.WriteLine("Xml Validated.");
            else
                Console.WriteLine(objVal.Errors);
            Console.ReadLine();
        }

        public static XmlDocument GetXmlDoc(string path)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path);
            return xDoc;
        }

    }
}
