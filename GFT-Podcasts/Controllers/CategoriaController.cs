using System;
using System.Collections.Generic;
using System.Linq;
using GFT_Podcasts.Libraries.ExtensionsMethods;
using GFT_Podcasts.Models;
using GFT_Podcasts.Models.ViewModels;
using GFT_Podcasts.Models.ViewModels.CategoriaViewModels;
using GFT_Podcasts.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GFT_Podcasts.Controllers {
    [Route("api/")]
    public class CategoriaController : ControllerBase {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaController(ICategoriaRepository categoriaRepository) {
            _categoriaRepository = categoriaRepository;
        }

        [HttpGet]
        [Route("v1/categorias/{id}")]
        public ObjectResult Get(int id) {
            var categoria = _categoriaRepository.Buscar(id);

            if (categoria == null) {
                return new ObjectResult(new ResultViewModel(false, "Categoria não encontrada!", null));
            }

            return new ObjectResult(new ResultViewModel(true, "Categoria encontrada!", categoria));
        }

        [HttpGet]
        [Route("v1/categorias")]
        public ObjectResult Get() {
            var categorias = _categoriaRepository.Listar();
            return new ObjectResult(new ResultViewModel(true, "Listagem de Categorias!", categorias));
        }

        [HttpPost]
        [Route("v1/categorias/")]
        public ObjectResult Post([FromBody] CategoriaCadastroViewModel categoriaTemp) {
            if (!ModelState.IsValid)
                return new ObjectResult(new ResultViewModel(false, "Erro ao cadastrar categoria.",
                    ModelState.ListarErros()));
            var categoria = new Categoria() {
                Id = 0,
                Nome = categoriaTemp.Nome
            };
            _categoriaRepository.Criar(categoria);
            return new ObjectResult(new ResultViewModel(true, "Categoria cadastrada com sucesso!", categoria));
        }

        [HttpPut]
        [Route("v1/categorias/{id}")]
        public ObjectResult Put(int id, [FromBody] CategoriaEdicaoViewModel categoriaTemp) {
            if (id != categoriaTemp.Id) {
                ModelState.AddModelError("Id", "Id da requisição difere do Id da categoria.");
            }
            
            if (!ModelState.IsValid)
                return new ObjectResult(new ResultViewModel(false, "Erro ao editar categoria.",
                    ModelState.ListarErros()));
            var categoria = new Categoria() {
                Id = categoriaTemp.Id,
                Nome = categoriaTemp.Nome
            };
            _categoriaRepository.Editar(categoria);
            return new ObjectResult(new ResultViewModel(true, "Categoria editada com sucesso!", categoria));
        }

        [HttpDelete]
        [Route("v1/categorias/{id}")]
        public ObjectResult Delete(int id) {
            var categoria = _categoriaRepository.Buscar(id);
            _categoriaRepository.Remover(categoria);
            return new ObjectResult(new ResultViewModel(true, "Categoria excluída com sucesso!", categoria));
        }
    }
}