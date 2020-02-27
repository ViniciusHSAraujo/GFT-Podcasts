using System;
using System.Collections.Generic;
using GFT_Podcasts.Libraries.ExtensionsMethods;
using GFT_Podcasts.Models;
using GFT_Podcasts.Models.ViewModels;
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
        public ObjectResult Get(int id) {
            var podcast = _podcastRepository.Buscar(id);
            
            if (podcast == null) {
                return new ObjectResult(new ResultViewModel(false, "Podcast não encontrado!", null));
            }
            
            var podcastSimplificado = new PodcastSimplificadoViewModel() {
                Id = podcast.Id,
                Nome = podcast.Nome,
                Autor = podcast.Autor,
                Descricao = podcast.Descricao,
                Link = podcast.Link,
                Imagem = podcast.Imagem
            };
            
            return new ObjectResult(new ResultViewModel(true, "Podcast encontrado com sucesso!", podcastSimplificado));
        }

        [HttpGet]
        [Route("v1/podcasts")]
        public ObjectResult Get() {
            var podcasts = _podcastRepository.Listar();
            return new ObjectResult(new ResultViewModel(true, "Listagem de Podcasts!", podcasts));
        }
        
        [HttpPost]
        [Route("v1/podcasts/")]
        public ObjectResult Post([FromBody] PodcastCadastroViewModel podcastTemp) {
            if (!ModelState.IsValid)
                return new ObjectResult(new ResultViewModel(false, "Erro ao cadastrar podcast.",
                    ModelState.ListarErros()));
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
            return new ObjectResult(new ResultViewModel(true, "Podcast cadastrado com sucesso!", 
                podcast));
        }
        
        [HttpPut]
        [Route("v1/podcasts/{id}")]
        public ObjectResult Put(int id, [FromBody] PodcastEdicaoViewModel podcastTemp) {
            if (id != podcastTemp.Id) {
                ModelState.AddModelError("Id", "Id da requisição difere do Id da categoria.");
            }
            
            if (!ModelState.IsValid)
                return new ObjectResult(new ResultViewModel(false, "Erro ao editar categoria.",
                    ModelState.ListarErros()));
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
            return new ObjectResult(new ResultViewModel(true, "Podcast editado com sucesso!", 
                podcast));
        }
        
        [HttpDelete]
        [Route("v1/podcasts/{id}")]
        public ObjectResult Delete(int id) {
            var podcast = _podcastRepository.Buscar(id);
            _podcastRepository.Remover(podcast);
            return new ObjectResult(new ResultViewModel(true, "Podcast excluído com sucesso!", 
                podcast));
        }
    }
}    
        