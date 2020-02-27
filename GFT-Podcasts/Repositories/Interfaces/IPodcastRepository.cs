using System.Collections.Generic;
using GFT_Podcasts.Models;
using GFT_Podcasts.Models.ViewModels.PodcastViewModels;

namespace GFT_Podcasts.Repositories.Interfaces {
    public interface IPodcastRepository {
        Podcast Buscar(int id);

        void Remover(Podcast obj);

        void Editar(Podcast obj);

        void Criar(Podcast obj);
        IEnumerable<PodcastListagemViewModel> Listar();

        bool Existe(int id);
    }
}