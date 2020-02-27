using System;
using System.ComponentModel.DataAnnotations;

namespace GFT_Podcasts.Models.ViewModels.EpisodioViewModels {
    public class EpisodioCadastroViewModel {

        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
        [MinLength(3, ErrorMessage = " O campo {0} deve ter no mínimo {1} caracteres")]
        [MaxLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
        [MinLength(3, ErrorMessage = " O campo {0} deve ter no mínimo {1} caracteres")]
        [MaxLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres")]
        public string Descricao { get; set; }
        
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
        [DisplayFormat()]
        public DateTime Lancamento { get; set; }

        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
        [Range(0, int.MaxValue, ErrorMessage = "O valor do campo {0} deve estar entre {1} e {2}.")]
        public int Duracao { get; set; }

        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
        [MinLength(3, ErrorMessage = " O campo {0} deve ter no mínimo {1} caracteres")]
        [MaxLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres")]
        public string LinkAudio { get; set; }

        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
        public int PodcastId { get; set; }
    }
}