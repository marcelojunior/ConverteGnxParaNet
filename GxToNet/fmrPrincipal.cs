using GxToNet.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GxToNet
{
    public partial class frmPrincipal : Form
    {
        private Converter converter;
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var conexao = new Conexao(txtDatabase.Text, txtHost.Text, txtUser.Text, txtSenha.Text);
            var XPZ = new XPZ(txtCaminho.Text, conexao);
            converter = new Converter(XPZ);
            converter.Iniciar();
        }

        private void btnProcurar_Click(object sender, EventArgs e)
        {
            openFile.ShowDialog();
        }

        private void openFile_FileOk(object sender, CancelEventArgs e)
        {
            txtCaminho.Text = openFile.FileName;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
