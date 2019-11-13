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
    public class Funcionario
    {
        private static string strConection = ConfigurationManager.AppSettings["connection"].ToString();

        public static List<Models.Funcionario> Listar()
        {
            using (MySqlConnection conn = new MySqlConnection(strConection))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"SELECT * FROM funcionario";

                    using (MySqlDataAdapter da = new MySqlDataAdapter())
                    {
                        da.SelectCommand = cmd;

                        DataSet ds = new DataSet();
                        da.Fill(ds, "Funcionario");

                        List<Models.Funcionario> lstRetorno = ds.Tables["Funcionario"].AsEnumerable().Select(x => new Models.Funcionario
                        {
                            Id = x.Field<int>("id"),
                            Nome = x.Field<string>("nome"),
                            Matricula = x.Field<string>("matricula")
                        }).ToList();

                        return lstRetorno;
                    }
                }
            }
        }

        public static void Salvar(Models.Funcionario funcionario)
        {
            using (MySqlConnection conn = new MySqlConnection(strConection))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;

                    if (funcionario.Id == 0)
                        cmd.CommandText = @"INSERT INTO funcionario
                                            (nome, matricula)
                                            VALUES(?nome, ?matricula);";
                    else
                        cmd.CommandText = @"UPDATE funcionario
                                                SET nome = ?nome,
                                                    matricula = ?matricula
                                            WHERE id = ?id;";

                    cmd.Parameters.AddWithValue("?nome", funcionario.Nome);
                    cmd.Parameters.AddWithValue("?matricula", funcionario.Matricula);
                    cmd.Parameters.AddWithValue("?id", funcionario.Id);

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
                    cmd.CommandText = @"DELETE FROM funcionario WHERE id = ?id";
                    cmd.Parameters.AddWithValue("?id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static Models.Funcionario BuscarPorId(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(strConection))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"SELECT * FROM funcionario WHERE id = ?id";
                    cmd.Parameters.AddWithValue("?id", id);

                    MySqlDataReader reader = cmd.ExecuteReader();

                    Models.Funcionario retorno = new Models.Funcionario();

                    while (reader.Read())
                    {
                        retorno.Id = (int)reader["Id"];
                        retorno.Nome = (string)reader["nome"];
                        retorno.Matricula = (string)reader["matricula"];
                    }

                    return retorno;
                }
            }
        }
    }
}
