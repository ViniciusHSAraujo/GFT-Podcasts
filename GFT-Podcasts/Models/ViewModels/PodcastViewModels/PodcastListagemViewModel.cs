﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GFT_Podcasts.Models.ViewModels.PodcastViewModels {
    public class PodcastListagemViewModel {

        public int Id { get; set; }

        public string Nome { get; set; }
        
        public string Autor { get; set; }

        public string Descricao { get; set; }

        public string Link { get; set; }

        public string Imagem { get; set; }

        public string Categoria { get; set; }

        public int QtdeEpisodios { get; set; }
    }
}