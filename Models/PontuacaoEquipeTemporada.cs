using System.ComponentModel.DataAnnotations;

namespace Formula1.Models
{
    public class PontuacaoEquipeTemporada
    {
        [Key]
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Required(ErrorMessage = "A equipe é obrigatória.")]
        [Display(Name = "Equipe")]
        public int EquipeId { get; set; }
        public Equipe Equipe { get; set; } // Propriedade de navegação para a Equipe

        [Required(ErrorMessage = "A temporada é obrigatória.")]
        [Display(Name = "Temporada")]
        public int TemporadaId { get; set; }
        public Temporada Temporada { get; set; } // Propriedade de navegação para a Temporada

        [Required(ErrorMessage = "A pontuação é obrigatória.")]
        [Display(Name = "Pontos")]
        public int Pontos { get; set; }
    }
}