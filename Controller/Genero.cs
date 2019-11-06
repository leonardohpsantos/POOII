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
    public class Genero
    {
        private static string strConection = ConfigurationManager.AppSettings["connection"].ToString();

        public static List<Models.Genero> Listar()
        {
            using (MySqlConnection conn = new MySqlConnection(strConection))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"SELECT * FROM genero";

                    using (MySqlDataAdapter da = new MySqlDataAdapter())
                    {
                        da.SelectCommand = cmd;

                        DataSet ds = new DataSet();
                        da.Fill(ds, "Cliente");

                        List<Models.Genero> lstRetorno = ds.Tables["Cliente"].AsEnumerable().Select(x => new Models.Genero
                        {
                            Id = x.Field<int>("id"),
                            Tipo = x.Field<string>("tipo")
                        }).ToList();

                        return lstRetorno;
                    }
                }
            }
        }

        public static void Salvar(Models.Genero genero)
        {
            using (MySqlConnection conn = new MySqlConnection(strConection))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;

                    if (genero.Id == 0)
                        cmd.CommandText = @"INSERT INTO genero (tipo) VALUES (?tipo);";
                    else
                        cmd.CommandText = @"UPDATE genero SET tipo = ?tipo WHERE id = ?id;";

                    cmd.Parameters.AddWithValue("?tipo", genero.Tipo);
                    cmd.Parameters.AddWithValue("?id", genero.Id);

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
                    cmd.CommandText = @"DELETE FROM genero WHERE id = ?id";
                    cmd.Parameters.AddWithValue("?id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static Models.Genero BuscarPorId(int clienteId)
        {
            using (MySqlConnection conn = new MySqlConnection(strConection))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"SELECT * FROM genero WHERE id = ?id";
                    cmd.Parameters.AddWithValue("?id", clienteId);

                    MySqlDataReader reader = cmd.ExecuteReader();

                    Models.Genero retorno = new Models.Genero();

                    while (reader.Read())
                    {
                        retorno.Id = (int)reader["Id"];
                        retorno.Tipo = (string)reader["tipo"];
                    }

                    return retorno;
                }
            }
        }
    }
}
