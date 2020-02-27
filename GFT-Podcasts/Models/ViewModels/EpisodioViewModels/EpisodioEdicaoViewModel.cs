using System;
using System.ComponentModel.DataAnnotations;

namespace GFT_Podcasts.Models.ViewModels.EpisodioViewModels {
    public class EpisodioEdicaoViewModel {

        [Display(Name = "Id")]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
        [MinLength(3, ErrorMessage = " O campo {0} deve ter no mínimo {1} caracteres")]
        [MaxLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
        [MinLength(3, ErrorMessage = " O campo {0} deve ter no mínimo {1} caracteres")]
        [MaxLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres")]
        public string Descricao { get; set; }
        
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
        public DateTime Lancamento { get; set; }

        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
        public double Duracao { get; set; }


        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
        [MinLength(3, ErrorMessage = " O campo {0} deve ter no mínimo {1} caracteres")]
        [MaxLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres")]
        public string LinkAudio { get; set; }

        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
        public int PodcastId { get; set; }
    }
}