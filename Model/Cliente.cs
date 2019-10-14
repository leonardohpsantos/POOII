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
        public string Email { get; set; }
        [MinLength(11, ErrorMessage = "O cpf deve ter 11 numeros")]
        [MaxLength(11, ErrorMessage = "O cpf deve ter 11 numeros")]
        public string Cpf { get; set; }

        public virtual List<Endereco> Enderecos { get; set; }
        public virtual List<Telefone> Telefones { get; set; }
    }
}