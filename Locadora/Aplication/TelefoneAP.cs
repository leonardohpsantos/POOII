using Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace Locadora.Aplication
{
    public class TelefoneAP
    {
        private string strConection = ConfigurationManager.AppSettings["connection"].ToString();

        public List<Telefone> Buscar()
        {
            using (MySqlConnection conn = new MySqlConnection(strConection))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"SELECT * FROM telefone";

                    using (MySqlDataAdapter da = new MySqlDataAdapter())
                    {
                        da.SelectCommand = cmd;

                        DataSet ds = new DataSet();
                        da.Fill(ds, "Telefone");

                        List<Telefone> lstRetorno = ds.Tables["Telefone"].AsEnumerable().Select(x => new Telefone
                        {
                            Id = x.Field<int>("id"),
                            Numero = x.Field<string>("numero"),
                            ClienteId = x.Field<int>("idCliente")
                        }).ToList();

                        return lstRetorno;
                    }
                }
            }
        }
    }
}