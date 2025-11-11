using System.ComponentModel.DataAnnotations;

namespace Formula1.Models
{
    public class Piloto
    {
        [Key]
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do piloto é obrigatório.")]
        [Display(Name = "Nome do Piloto")]
        public string Nome_piloto { get; set; }

        [Required(ErrorMessage = "A idade é obrigatória.")]
        [Display(Name = "Idade")]
        public int Idade { get; set; }

        [Required(ErrorMessage = "A altura é obrigatória.")]
        [Display(Name = "Altura (m)")]
        public float Altura { get; set; }

        [Required(ErrorMessage = "O peso é obrigatório.")]
        [Display(Name = "Peso (kg)")]
        public float Peso { get; set; }
    }
}
