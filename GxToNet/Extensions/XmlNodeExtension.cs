using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GxToNet.Extensions
{
    public static class XmlNodeExtension
    {
        public static int GetInt(this XmlNode _node, string xpath)
        {
            var node = _node.SelectSingleNode(xpath);
            var valor = 0;
            if (node != null)
                valor = Int32.Parse(node.InnerText);

            return valor;
        }

        public static string GetString(this XmlNode _node, string xpath)
        {
            return _node.SelectSingleNode(xpath).InnerText;
        }
    }
}
