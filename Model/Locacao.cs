using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Locacao : Basico
    {
        public DateTime DataEntrega { get; set; }
        public DateTime DataLocacao { get; set; }
        public int Numero { get; set; }

        public int FilmeId { get; set; }
        public int ClienteId { get; set; }
        public int FuncionarioId { get; set; }

        public virtual Filme Filme { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual Funcionario Funcionario { get; set; }

    }
}
