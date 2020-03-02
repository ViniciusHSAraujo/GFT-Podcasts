using System.Linq;
using GFT_Podcasts.Libraries.ExtensionsMethods;
using GFT_Podcasts.Models;
using GFT_Podcasts.Models.ViewModels;
using GFT_Podcasts.Models.ViewModels.PodcastViewModels;
using GFT_Podcasts.Repositories.Interfaces;
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
            return !podcasts.Any()
                ? new ObjectResult
                    (new ResultViewModel(false, "Nenhum podcast encontrado.", podcasts))
                : new ObjectResult
                    (new ResultViewModel(true, "Listagem de Podcasts!", podcasts));
        }
        
        [HttpPost]
        [Route("v1/podcasts/")]
        public ObjectResult Post([FromBody] PodcastCadastroViewModel podcastTemp) {
            if (!_categoriaRepository.Existe(podcastTemp.CategoriaId)) {
                ModelState.AddModelError("CategoriaId", "Categoria inexistente.");
            }
            if (!ModelState.IsValid) {
                return new ObjectResult(new ResultViewModel(false, "Erro ao cadastrar podcast.",
                    ModelState.ListarErros()));
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
            return new ObjectResult(new ResultViewModel(true, "Podcast cadastrado com sucesso!", 
                podcastTemp));
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
                podcastTemp));
        }
        
        [HttpDelete]
        [Route("v1/podcasts/{id}")]
        public ObjectResult Delete(int id) {
            var podcast = _podcastRepository.Buscar(id);
            if (podcast == null) {
                return new ObjectResult(new ResultViewModel(false, "Podcast inexistente.", null));
            }
            _podcastRepository.Remover(podcast);
            return new ObjectResult(new ResultViewModel(true, "Podcast excluído com sucesso!", 
                podcast));

        }
    }
}    
        