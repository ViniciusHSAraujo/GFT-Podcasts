using System.Collections.Generic;
using GFT_Podcasts.Models;
using GFT_Podcasts.Models.ViewModels.CategoriaViewModels;

namespace GFT_Podcasts.Repositories.Interfaces {
    public interface ICategoriaRepository {
        Categoria Buscar(int id);

        void Remover(Categoria obj);

        void Editar(Categoria obj);

        void Criar(Categoria obj);
        bool Existe(int id);
        IEnumerable<CategoriaListagemViewModel> Listar();
    }
}