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
        string PadraTodosCaracteres = @"([\w\s\>\<\=\!\d\.\,\(\)\&]*)";
        public XPZ XPZ;

        public Converter(XPZ xpz)
        {
            this.XPZ = xpz;
        }

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
