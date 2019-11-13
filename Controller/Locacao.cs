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

        public static Models.Filme BuscarPorId(int locacaoId)
        {
            using (MySqlConnection conn = new MySqlConnection(strConection))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"SELECT * FROM filme WHERE id = ?id";
                    cmd.Parameters.AddWithValue("?id", locacaoId);

                    MySqlDataReader reader = cmd.ExecuteReader();

                    Models.Filme retorno = new Models.Filme();

                    while (reader.Read())
                    {
                        retorno.Id = (int)reader["Id"];
                        retorno.Duracao = (int)reader["duracao"];
                        retorno.Ano = (int)reader["ano"];
                        retorno.Titulo = (string)reader["titulo"];
                    }

                    return retorno;
                }
            }
        }

        public static Models.Filme BuscarFilmeCompletoPorId(int id )
        {
            using (MySqlConnection conn = new MySqlConnection(strConection))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"SELECT
                                            f.*,
                                            p.nome,
                                            c.faixaEtaria,
                                            g.tipo
                                        FROM filme AS f
                                        INNER JOIN produtora AS p ON (p.id = f.idProdutora)
                                        INNER JOIN genero AS g ON (g.id = f.idGenero)
                                        INNER JOIN classificacao AS c ON (c.id = f.idClassificacao)
                                        WHERE f.id = ?id";

                    cmd.Parameters.AddWithValue("?id", id);

                    MySqlDataReader reader = cmd.ExecuteReader();

                    Models.Filme retorno = new Models.Filme();

                    while (reader.Read())
                    {
                        retorno.Id = (int)reader["Id"];
                        retorno.Duracao = (int)reader["duracao"];
                        retorno.Ano = (int)reader["ano"];
                        retorno.Titulo = (string)reader["titulo"];

                        retorno.ClassificacaoId = (int)reader["idClassificacao"];
                        retorno.GeneroId = (int)reader["idGenero"];
                        retorno.ProdutoraId = (int)reader["idProdutora"];

                        if (reader["idClassificacao"] != DBNull.Value)
                        {
                            retorno.Classificacao = new Models.Classificacao
                            {
                                Id = (int)reader["idClassificacao"],
                                FaixaEtaria = (string)reader["faixaEtaria"]
                            };
                        }

                        if (reader["idProdutora"] != DBNull.Value)
                        {
                            retorno.Produtora = new Models.Produtora
                            {
                                Id = (int)reader["idProdutora"],
                                Nome = (string)reader["nome"]
                            };
                        }

                        if (reader["idGenero"] != DBNull.Value)
                        {
                            retorno.Genero = new Models.Genero
                            {
                                Id = (int)reader["idGenero"],
                                Tipo = (string)reader["tipo"]
                            };
                        }
                    }

                    return retorno;
                }
            }
        }
    }
}
