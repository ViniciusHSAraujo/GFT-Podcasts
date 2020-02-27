using System.Collections.Generic;
using System.Linq;
using GFT_Podcasts.Database;
using GFT_Podcasts.Models;
using GFT_Podcasts.Models.ViewModels.PodcastViewModels;
using GFT_Podcasts.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GFT_Podcasts.Repositories {
    public class PodcastRepository : IPodcastRepository {
        private readonly ApplicationDbContext _dbContext;

        public PodcastRepository(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        public void Criar(Podcast obj) {
            _dbContext.Set<Podcast>().Add(obj);
            _dbContext.SaveChanges();
        }

        public void Editar(Podcast obj) {
            _dbContext.Set<Podcast>().Attach(obj);
            _dbContext.Entry(obj).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Remover(Podcast obj) {
            _dbContext.Set<Podcast>().Remove(obj);
            _dbContext.SaveChanges();
        }

        public Podcast Buscar(int id) {
            return _dbContext.Set<Podcast>().Include(x => x.Episodios).Include
                (x => x.Categoria).FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<PodcastListagemViewModel> Listar() {
            return _dbContext.Set<Podcast>().Include(x => x.Episodios).Include
                (x => x.Categoria).Select(x => new PodcastListagemViewModel() {
                Id = x.Id,
                Autor = x.Autor,
                Categoria = x.Categoria.Nome,
                Descricao = x.Descricao,
                Nome = x.Nome,
                Imagem = x.Imagem,
                Link = x.Link,
                QtdeEpisodios = x.Episodios.Count()
            }).ToList();
        }

        public bool Existe(int id) {
            return _dbContext.Set<Podcast>().Any(x => x.Id == id);
        }
    }
}