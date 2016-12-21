using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace GxToNet
{
    public class Converter
    {
        string CodigoConvertido = "";
        string PadraTodosCaracteres = @"([\w\s\>\<\=\!\d\.\,\(\)\&]*)";
        public string CodigoGnxOriginal { get; set; } 
        public string CodigoNetOriginal { get; set; }

        public void Iniciar()
        {
            this.MetodosPrivados();
            this.IfsComElse();
        }

        private void IfsComElse()
        {
            var padrao = @"
                    (If)(\s{1,})([\w\s\>\<\=\!\d]*)\n
                        ([\w\s\>\<\=\!\d\.\,\(\)\&]*)
                    (Else)\n
                        ([\w\s\>\<\=\!\d\.\,\(\)\&]*)
                    (EndIf)
                ".Replace(" ", string.Empty);
        }

        private void MetodosPrivados()
        {
            var padrao = @"(Sub)\s('{1,}(\w{1,})')".Replace(" ", string.Empty);

        }

      

    }
}
