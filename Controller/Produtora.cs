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
    public class Produtora
    {
        private static string strConection = ConfigurationManager.AppSettings["connection"].ToString();

        public static List<Models.Produtora> Listar()
        {
            using (MySqlConnection conn = new MySqlConnection(strConection))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"SELECT * FROM produtora";

                    using (MySqlDataAdapter da = new MySqlDataAdapter())
                    {
                        da.SelectCommand = cmd;

                        DataSet ds = new DataSet();
                        da.Fill(ds, "Classificacao");

                        List<Models.Produtora> lstRetorno = ds.Tables["Classificacao"].AsEnumerable().Select(x => new Models.Produtora
                        {
                            Id = x.Field<int>("id"),
                            Nome = x.Field<string>("nome")
                        }).ToList();

                        return lstRetorno;
                    }
                }
            }
        }

        public static void Salvar(Models.Produtora produtora)
        {
            using (MySqlConnection conn = new MySqlConnection(strConection))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;

                    if (produtora.Id == 0)
                        cmd.CommandText = @"INSERT INTO produtora (nome) VALUES (?nome);";
                    else
                        cmd.CommandText = @"UPDATE produtora SET nome = ?nome WHERE id = ?id;";

                    cmd.Parameters.AddWithValue("?nome", produtora.Nome);
                    cmd.Parameters.AddWithValue("?id", produtora.Id);

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
                    cmd.CommandText = @"DELETE FROM produtora WHERE id = ?id";
                    cmd.Parameters.AddWithValue("?id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static Models.Produtora BuscarPorId(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(strConection))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"SELECT * FROM produtora WHERE id = ?id";
                    cmd.Parameters.AddWithValue("?id", id);

                    MySqlDataReader reader = cmd.ExecuteReader();

                    Models.Produtora retorno = new Models.Produtora();

                    while (reader.Read())
                    {
                        retorno.Id = (int)reader["Id"];
                        retorno.Nome = (string)reader["nome"];
                    }

                    return retorno;
                }
            }
        }
    }
}
