using System;

namespace GFT_Podcasts.Models.ViewModels.EpisodioViewModels {
    public class EpisodioSimplificadoViewModel {
        public int Id { get; set; }

        public string Titulo { get; set; }

        public string Descricao { get; set; }

        public DateTime Lancamento { get; set; }

        public double Duracao { get; set; }

        public string LinkAudio { get; set; }

        public string Podcast { get; set; }
        
    }
}