using GxToNet.ObjetosGenexus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using GxToNet.Extensions;
using GxToNet.DAL;

namespace GxToNet.Servicos
{
    public class CarregarObjetosGenexus
    {
        public XPZ xpz { get; set; }
        private XmlNamespaceManager nsmgr;

        public Conexao conexao;

        public CarregarObjetosGenexus(XPZ _xpz, Conexao _conexao)
        {
            this.xpz = _xpz;
            this.nsmgr = new XmlNamespaceManager(xpz.XML.NameTable);
            this.conexao = _conexao;
        }

        public List<Atributo> CarregarAtributos()
        {
            var atributos = new List<Atributo>();

            var xmlAtributos = xpz.documento.SelectNodes("/ExportFile/Attributes/GXAtt", nsmgr);
            foreach (XmlNode item in xmlAtributos)
            {
                var atributo = new Atributo();
                atributo.Id = item.GetInt("Attribute/Id");
                atributo.Nome = item.GetString("Attribute/Name");
                atributo.Titulo = item.GetString("Attribute/Title");
                atributo.Tipo = _SelecionarTipoAtributo(item);
                atributo.Tamanho = item.GetInt("Attribute/Length");

                atributos.Add(atributo);
            }

            return atributos;
        }
        
        private TipoAtributo _SelecionarTipoAtributo(XmlNode item)
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

        internal Transacao CarregarTransacao()
        {
            var transacao = new Transacao();
            var xmlTransacoes = xpz.documento.SelectNodes("/ExportFile/GXObject/Transaction", nsmgr);

            if (xmlTransacoes != null)
            {
                foreach (XmlNode item in xmlTransacoes)
                {
                    // A transação principal não contém a tag StyleClass
                    // TODO: Precisa encontrar outra forma de verificar o nome da transação
                    var styleClass = item.SelectSingleNode("Info/StyleClass");
                    if (styleClass == null)
                    {
                        transacao.Nome = item.GetString("Info/Name");
                        transacao.Descricao = item.GetString("Info/Description");
                        transacao.Pasta = item.GetString("Info/Folder");
                    }
                }
            }

            return transacao;
        }

        internal List<Tabela> CarregarTabelas()
        {
            var tabelas = new List<Tabela>();
            var xmlTabelas = xpz.documento.SelectNodes("/ExportFile/GXObject/Table", nsmgr);
            foreach (XmlNode item in xmlTabelas)
            {
                var tabela = new Tabela();
                tabela.Id = item.GetInt("Id");
                tabela.Nome = item.GetString("Info/Name");
                tabela.Descricao = item.GetString("Info/Description");
                tabela.Chaves = _CarregarChavesTabela(item);
                tabela.Colunas = _CarregarColunasTabela(item, tabela);

                tabelas.Add(tabela);
            }
            return tabelas;
        }

        private List<TabelaColuna> _CarregarColunasTabela(XmlNode node, Tabela tabela)
        {
            var colunas = new List<TabelaColuna>();
            var nomesColunas = conexao.ListarNomeDeColunas(tabela.Nome);
            foreach (var nome in nomesColunas)
            {
                var coluna = new TabelaColuna();
                coluna.Nome = nome;
                coluna.Atributo = _BuscarAtributo(nome);
                coluna.Atributo.Tipo = conexao.VerificarTipoColuna(tabela.Nome, coluna.Nome);
                coluna.PermiteNulo = conexao.VerificarColunaPermiteNulo(tabela.Nome, coluna.Nome);

                colunas.Add(coluna);
            }

            return colunas;
        }

        private List<TabelaChave> _CarregarChavesTabela(XmlNode node)
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

        private Atributo _BuscarAtributo(string nome)
        {
            var atributo = new Atributo();
            foreach (var item in xpz.Atributos)
            {
                if (item.Nome == nome)
                    atributo = item;
            }

            return atributo;
        }
    }
}
