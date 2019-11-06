using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Classificacao : Basico
    {
        [Display(Name = "Faixa Etária")]
        public string FaixaEtaria { get; set; }
    }
}