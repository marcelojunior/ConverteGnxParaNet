using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GxToNet
{
    public class XPZ
    {
        public string Caminho { get; set; }
        public XmlDocument XML { get; set; }

        public XPZ(string caminho)
        {
            this.Caminho = caminho;
            XML = new XmlDocument();
            this.CarregarXML();
        }

        public void CarregarXML()
        {
            XML.Load(this.Caminho);
        }
    }
}
