using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GFT_Podcasts.Models {
    public class Episodio {
        public int Id { get; set; }

        public string Titulo { get; set; }

        public string Descricao { get; set; }

        public DateTime Lancamento { get; set; }

        public double Duracao { get; set; }

        public string LinkAudio { get; set; }

        public int PodcastId { get; set; }
        [ForeignKey(nameof(PodcastId))] public virtual Podcast Podcast { get; set; }
    }
}