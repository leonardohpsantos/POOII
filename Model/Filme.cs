using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Filme : Basico
    {
        [DisplayName("Título")]
        public string Titulo { get; set; }
        [DisplayName("Duração")]
        public int Duracao { get; set; }
        public int Ano { get; set; }
        [DisplayName("Classificação")]
        public int ClassificacaoId { get; set; }
        [DisplayName("Gênero")]
        public int GeneroId { get; set; }
        [DisplayName("Produtora")]
        public int ProdutoraId { get; set; }

        public virtual Classificacao Classificacao { get; set; }
        public virtual Produtora Produtora { get; set; }
        public virtual Genero Genero { get; set; }
    }
}
