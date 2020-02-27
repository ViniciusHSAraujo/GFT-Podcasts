using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GFT_Podcasts.Libraries.ExtensionsMethods;
using GFT_Podcasts.Models;
using GFT_Podcasts.Models.ViewModels;
using GFT_Podcasts.Models.ViewModels.EpisodioViewModels;
using GFT_Podcasts.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GFT_Podcasts.Controllers {
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
                return new ObjectResult(new ResultViewModel
                    (false, "Podcast não encontrado!", null));
            }

            return new ObjectResult(new ResultViewModel
                (true, "Episódio encontrado com sucesso!", episodio));
        }

        [HttpGet]
        [Route("v1/episodios")]
        public ObjectResult Get() {
            var episodios = _episodioRepository.Listar();
            return !episodios.Any()
                ? new ObjectResult
                    (new ResultViewModel(false, "Nenhum episódio encontrado", episodios))
                : new ObjectResult
                    (new ResultViewModel(true, "Listagem de Episódios!", episodios));
        }

        [HttpPost]
        [Route("v1/episodios/")]
        public ObjectResult Post([FromBody] EpisodioCadastroViewModel episodioTemp) {
            if (!_podcastRepository.Existe(episodioTemp.PodcastId)) {
                ModelState.AddModelError("PodcastId", "Podcast inexistente.");
            }

            if (!ModelState.IsValid)
                return new ObjectResult(
                    new ResultViewModel(false, "Erro ao cadastrar o episódio", ModelState.ListarErros()));
            var episodio = new Episodio() {
                Id = 0,
                Descricao = episodioTemp.Descricao,
                Duracao = episodioTemp.Duracao,
                Lancamento = episodioTemp.Lancamento,
                PodcastId = episodioTemp.PodcastId,
                Titulo = episodioTemp.Titulo,
                LinkAudio = episodioTemp.LinkAudio
            };


            _episodioRepository.Criar(episodio);
            return new ObjectResult(
                new ResultViewModel(true, "Episódio cadastrado com sucesso!", episodio));
        }

        [HttpPut]
        [Route("v1/episodios/{id}")]
        public ObjectResult Put(int id, [FromBody] EpisodioEdicaoViewModel episodioTemp) {
            if (id != episodioTemp.Id) {
                ModelState.AddModelError("Id", "Id da requisição difere do Id do episódio.");
            }

            if (!_podcastRepository.Existe(episodioTemp.PodcastId)) {
                ModelState.AddModelError("PodcastId", "Podcast inexistente.");
            }

            if (!ModelState.IsValid)
                return new ObjectResult(
                    new ResultViewModel(false, "Erro ao editar o episódio", ModelState.ListarErros()));
            var episodio = new Episodio() {
                Id = episodioTemp.Id,
                Descricao = episodioTemp.Descricao,
                Duracao = episodioTemp.Duracao,
                Lancamento = episodioTemp.Lancamento,
                PodcastId = episodioTemp.PodcastId,
                Titulo = episodioTemp.Titulo,
                LinkAudio = episodioTemp.LinkAudio
            };
            _episodioRepository.Editar(episodio);
            return new ObjectResult(
                new ResultViewModel(true, "Episódio editado com sucesso!", episodio));
        }

        [HttpDelete]
        [Route("v1/episodios/{id}")]
        public ObjectResult Delete(int id) {
            var episodio = _episodioRepository.Buscar(id);
            _episodioRepository.Remover(episodio);
            return new ObjectResult(
                new ResultViewModel(true, "Episódio excluido com sucesso!", episodio));
        }
    }
}