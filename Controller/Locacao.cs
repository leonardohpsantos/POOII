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
    public class Locacao
    {
        private static string strConection = ConfigurationManager.AppSettings["connection"].ToString();

        public static List<Models.Locacao> Listar()
        {
            using (MySqlConnection conn = new MySqlConnection(strConection))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"SELECT * FROM locacao";

                    using (MySqlDataAdapter da = new MySqlDataAdapter())
                    {
                        da.SelectCommand = cmd;

                        DataSet ds = new DataSet();
                        da.Fill(ds, "Locacao");

                        List<Models.Locacao> lstRetorno = ds.Tables["Locacao"].AsEnumerable().Select(x => new Models.Locacao
                        {
                            Id = x.Field<int>("id"),
                            DataEntrega = x.Field<DateTime>("dataEntrega"),
                            DataLocacao  = x.Field<DateTime>("dataLocacao"),
                            Numero = x.Field<int>("numeroLocacao")
                        }).ToList();

                        return lstRetorno;
                    }
                }
            }
        }

        public static void Salvar(Models.Locacao locacao)
        {
            using (MySqlConnection conn = new MySqlConnection(strConection))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;

                    if (locacao.Id == 0)
                        cmd.CommandText = @"INSERT INTO locacao
                                            (dataEntrega, dataLocacao, numeroLocacao, idCliente, idFilme, idFuncionario)
                                            VALUES(?dataEntrega, ?dataLocacao, ?numeroLocacao, ?idCliente, ?idFilme, ?idFuncionario);";
                    else
                        cmd.CommandText = @"UPDATE locacao
                                                SET dataEntrega = ?dataEntrega,
                                                    dataLocacao = ?dataLocacao,
                                                    numeroLocacao = ?numeroLocacao,
                                                    idCliente = ?idCliente,
                                                    idFilme = ?idFilme,
                                                    idFuncionario = ?idFuncionario
                                            WHERE id = ?id;";

                    cmd.Parameters.AddWithValue("?dataEntrega", locacao.DataEntrega);
                    cmd.Parameters.AddWithValue("?dataLocacao", locacao.DataLocacao);
                    cmd.Parameters.AddWithValue("?numeroLocacao", locacao.Numero);
                    cmd.Parameters.AddWithValue("?idCliente", locacao.ClienteId);
                    cmd.Parameters.AddWithValue("?idFilme", locacao.FilmeId);
                    cmd.Parameters.AddWithValue("?idFuncionario", locacao.FuncionarioId);

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
                    cmd.CommandText = @"DELETE FROM locacao WHERE id = ?id";
                    cmd.Parameters.AddWithValue("?id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static Models.Locacao BuscarPorId(int locacaoId)
        {
            using (MySqlConnection conn = new MySqlConnection(strConection))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"SELECT * FROM locacao WHERE id = ?id";
                    cmd.Parameters.AddWithValue("?id", locacaoId);

                    MySqlDataReader reader = cmd.ExecuteReader();

                    Models.Locacao retorno = new Models.Locacao();

                    while (reader.Read())
                    {
                        retorno.Id = (int)reader["Id"];
                        retorno.DataEntrega = (DateTime)reader["dataEntrega"];
                        retorno.DataLocacao = (DateTime)reader["dataLocacao"];
                        retorno.Numero = (int)reader["numero"];
                    }

                    return retorno;
                }
            }
        }

        public static Models.Locacao BuscarFilmeCompletoPorId(int id )
        {
            using (MySqlConnection conn = new MySqlConnection(strConection))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"SELECT
	                                        l.*,
	                                        f.titulo,
	                                        fc.nome AS funcionarioNome,
	                                        c.nome AS clienteNome
                                        FROM locacao AS l
                                        INNER JOIN filme AS f ON (f.id = l.idFilme)
                                        INNER JOIN funcionario AS fc ON (fc.id = l.idFuncionario)
                                        INNER JOIN cliente AS c ON (c.id = l.idCliente)
                                        WHERE l.id = ?id";

                    cmd.Parameters.AddWithValue("?id", id);

                    MySqlDataReader reader = cmd.ExecuteReader();

                    Models.Locacao retorno = new Models.Locacao();

                    while (reader.Read())
                    {
                        retorno.Id = (int)reader["Id"];
                        retorno.Numero = (int)reader["numeroLocacao"];
                        retorno.DataEntrega = (DateTime)reader["dataEntrega"];
                        retorno.DataLocacao = (DateTime)reader["dataLocacao"];

                        retorno.FilmeId = (int)reader["idFilme"];
                        retorno.FuncionarioId = (int)reader["idFuncionario"];
                        retorno.ClienteId = (int)reader["idCliente"];

                        if (reader["idFilme"] != DBNull.Value)
                        {
                            retorno.Filme = new Models.Filme
                            {
                                Id = (int)reader["idFilme"],
                                Titulo = (string)reader["titulo"]
                            };
                        }

                        if (reader["idFuncionario"] != DBNull.Value)
                        {
                            retorno.Funcionario = new Models.Funcionario
                            {
                                Id = (int)reader["idFuncionario"],
                                Nome = (string)reader["funcionarioNome"]
                            };
                        }

                        if (reader["idCliente"] != DBNull.Value)
                        {
                            retorno.Cliente = new Models.Cliente
                            {
                                Id = (int)reader["idCliente"],
                                Nome = (string)reader["clienteNome"]
                            };
                        }
                    }

                    return retorno;
                }
            }
        }
    }
}
