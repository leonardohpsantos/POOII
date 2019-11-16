using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
    public class Cliente : Basico
    {
        [Required]
        public string Nome { get; set; }
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        [Display(Name ="Documento")]
        public string Cpf { get; set; }

        public virtual List<Endereco> Enderecos { get; set; }
        public virtual List<Telefone> Telefones { get; set; }
    }
}