using System.ComponentModel.DataAnnotations;

namespace Formula1.Models
{
    public class Temporada
    {
        [Key]
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O ano é obrigatório.")]
        [Display(Name = "Ano da Temporada")]
        public int Ano { get; set; }

        [Display(Name = "Líder da Temporada")]
        public string Lider_temporada { get; set; }
    }
}
