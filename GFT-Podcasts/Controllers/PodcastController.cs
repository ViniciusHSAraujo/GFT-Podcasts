using System;
using System.Collections.Generic;
using GFT_Podcasts.Models;
using GFT_Podcasts.Models.ViewModels.PodcastViewModels;
using GFT_Podcasts.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GFT_Podcasts.Controllers {
    [Route("api/")]
    public class PodcastController : ControllerBase {
        private readonly IPodcastRepository _podcastRepository;
        
        public PodcastController(IPodcastRepository podcastRepository) {
            _podcastRepository = podcastRepository;
        }

        [HttpGet]
        [Route("v1/podcasts/{id}")]
        public Podcast Get(int id) {
            return _podcastRepository.Buscar(id);
        }

        [HttpGet]
        [Route("v1/podcasts")]
        public IEnumerable<PodcastListagemViewModel> Get() {
            return _podcastRepository.Listar();
        }
        
        [HttpPost]
        [Route("v1/podcasts/")]
        public IActionResult Post([FromBody] PodcastCadastroViewModel podcastTemp) {
            if (!ModelState.IsValid) return BadRequest(podcastTemp);
            var podcast = new Podcast() {
                Id = 0,
                Nome = podcastTemp.Nome,
                Descricao = podcastTemp.Descricao,
                Autor = podcastTemp.Autor,
                Imagem = podcastTemp.Imagem,
                Link = podcastTemp.Link,
                CategoriaId = podcastTemp.CategoriaId
            };
            _podcastRepository.Criar(podcast);
            return Ok(podcast);
        }
        
        [HttpPut]
        [Route("v1/podcasts/{id}")]
        public IActionResult Put(int id, [FromBody] PodcastEdicaoViewModel podcastTemp) {
            if (!ModelState.IsValid || podcastTemp.Id != id) return BadRequest(podcastTemp);
            var podcast = new Podcast() {
                Id = podcastTemp.Id,
                Nome = podcastTemp.Nome,
                Descricao = podcastTemp.Descricao,
                Autor = podcastTemp.Autor,
                Imagem = podcastTemp.Imagem,
                Link = podcastTemp.Link,
                CategoriaId = podcastTemp.CategoriaId
            };
            _podcastRepository.Editar(podcast);
            return Ok(podcast);
        }
        
        [HttpDelete]
        [Route("v1/podcasts/{id}")]
        public IActionResult Delete(int id) {
            var podcast = _podcastRepository.Buscar(id);
            _podcastRepository.Remover(podcast);
            return Ok(podcast);
        }
    }
}    
        