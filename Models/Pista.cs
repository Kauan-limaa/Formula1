using System.ComponentModel.DataAnnotations;

namespace Formula1.Models
{
    public class Pista
    {
        [Key]
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da pista é obrigatório.")]
        [Display(Name = "Nome da Pista")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O país é obrigatório.")]
        [Display(Name = "País")]
        public string Pais { get; set; }

        [Required(ErrorMessage = "O tamanho da pista é obrigatório.")]
        [Display(Name = "Tamanho (km)")]
        public float Tamanho { get; set; }

        [Required(ErrorMessage = "O tempo de volta é obrigatório.")]
        [Display(Name = "Tempo de Volta (s)")]
        public float Tempo_volta { get; set; }
    }
}
