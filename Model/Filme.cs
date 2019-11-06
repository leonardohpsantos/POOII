using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Filme : Basico
    {
        public string Titulo { get; set; }
        public int Duracao { get; set; }
        public int Ano { get; set; }

        public virtual Classificacao Classificacao { get; set; }
        public virtual Produtora Produtora { get; set; }
        public virtual Genero Genero { get; set; }
    }
}
