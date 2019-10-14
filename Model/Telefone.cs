using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class Telefone : Basico
    {
        public string Numero { get; set; }
        public int ClienteId { get; set; }
    }
}