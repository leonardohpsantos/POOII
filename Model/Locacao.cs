using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Locacao : Basico
    {
        [DisplayName("Data da Entrega")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}")]
        public DateTime DataEntrega { get; set; }
        [DisplayName("Data da Locação")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}")]
        public DateTime DataLocacao { get; set; }
        [DisplayName("Número")]
        public int Numero { get; set; }

        public int FilmeId { get; set; }
        public int ClienteId { get; set; }
        public int FuncionarioId { get; set; }

        public virtual Filme Filme { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual Funcionario Funcionario { get; set; }

    }
}
