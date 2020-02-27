using System;
using System.Collections.Generic;
using System.Linq;
using GFT_Podcasts.Models;
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
        public Categoria Get(int id) {
            return _categoriaRepository.Buscar(id);
        }

        [HttpGet]
        [Route("v1/categorias")]
        public IEnumerable<CategoriaListagemViewModel> Get() {
            return _categoriaRepository.Listar();
        }

        [HttpPost]
        [Route("v1/categorias/")]
        public IActionResult Post([FromBody] CategoriaCadastroViewModel categoriaTemp) {
            if (ModelState.IsValid) {
                var categoria = new Categoria() {
                    Id = 0,
                    Nome = categoriaTemp.Nome
                };
                _categoriaRepository.Criar(categoria);
                return Ok(categoria);
            }

            return BadRequest(categoriaTemp);
        }

        [HttpPut]
        [Route("v1/categorias/{id}")]
        public IActionResult Put(int id, [FromBody] CategoriaEdicaoViewModel categoriaTemp) {
            if (ModelState.IsValid && id == categoriaTemp.Id) {
                var categoria = new Categoria() {
                    Id = categoriaTemp.Id,
                    Nome = categoriaTemp.Nome
                };
                _categoriaRepository.Editar(categoria);
                return Ok(categoria);
            }

            return BadRequest(categoriaTemp);
        }

        [HttpDelete]
        [Route("v1/categorias/{id}")]
        public IActionResult Delete(int id) {
            var categoria = _categoriaRepository.Buscar(id);
            _categoriaRepository.Remover(categoria);
            return Ok(categoria);
        }
    }
}