using System.Collections;
using System.Collections.Generic;

namespace GFT_Podcasts.Repositories.Interfaces {
    public interface IRepository<T> {
        void Criar(T obj);

        void Editar(T obj);

        void Remover(T obj);

        T Buscar(int id);

        IEnumerable<T> Listar();
    }
}