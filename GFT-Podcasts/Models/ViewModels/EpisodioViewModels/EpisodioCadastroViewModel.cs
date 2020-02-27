using System;
using System.ComponentModel.DataAnnotations;

namespace GFT_Podcasts.Models.ViewModels.EpisodioViewModels {
    public class EpisodioCadastroViewModel {

        [Required(ErrorMessage = "{0} é requerido")]
        [MinLength(3, ErrorMessage = " {0} tem de ser maior que {1}")]
        [MaxLength(100, ErrorMessage = "{0} precisa ser maior que {1}")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "{0} é requerido")]
        [MinLength(3, ErrorMessage = " {0} tem de ser maior que {1}")]
        [MaxLength(100, ErrorMessage = "{0} precisa ser maior que {1}")]
        public string Descricao { get; set; }
        
        [Required(ErrorMessage = "{0} é requerido")]
        [DisplayFormat()]
        public DateTime Lancamento { get; set; }

        [Required(ErrorMessage = "{0} é requerido")]
        public double Duracao { get; set; }


        [Required(ErrorMessage = "{0} é requerido")]
        [MinLength(3, ErrorMessage = " {0} tem de ser maior que {1}")]
        [MaxLength(100, ErrorMessage = "{0} precisa ser maior que {1}")]
        public string LinkAudio { get; set; }

        [Required(ErrorMessage = "{0} é requerido")]
        public int PodcastId { get; set; }
    }
}