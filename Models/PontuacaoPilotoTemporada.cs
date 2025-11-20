using System.ComponentModel.DataAnnotations;

namespace Formula1.Models
{
    public class PontuacaoPilotoTemporada
    {
        [Key]
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O piloto é obrigatório.")]
        [Display(Name = "Piloto")]
        public int PilotoId { get; set; }
        public Piloto Piloto { get; set; }

        [Required(ErrorMessage = "A temporada é obrigatória.")]
        [Display(Name = "Temporada")]
        public int TemporadaId { get; set; }
        public Temporada Temporada { get; set; }

        [Required(ErrorMessage = "A pontuação é obrigatória.")]
        [Display(Name = "Pontos")]
        public int Pontos { get; set; }
    }
}
