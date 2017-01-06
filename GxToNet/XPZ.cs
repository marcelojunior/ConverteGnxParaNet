using GxToNet.DAL;
using GxToNet.ObjetosGenexus;
using GxToNet.Servicos;
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
        public XmlNode documento { get; set; }
        public List<Atributo> Atributos { get; set; }
        public List<Tabela> Tabelas { get; set; }
        public Transacao Transacao { get; set; }

        private CarregarObjetosGenexus sGenexus;

        public XPZ(string caminho, Conexao conexao)
        {
            this.Caminho = caminho;
            XML = new XmlDocument();
            this.CarregarXML();

            this.sGenexus = new CarregarObjetosGenexus(this, conexao);

            this.Atributos = sGenexus.CarregarAtributos();
            this.Transacao = sGenexus.CarregarTransacao();
            this.Tabelas = sGenexus.CarregarTabelas();
        }

        public void CarregarXML()
        {
            XML.Load(this.Caminho);
            documento = XML.DocumentElement;
        }
    }
}
