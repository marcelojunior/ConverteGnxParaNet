using GxToNet.ObjetosGenexus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using GxToNet.Extensions;

namespace GxToNet.Servicos
{
    public class CarregarObjetosGenexus
    {
        public XPZ xpz { get; set; }
        private XmlNamespaceManager nsmgr;

        public CarregarObjetosGenexus(XPZ _xpz)
        {
            this.xpz = _xpz;
            nsmgr = new XmlNamespaceManager(xpz.XML.NameTable);
        }

        public List<Atributo> CarregaAtributos()
        {
            var atributos = new List<Atributo>();

            var xmlAtributos = xpz.documento.SelectNodes("/ExportFile/Attributes/GXAtt", nsmgr);
            foreach (XmlNode item in xmlAtributos)
            {
                var atributo = new Atributo();
                atributo.Id = item.ParseInt("Attribute/Id");
                atributo.Nome = item.SelectSingleNode("Attribute/Name").InnerText;
                atributo.Titulo = item.SelectSingleNode("Attribute/Title").InnerText;
                atributo.Tipo = _selecionaTipoAtributo(item);
                atributo.Tamanho = item.ParseInt("Attribute/Length");

                atributos.Add(atributo);
            }

            return atributos;
        }
        
        private TipoAtributo _selecionaTipoAtributo(XmlNode item)
        {
            var tipo = TipoAtributo.Texto;
            var node = item.SelectSingleNode("Attribute/Type");
            if (node != null)
            {
                if (node.InnerText == "Character")
                    tipo = TipoAtributo.Texto;
                else if (node.InnerText == "Date")
                    tipo = TipoAtributo.Data;
            }
            

            return tipo;
        }

        internal string CarregaNameSpace()
        {
            var name = xpz.documento.SelectSingleNode("/ExportFile/GXObject/Folder/Info/Name").InnerText;
            var folder = xpz.documento.SelectSingleNode("/ExportFile/GXObject/Folder/Info/Folder").InnerText;
            return string.Format("{0}.{1}", folder, name);
        }

        internal string CarregaNomeTransacao()
        {
            var nomeTransacao = "";
            var node = xpz.documento.SelectSingleNode("/ExportFile/GXObject/Transaction");

            if (node != null)
            {
                nomeTransacao = node.SelectSingleNode("Info/Name").InnerText;
            }

            return nomeTransacao;
        }

        internal List<Tabela> CarregaTabelas()
        {
            var tabelas = new List<Tabela>();
            var xmlTabelas = xpz.documento.SelectNodes("/ExportFile/GXObject/Table", nsmgr);
            foreach (XmlNode item in xmlTabelas)
            {
                var tabela = new Tabela();
                tabela.Id = item.ParseInt("Id");
                tabela.Nome = item.Texto("Info/Name");
                tabela.Descricao = item.Texto("Info/Description");
                tabela.Chaves = _CarregaChavesTabela(item);

                tabelas.Add(tabela);
            }
            return tabelas;
        }

        private List<TabelaChave> _CarregaChavesTabela(XmlNode node)
        {
            var chaves = new List<TabelaChave>();
            var xmlChaves = node.SelectNodes("Key", nsmgr);
            foreach (XmlNode item in xmlChaves)
            {
                var chave = new TabelaChave();
                chave.Nome = item.InnerText;

                chaves.Add(chave);
            }

            return chaves;
        }
    }
}
