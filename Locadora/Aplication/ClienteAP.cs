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
    public class ClienteAP
    {
        public static string strConection = ConfigurationManager.AppSettings["connection"].ToString();

        public static List<Cliente> Buscar()
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

                        List<Cliente> lstRetorno = ds.Tables["Cliente"].AsEnumerable().Select(x => new Cliente
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
    }
}