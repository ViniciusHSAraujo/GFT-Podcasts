﻿using System.Collections.Generic;
using System.Linq;
using GFT_Podcasts.Database;
using GFT_Podcasts.Models;
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
            return _dbContext.Set<Podcast>().FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Podcast> Listar() {
            return _dbContext.Set<Podcast>().Include(x => x.Episodios).ToList();
        }
    }
}