using System.Collections;
using System.Collections.Generic;
using GFT_Podcasts.Models;
using GFT_Podcasts.Models.ViewModels.EpisodioViewModels;
using GFT_Podcasts.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GFT_Podcasts.Controllers {
    [Route("api/")]
    public class EpisodioController : Controller {
        private readonly IEpisodioRepository _episodioRepository;

        public EpisodioController(IEpisodioRepository episodioRepository) {
            _episodioRepository = episodioRepository;
        }

        [HttpGet]
        [Route("v1/episodios/{id}")]
        public Episodio Get(int id) {
            return _episodioRepository.Buscar(id);
        }

        [HttpGet]
        [Route("v1/episodios")]
        public IEnumerable<EpisodioListagemViewModel> Get() {
            return _episodioRepository.Listar();
        }
        
        [HttpPost]
        [Route("v1/episodios/")]
        public IActionResult Post([FromBody] EpisodioCadastroViewModel episodioTemp) {
            if (!ModelState.IsValid) return BadRequest(episodioTemp);
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
            return Ok(episodio);
        }
        
        [HttpPut]
        [Route("v1/episodios/{id}")]
        public IActionResult Put(int id, [FromBody] EpisodioEdicaoViewModel episodioTemp) {
            if (!ModelState.IsValid || id != episodioTemp.Id) return BadRequest(episodioTemp);
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
            return Ok(episodio);
        }
        
        [HttpDelete]
        [Route("v1/episodios/{id}")]
        public IActionResult Delete(int id) {
            var episodio = _episodioRepository.Buscar(id);
            _episodioRepository.Remover(episodio);
            return Ok(episodio);
        }
    }
}