using System;
using System.Linq;
using GFT_Podcasts.Libraries.ExtensionsMethods;
using GFT_Podcasts.Libraries.Utils;
using GFT_Podcasts.Models;
using GFT_Podcasts.Models.ViewModels;
using GFT_Podcasts.Models.ViewModels.EpisodioViewModels;
using GFT_Podcasts.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GFT_Podcasts.Controllers
{
    [Route("api/")]
    public class EpisodioController : Controller {
        private readonly IEpisodioRepository _episodioRepository;
        private readonly IPodcastRepository _podcastRepository;

        public EpisodioController(IEpisodioRepository episodioRepository, IPodcastRepository podcastRepository) {
            _episodioRepository = episodioRepository;
            _podcastRepository = podcastRepository;
        }

        [HttpGet]
        [Route("v1/episodios/{id}")]
        public ObjectResult Get(int id) {
            var episodio = _episodioRepository.Buscar(id);

            if (episodio == null) {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Episódio não encontrado!", null);
            }

            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Episódio encontrado com sucesso!", episodio);
        }

        [HttpPost]
        [Route("v1/episodios/")]
        public ObjectResult Post([FromBody] EpisodioCadastroViewModel episodioTemp) {
            if (!_podcastRepository.Existe(episodioTemp.PodcastId)) {
                ModelState.AddModelError("PodcastId", "Podcast inexistente.");
            }

            if (!ModelState.IsValid){
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return ResponseUtils.GenerateObjectResult("Erro ao cadastrar o episódio", ModelState.ListarErros());
            }
            var episodio = new Episodio() {
                Id = 0,
                Descricao = episodioTemp.Descricao,
                Duracao = episodioTemp.Duracao,
                Lancamento = episodioTemp.Lancamento,
                PodcastId = episodioTemp.PodcastId,
                Titulo = episodioTemp.Titulo,
                LinkAudio = episodioTemp.LinkAudio
            };

            Response.StatusCode = StatusCodes.Status200OK;
            _episodioRepository.Criar(episodio);
            return ResponseUtils.GenerateObjectResult("Episódio cadastrado com sucesso!", episodio);
        }

        [HttpPut]
        [Route("v1/episodios/{id}")]
        public ObjectResult Put(int id, [FromBody] EpisodioEdicaoViewModel episodioTemp) {
            if (id != episodioTemp.Id) {
                ModelState.AddModelError("Id", "Id da requisição difere do Id do episódio.");
            }
            if (!_podcastRepository.Existe(episodioTemp.Id)) {
                ModelState.AddModelError("Id", "Episódio inexistente.");
            }
            if (!_podcastRepository.Existe(episodioTemp.PodcastId)) {
                ModelState.AddModelError("PodcastId", "Podcast inexistente.");
            }
            if (!ModelState.IsValid){
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return ResponseUtils.GenerateObjectResult("Erro ao editar o episódio", ModelState.ListarErros());
            }

            var episodio = new Episodio() {
                Id = episodioTemp.Id,
                Descricao = episodioTemp.Descricao,
                Duracao = episodioTemp.Duracao,
                Lancamento = episodioTemp.Lancamento,
                PodcastId = episodioTemp.PodcastId,
                Titulo = episodioTemp.Titulo,
                LinkAudio = episodioTemp.LinkAudio
            };
            
            Response.StatusCode = StatusCodes.Status200OK;
            _episodioRepository.Editar(episodio);
            return ResponseUtils.GenerateObjectResult( "Episódio editado com sucesso!", episodio);
        }

        [HttpDelete]
        [Route("v1/episodios/{id}")]
        public ObjectResult Delete(int id) {
            var episodio = _episodioRepository.Buscar(id);

            if (episodio == null) {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Episódio inexistente.", null);
            }

            try
            {
                Response.StatusCode = StatusCodes.Status200OK;
                _episodioRepository.Remover(episodio);
                return ResponseUtils.GenerateObjectResult("Episódio excluido com sucesso!", episodio);
            }
            catch (Exception)
            {
                Response.StatusCode = StatusCodes.Status406NotAcceptable;
                return ResponseUtils.GenerateObjectResult("Não foi possível excluir o episódio, contate o suporte!", episodio);
            }

            
        }
    }
}