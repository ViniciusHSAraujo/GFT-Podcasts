using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GFT_Podcasts.Models.ViewModels.PodcastViewModels {
    public class PodcastCadastroViewModel {
        
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
        [MinLength(3, ErrorMessage = " O campo {0} deve ter no mínimo {1} caracteres")]
        [MaxLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
        [MinLength(3, ErrorMessage = " O campo {0} deve ter no mínimo {1} caracteres")]
        [MaxLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres")]
        public string Autor { get; set; }
        
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
        [MinLength(3, ErrorMessage = " O campo {0} deve ter no mínimo {1} caracteres")]
        [MaxLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres")]
        public string Descricao { get; set; }
        
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
        public string Link { get; set; }
        
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
        public string Imagem { get; set; }
        
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
        public int CategoriaId { get; set; }
    }
}