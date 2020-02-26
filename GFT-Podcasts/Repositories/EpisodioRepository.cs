using System.Collections.Generic;
using System.Linq;
using GFT_Podcasts.Database;
using GFT_Podcasts.Models;
using GFT_Podcasts.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GFT_Podcasts.Repositories {
    public class EpisodioRepository : IEpisodioRepository {
        private readonly ApplicationDbContext _dbContext;
        
        public EpisodioRepository(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        public void Criar(Episodio obj) {
            _dbContext.Set<Episodio>().Add(obj);
            _dbContext.SaveChanges();
        }

        public void Editar(Episodio obj) {
            _dbContext.Set<Episodio>().Attach(obj);
            _dbContext.Entry(obj).State = EntityState.Modified;
            _dbContext.SaveChanges();        }

        public void Remover(Episodio obj) {
            _dbContext.Set<Episodio>().Remove(obj);
            _dbContext.SaveChanges();
        }

        public Episodio Buscar(int id) {
            return _dbContext.Set<Episodio>().FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Episodio> Listar() {
            return _dbContext.Set<Episodio>().ToList();
        }
    }
}