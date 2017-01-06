using GxToNet.ObjetosGenexus;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GxToNet.DAL
{
    public class Conexao
    {
        public string BancoDeDados { get; set; }
        public string Host { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public string ConnectionString
        {
            get
            {
                return string.Format("data source={0};initial catalog={1};persist security info=True;user id={2};password={3};",
                    Host, BancoDeDados, Usuario, Senha);
            }
        }

        public Conexao(string _Bd, string _Host, string _User, string _Pass)
        {
            this.BancoDeDados = _Bd;
            this.Host = _Host;
            this.Usuario = _User;
            this.Senha = _Pass;
        }

        public SqlConnection NovaConexao()
        {
            return new SqlConnection(this.ConnectionString);
        }

        public List<string> ListarNomeDeColunas(string nomeDaTabela)
        {
            var colunas = new List<string>();
            using(SqlConnection conn = this.NovaConexao()){
                var sql = string.Format(@"SELECT name FROM sys.columns WHERE object_id = OBJECT_ID('dbo.{0}')", nomeDaTabela);
                SqlCommand command = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    colunas.Add(reader[0].ToString());
                }
                reader.Close();
            }

            return colunas;
        }

        public bool VerificarColunaPermiteNulo(string nomeDaTabela, string nomeColuna)
        {
            var permiteNulo = true;
            using (SqlConnection conn = this.NovaConexao())
            {
                var sql = string.Format(@"SELECT is_nullable FROM sys.columns WHERE object_id = OBJECT_ID('dbo.{0}') and name = '{1}'", nomeDaTabela, nomeColuna);
                SqlCommand command = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    permiteNulo = reader[0].ToString() == "1";
                }
                reader.Close();
            }

            return permiteNulo;
        }

        public TipoAtributo VerificarTipoColuna(string nomeDaTabela, string nomeColuna)
        {
            var tipo = TipoAtributo.Texto;
            using (SqlConnection conn = this.NovaConexao())
            {
                var sql = string.Format(@"
                        SELECT tp.name FROM sys.columns cl 
                        inner join sys.types tp on cl.system_type_id = tp.system_type_id
                        WHERE object_id = OBJECT_ID('dbo.{0}')  and cl.name = '{1}'
                ", 
                    nomeDaTabela, nomeColuna);
                SqlCommand command = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var tipoBanco = reader[0].ToString();
                    if (tipoBanco == "smallint" || tipoBanco == "tinyint")
                        tipo = TipoAtributo.SmallInt;
                    else if (tipoBanco == "datetime" || tipoBanco == "date" || tipoBanco == "datetime2")
                        tipo = TipoAtributo.Data;
                    else if (tipoBanco == "money" || tipoBanco == "float" || tipoBanco == "numeric" || tipoBanco == "decimal")
                        tipo = TipoAtributo.Decimal;
                    else if (tipoBanco == "int")
                        tipo = TipoAtributo.Inteiro;
                    else if (tipoBanco == "bigint")
                        tipo = TipoAtributo.Long;
                    else if (tipoBanco == "varchar" || tipoBanco == "nchar" || tipoBanco == "nvarchar")
                        tipo = TipoAtributo.Texto;
                    else
                        new Exception("O tipo " + tipoBanco + " não foi programado.");
                }
                reader.Close();
            }

            return tipo;
        }
       
    }
}
