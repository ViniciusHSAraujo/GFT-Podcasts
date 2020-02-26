using System.Collections.Generic;
using System.Linq;
using GFT_Podcasts.Database;
using GFT_Podcasts.Models;
using GFT_Podcasts.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GFT_Podcasts.Repositories {
    public class CategoriaRepository : ICategoriaRepository {

        private readonly ApplicationDbContext _dbContext;
        
        public CategoriaRepository(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        public void Criar(Categoria obj) {
            _dbContext.Set<Categoria>().Add(obj);
            _dbContext.SaveChanges();
        }

        public void Editar(Categoria obj) {
            _dbContext.Set<Categoria>().Attach(obj);
            _dbContext.Entry(obj).State = EntityState.Modified;
            _dbContext.SaveChanges();        }

        public void Remover(Categoria obj) {
            _dbContext.Set<Categoria>().Remove(obj);
            _dbContext.SaveChanges();
        }

        public Categoria Buscar(int id) {
            return _dbContext.Set<Categoria>().Include(x => x.Podcasts).FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Categoria> Listar() {
            return _dbContext.Set<Categoria>().ToList();
        }
    }
}