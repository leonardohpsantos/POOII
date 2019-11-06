using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public class Classificacao
    {
        private static string strConection = ConfigurationManager.AppSettings["connection"].ToString();

        public static List<Models.Classificacao> Listar()
        {
            using (MySqlConnection conn = new MySqlConnection(strConection))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"SELECT * FROM classificacao";

                    using (MySqlDataAdapter da = new MySqlDataAdapter())
                    {
                        da.SelectCommand = cmd;

                        DataSet ds = new DataSet();
                        da.Fill(ds, "Classificacao");

                        List<Models.Classificacao> lstRetorno = ds.Tables["Classificacao"].AsEnumerable().Select(x => new Models.Classificacao
                        {
                            Id = x.Field<int>("id"),
                            FaixaEtaria = x.Field<string>("faixaEtaria")
                        }).ToList();

                        return lstRetorno;
                    }
                }
            }
        }

        public static void Salvar(Models.Classificacao classificacao)
        {
            using (MySqlConnection conn = new MySqlConnection(strConection))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;

                    if (classificacao.Id == 0)
                        cmd.CommandText = @"INSERT INTO classificacao (faixaEtaria) VALUES (?faixaEtaria);";
                    else
                        cmd.CommandText = @"UPDATE classificacao SET faixaEtaria = ?faixaEtaria WHERE id = ?id;";

                    cmd.Parameters.AddWithValue("?faixaEtaria", classificacao.FaixaEtaria);
                    cmd.Parameters.AddWithValue("?id", classificacao.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void Excluir(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(strConection))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"DELETE FROM classificacao WHERE id = ?id";
                    cmd.Parameters.AddWithValue("?id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static Models.Classificacao BuscarPorId(int clienteId)
        {
            using (MySqlConnection conn = new MySqlConnection(strConection))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"SELECT * FROM classificacao WHERE id = ?id";
                    cmd.Parameters.AddWithValue("?id", clienteId);

                    MySqlDataReader reader = cmd.ExecuteReader();

                    Models.Classificacao retorno = new Models.Classificacao();

                    while (reader.Read())
                    {
                        retorno.Id = (int)reader["Id"];
                        retorno.FaixaEtaria = (string)reader["faixaEtaria"];
                    }

                    return retorno;
                }
            }
        }
    }
}
