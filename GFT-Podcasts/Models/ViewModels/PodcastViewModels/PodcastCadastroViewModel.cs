using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GFT_Podcasts.Models.ViewModels.PodcastViewModels {
    public class PodcastCadastroViewModel {
        
        [Required(ErrorMessage = "Este campo é de preenchimento obrigatório.")]
        [MinLength(3, ErrorMessage = "Nome muito curto.")]
        [MaxLength(100, ErrorMessage = "Nome muito grande.")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "Este campo é de preenchimento obrigatório.")]
        [MinLength(5, ErrorMessage = "Nome do autor muito curto.")]
        [MaxLength(50, ErrorMessage = "Nome do autor muito grande.")]
        public string Autor { get; set; }
        
        [Required(ErrorMessage = "Este campo é de preenchimento obrigatório.")]
        [MinLength(10, ErrorMessage = "A descrição precisa ser mais detalhada.")]
        [MaxLength(100, ErrorMessage = "Descrição muito longa.")]
        public string Descricao { get; set; }
        
        [Required(ErrorMessage = "Este campo é de preenchimento obrigatório.")]
        public string Link { get; set; }
        
        [Required(ErrorMessage = "Este campo é de preenchimento obrigatório.")]
        public string Imagem { get; set; }
        
        public int CategoriaId { get; set; }
    }
}