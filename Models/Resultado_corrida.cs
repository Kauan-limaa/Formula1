using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Formula1.Models
{
    public class ResultadoCorrida
    {
        [Key]
        [Display(Name = "Código")]
        public int Id { get; set; }


        [Required(ErrorMessage = "O piloto é obrigatório.")]
        [Display(Name = "Piloto")]
        public int PilotoId { get; set; }
        public Piloto Piloto { get; set; }

        [Required(ErrorMessage = "A equipe é obrigatório.")]
        [Display(Name = "Equipe")]
        public int EquipeId { get; set; }
        public Equipe Equipe { get; set; }

        [Required(ErrorMessage = "A temporada é obrigatória.")]
        [Display(Name = "Temporada")]
        public int TemporadaId { get; set; }
        public Temporada Temporada { get; set; }


        [Required(ErrorMessage = "A pista é obrigatória.")]
        [Display(Name = "Pista")]
        public int PistaId { get; set; }
        public Pista Pista { get; set; }


        [Required(ErrorMessage = "A posição é obrigatória.")]
        [Display(Name = "Posição")]
        public int Posicao { get; set; }


        [Display(Name = "Pontos Ganhos")]
        public int PontosGanhos { get; set; }
    }
}
