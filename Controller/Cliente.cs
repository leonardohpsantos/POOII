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
    public class Cliente
    {
        private static string strConection = ConfigurationManager.AppSettings["connection"].ToString();

        public static List<Models.Cliente> Listar()
        {
            using (MySqlConnection conn = new MySqlConnection(strConection))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"SELECT * FROM cliente";

                    using (MySqlDataAdapter da = new MySqlDataAdapter())
                    {
                        da.SelectCommand = cmd;

                        DataSet ds = new DataSet();
                        da.Fill(ds, "Cliente");

                        List<Models.Cliente> lstRetorno = ds.Tables["Cliente"].AsEnumerable().Select(x => new Models.Cliente
                        {
                            Id = x.Field<int>("id"),
                            Nome = x.Field<string>("nome"),
                            Email = x.Field<string>("email"),
                            Cpf = x.Field<string>("cpf")
                        }).ToList();

                        return lstRetorno;
                    }
                }
            }
        }

        public static void Salvar(Models.Cliente cliente)
        {
            using (MySqlConnection conn = new MySqlConnection(strConection))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;

                    if (cliente.Id == 0)
                        cmd.CommandText = @"INSERT INTO cliente (nome, email, cpf) VALUES (?nome, ?email, ?cpf);";
                    else
                        cmd.CommandText = @"UPDATE cliente SET nome = ?nome, email = ?email, cpf = ?cpf WHERE id = ?id;";

                    cmd.Parameters.AddWithValue("?nome", cliente.Nome);
                    cmd.Parameters.AddWithValue("?email", cliente.Email);
                    cmd.Parameters.AddWithValue("?cpf", cliente.Cpf);
                    cmd.Parameters.AddWithValue("?id", cliente.Id);

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
                    cmd.CommandText = @"DELETE FROM cliente WHERE id = ?id";
                    cmd.Parameters.AddWithValue("?id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static Models.Cliente BuscarPorId(int clienteId)
        {
            using (MySqlConnection conn = new MySqlConnection(strConection))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"SELECT * FROM cliente WHERE id = ?id";
                    cmd.Parameters.AddWithValue("?id", clienteId);

                    MySqlDataReader reader = cmd.ExecuteReader();

                    Models.Cliente retorno = new Models.Cliente();

                    while (reader.Read())
                    {
                        retorno.Id = (int)reader["Id"];
                        retorno.Nome = (string)reader["Nome"];
                        retorno.Email = (string)reader["Email"];
                        retorno.Cpf = (string)reader["cpf"];
                    }

                    return retorno;
                }
            }
        }
    }
}
