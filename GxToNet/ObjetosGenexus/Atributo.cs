using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GxToNet.ObjetosGenexus
{
    public class Atributo
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Titulo { get; set; }
        public TipoAtributo Tipo { get; set; }
        public int Tamanho { get; set; }
    }
}
