using System.ComponentModel.DataAnnotations;

namespace GFT_Podcasts.Models.ViewModels.CategoriaViewModels {
    public class CategoriaEdicaoViewModel {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é de preenchimento obrigatório.")]
        [MinLength(3, ErrorMessage = "Nome muito curto.")]
        [MaxLength(100, ErrorMessage = "Nome muito grande.")]
        public string Nome { get; set; }
    }
}