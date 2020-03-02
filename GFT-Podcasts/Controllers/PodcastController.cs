using System;
using System.Linq;
using GFT_Podcasts.Libraries.ExtensionsMethods;
using GFT_Podcasts.Libraries.Utils;
using GFT_Podcasts.Models;
using GFT_Podcasts.Models.ViewModels.PodcastViewModels;
using GFT_Podcasts.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GFT_Podcasts.Controllers
{
    [Route("api/")]
    public class PodcastController : ControllerBase {
        private readonly IPodcastRepository _podcastRepository;

        private readonly ICategoriaRepository _categoriaRepository;
        
        public PodcastController(IPodcastRepository podcastRepository, ICategoriaRepository categoriaRepository) {
            _podcastRepository = podcastRepository;
            _categoriaRepository= categoriaRepository;
        }
        

        [HttpGet]
        [Route("v1/podcasts/{id}")]
        public ObjectResult Get(int id) {
            var podcast = _podcastRepository.Buscar(id);
            
            if (podcast == null) {
                return ResponseUtils.GenerateObjectResult("Podcast não encontrado!", null);
            }
            
            var podcastSimplificado = new PodcastDetalhadoViewModel() {
                Id = podcast.Id,
                Nome = podcast.Nome,
                Autor = podcast.Autor,
                Descricao = podcast.Descricao,
                Link = podcast.Link,
                Imagem = podcast.Imagem,
                Episodios = podcast.Episodios
            };
            
            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Podcast encontrado com sucesso!", podcastSimplificado);
        }

        [HttpGet]
        [Route("v1/podcasts")]
        public ObjectResult Get() {
            var podcasts = _podcastRepository.Listar();

            if(!podcasts.Any()){
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Nenhum podcast encontrado.", podcasts);
            }

            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Listagem de Podcasts!", podcasts);
        }
        
        [HttpPost]
        [Route("v1/podcasts/")]
        public ObjectResult Post([FromBody] PodcastCadastroViewModel podcastTemp) {
            if (!_categoriaRepository.Existe(podcastTemp.CategoriaId)) {
                ModelState.AddModelError("CategoriaId", "Categoria inexistente.");
            }
            if (!ModelState.IsValid) {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return ResponseUtils.GenerateObjectResult("Erro ao cadastrar podcast.",
                    ModelState.ListarErros());
            }
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
            Response.StatusCode = StatusCodes.Status201Created;
            return ResponseUtils.GenerateObjectResult("Podcast cadastrado com sucesso!", 
                podcastTemp);
        }
        
        [HttpPut]
        [Route("v1/podcasts/{id}")]
        public ObjectResult Put(int id, [FromBody] PodcastEdicaoViewModel podcastTemp) {
            if (id != podcastTemp.Id) {
                ModelState.AddModelError("Id", "Id da requisição difere do Id da categoria.");
            }
            if (!_podcastRepository.Existe(podcastTemp.Id)) {
                ModelState.AddModelError("Id", "Podcast inexistente.");
            }
            if (!_categoriaRepository.Existe(podcastTemp.CategoriaId)) {
                ModelState.AddModelError("CategoriaId", "Categoria inexistente.");
            }
            if (!ModelState.IsValid) {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return ResponseUtils.GenerateObjectResult("Erro ao editar categoria.",
                    ModelState.ListarErros());
            }
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
            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Podcast editado com sucesso!", 
                podcastTemp);
        }
        
        [HttpDelete]
        [Route("v1/podcasts/{id}")]
        public ObjectResult Delete(int id) {
            var podcast = _podcastRepository.Buscar(id);
            if (podcast == null) {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Podcast inexistente.", null);
            }
            try {
                _podcastRepository.Remover(podcast);
                Response.StatusCode = StatusCodes.Status200OK;
                return ResponseUtils.GenerateObjectResult("Podcast excluído com sucesso!", podcast);
            } catch (Exception) {
                Response.StatusCode = StatusCodes.Status406NotAcceptable;
                return ResponseUtils.GenerateObjectResult("Não foi possível excluir o podcast, contate o suporte!", podcast);
            }
        }
    }
}    
        