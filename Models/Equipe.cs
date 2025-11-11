using System.ComponentModel.DataAnnotations;

namespace Formula1.Models
{
    public class Equipe
    {
        [Key]
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da equipe é obrigatório.")]
        [Display(Name = "Nome da Equipe")]
        public string Nome_equipe { get; set; }
    }
}
