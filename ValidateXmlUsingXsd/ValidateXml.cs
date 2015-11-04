using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;
using System.Xml.Schema;
using System.IO;

namespace ValidateXmlUsingXsd
{
    class ValidateXml
    {
        // Validation Error Message
        public StringBuilder ErrorLog = new StringBuilder();
        public string Errors { get { return ErrorLog.ToString(); } }
        
        public void Validate(XmlDocument xmlReport, XmlSchemaSet schemaSet)
        {
            XmlSchema compiledSchema = null;

            foreach (XmlSchema schema in schemaSet.Schemas())
            {
                compiledSchema = schema;
            }

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ValidationFlags = XmlSchemaValidationFlags.ProcessIdentityConstraints | XmlSchemaValidationFlags.ReportValidationWarnings;
            settings.Schemas.Add(compiledSchema);
            settings.ValidationEventHandler += new ValidationEventHandler(ValidationCallBack);
            settings.ValidationType = ValidationType.Schema;

            //Create the schema validating reader.
            XmlReader vreader = XmlReader.Create(new StringReader(xmlReport.InnerXml), settings);

            while (vreader.Read()) { }

            //Close the reader.
            vreader.Close();
        }

        //Display any warnings or errors.
        private void ValidationCallBack(object sender, ValidationEventArgs args)
        {
            if (args.Severity == XmlSeverityType.Warning)
            {
                ErrorLog.AppendLine("Warning: Matching schema not found.  No validation occurred.");
                ErrorLog.AppendLine(args.Message);
            }
            else
            {
                ErrorLog.AppendLine("Validation error: ");
                ErrorLog.AppendLine(args.Message);
            }
        }
    }
}
