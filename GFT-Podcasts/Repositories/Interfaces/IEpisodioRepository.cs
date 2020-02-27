using System.Collections;
using System.Collections.Generic;
using GFT_Podcasts.Models;
using GFT_Podcasts.Models.ViewModels.EpisodioViewModels;

namespace GFT_Podcasts.Repositories.Interfaces {
    public interface IEpisodioRepository {
        Episodio Buscar(int id);

        void Remover(Episodio obj);

        void Criar(Episodio obj);

        void Editar(Episodio obj);

        IEnumerable<EpisodioListagemViewModel> Listar();
    }
}