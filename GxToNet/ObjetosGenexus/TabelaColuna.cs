using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GxToNet.ObjetosGenexus
{
    public class TabelaColuna
    {
        public string Nome { get; set; }
        public bool PermiteNulo { get; set; }
        public TipoAtributo Tipo { get; set; }
    }
}
