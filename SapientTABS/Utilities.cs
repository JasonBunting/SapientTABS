using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

using UnityEngine;

namespace SapientTABS
{
    public class Utilities
    {
        public static string GetInnerText(string xml, string xpath)
        {
            return GetNamespacelessXmlDocument(xml).SelectSingleNode(xpath)?.InnerText ?? string.Empty;
        }

        public static string[] GetInnerTexts(string xml, string xpath)
        {
            List<string> matchingNodesText = new List<string>();
            var matchingNodes = GetNamespacelessXmlDocument(xml).SelectNodes(xpath);
            foreach (XmlNode matchingNode in matchingNodes)
            {
                matchingNodesText.Add(matchingNode.InnerText);
            }
            return matchingNodesText.ToArray();
        }

        public static XmlDocument GetNamespacelessXmlDocument(string xml)
        {
            // strip namespaces out so we can use 'cleaner' XPath, then load the xml and get the text of the node
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(StripNameSpaces(XElement.Parse(xml)).ToString());
            return xmlDoc;
        }

        /// <summary>
        /// Found this at https://stackoverflow.com/questions/987135/
        /// </summary>
        public static XElement StripNameSpaces(XElement root)
        {
            XElement res = new XElement(
                root.Name.LocalName,
                root.HasElements ?
                    root.Elements().Select(el => StripNameSpaces(el)) :
                    (object)root.Value
            );
            res.ReplaceAttributes(
                root.Attributes().Where(attr => (!attr.IsNamespaceDeclaration)));
            return res;
        }


        public static void Log(string data)
        {
            Log(data, "SapientTABS.PinaCollada.log");
        }
        public static void LogError(string data)
        {
            Log(data, "SapientTABS.PinaCollada.ERROR.log");
        }
        public static void Log(string data, string fileName)
        {
            System.IO.File.AppendAllText(System.IO.Path.Combine(Application.dataPath, fileName), string.Format("================================\n{0}\n================================\n{1}\n", DateTime.Now.ToString(), data));
        }
    }
}
