using Locadora.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace Locadora.Aplication
{
    public class EnderecoAP
    {
        public static string strConection = ConfigurationManager.AppSettings["connection"].ToString();

        public static List<Endereco> Buscar()
        {
            using (MySqlConnection conn = new MySqlConnection(strConection))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"SELECT * FROM endereco";

                    using (MySqlDataAdapter da = new MySqlDataAdapter())
                    {
                        da.SelectCommand = cmd;

                        DataSet ds = new DataSet();
                        da.Fill(ds, "Endereco");

                        List<Endereco> lstRetorno = ds.Tables["Endereco"].AsEnumerable().Select(x => new Endereco
                        {

                            Id = x.Field<int>("id"),
                            Logradouro = x.Field<string>("endereco")
                        }).ToList();

                        return lstRetorno;
                    }
                }
            }
        }
    }
}