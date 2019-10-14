using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class Endereco : Basico
    {
        public string Logradouro { get; set; }
        public int ClienteId { get; set; }
    }
}