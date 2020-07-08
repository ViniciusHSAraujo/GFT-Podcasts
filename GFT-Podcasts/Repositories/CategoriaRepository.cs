using System.Collections.Generic;
using System.Linq;
using GFT_Podcasts.Database;
using GFT_Podcasts.Models;
using GFT_Podcasts.Models.ViewModels.CategoriaViewModels;
using GFT_Podcasts.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GFT_Podcasts.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {

        private readonly ApplicationDbContext _dbContext;

        public CategoriaRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Criar(Categoria obj)
        {
            _dbContext.Set<Categoria>().Add(obj);
            _dbContext.SaveChanges();
        }

        public void Editar(Categoria obj)
        {
            _dbContext.Set<Categoria>().Attach(obj);
            _dbContext.Entry(obj).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public bool Existe(int id)
        {
            return _dbContext.Set<Categoria>().Any(x => x.Id == id);
        }

        public void Remover(Categoria obj)
        {
            _dbContext.Set<Categoria>().Remove(obj);
            _dbContext.SaveChanges();
        }

        public Categoria Buscar(int id)
        {
            return _dbContext.Set<Categoria>().Include(x => x.Podcasts).FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<CategoriaListagemViewModel> Listar()
        {
            return _dbContext.Set<Categoria>().Select(x => new CategoriaListagemViewModel() { Id = x.Id, Nome = x.Nome }).ToList();
        }
    }
}