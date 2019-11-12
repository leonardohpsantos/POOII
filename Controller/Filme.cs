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
    public class Filme
    {
        private static string strConection = ConfigurationManager.AppSettings["connection"].ToString();

        public static List<Models.Filme> Listar()
        {
            using (MySqlConnection conn = new MySqlConnection(strConection))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"SELECT * FROM filme";

                    using (MySqlDataAdapter da = new MySqlDataAdapter())
                    {
                        da.SelectCommand = cmd;

                        DataSet ds = new DataSet();
                        da.Fill(ds, "Filme");

                        List<Models.Filme> lstRetorno = ds.Tables["Filme"].AsEnumerable().Select(x => new Models.Filme
                        {
                            Id = x.Field<int>("id"),
                            Duracao = x.Field<int>("duracao"),
                            Titulo = x.Field<string>("titulo"),
                            Ano = x.Field<int>("ano")
                        }).ToList();

                        return lstRetorno;
                    }
                }
            }
        }

        public static void Salvar(Models.Filme filme)
        {
            using (MySqlConnection conn = new MySqlConnection(strConection))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;

                    if (filme.Id == 0)
                        cmd.CommandText = @"INSERT INTO filme
                                            (titulo, duracao, ano, idClassificacao, idProdutora, idGenero)
                                            VALUES(?titulo, ?duracao, ?ano, ?idClassificacao, ?idProdutora, ?idGenero);";
                    else
                        cmd.CommandText = @"UPDATE filme
                                                SET titulo = ?titulo,
                                                    duracao = ?duracao,
                                                    ano = ?ano,
                                                    idClassificacao = ?idClassificacao,
                                                    idProdutora = ?idProdutora,
                                                    idGenero = ?idGenero
                                            WHERE id = ?id;";

                    cmd.Parameters.AddWithValue("?titulo", filme.Titulo);
                    cmd.Parameters.AddWithValue("?duracao", filme.Duracao);
                    cmd.Parameters.AddWithValue("?ano", filme.Ano);
                    cmd.Parameters.AddWithValue("?idClassificacao", filme.ClassificacaoId);
                    cmd.Parameters.AddWithValue("?idProdutora", filme.ProdutoraId);
                    cmd.Parameters.AddWithValue("?idGenero", filme.GeneroId);

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
                    cmd.CommandText = @"DELETE FROM filme WHERE id = ?id";
                    cmd.Parameters.AddWithValue("?id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static Models.Filme BuscarPorId(int clienteId)
        {
            using (MySqlConnection conn = new MySqlConnection(strConection))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"SELECT * FROM filme WHERE id = ?id";
                    cmd.Parameters.AddWithValue("?id", clienteId);

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
                                        WHERE id = ?id";

                    cmd.Parameters.AddWithValue("?id", id);

                    MySqlDataReader reader = cmd.ExecuteReader();

                    Models.Filme retorno = new Models.Filme();

                    while (reader.Read())
                    {
                        retorno.Id = (int)reader["Id"];
                        retorno.Duracao = (int)reader["duracao"];
                        retorno.Ano = (int)reader["ano"];
                        retorno.Titulo = (string)reader["titulo"];

                        if (reader["idClassificao"] != DBNull.Value)
                        {
                            retorno.Classificacao = new Models.Classificacao
                            {
                                Id = (int)reader["IdClassificacao"],
                                FaixaEtaria = (string)reader["faixaEtaria"]
                            };
                        }

                        if (reader["idProdutora"] != DBNull.Value)
                        {
                            retorno.Produtora = new Models.Produtora
                            {
                                Id = (int)reader["IdClassificacao"],
                                Nome = (string)reader["nome"]
                            };
                        }

                        if (reader["idGenero"] != DBNull.Value)
                        {
                            retorno.Genero = new Models.Genero
                            {
                                Id = (int)reader["IdClassificacao"],
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
