using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GxToNet.ObjetosGenexus
{
    public class Tabela
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public List<TabelaChave> Chaves { get; set; }
        public List<TabelaColuna> Colunas { get; set; }

        public Tabela()
        {
            this.Chaves = new List<TabelaChave>();
            this.Colunas = new List<TabelaColuna>();
        }
    }
}
