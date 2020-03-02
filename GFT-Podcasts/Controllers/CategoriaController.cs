using System;
using System.Linq;
using System.Net.Mime;
using GFT_Podcasts.Libraries.ExtensionsMethods;
using GFT_Podcasts.Libraries.Utils;
using GFT_Podcasts.Models;
using GFT_Podcasts.Models.ViewModels;
using GFT_Podcasts.Models.ViewModels.CategoriaViewModels;
using GFT_Podcasts.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GFT_Podcasts.Controllers {
    [Route("api/")]
    public class CategoriaController : ControllerBase {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaController(ICategoriaRepository categoriaRepository) {
            _categoriaRepository = categoriaRepository;
        }

        /// <summary>
        /// Retorna a categoria específicada pelo ID.
        /// </summary>
        /// <param name="id">ID da Categoria</param>
        /// <response code="200">Retorna a categorias do ID especificado.</response>
        /// <response code="404">Nenhuma categoria encontrada para esse ID.</response>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [Route("v1/categorias/{id}")]
        public ObjectResult Get(int id) {
            var categoria = _categoriaRepository.Buscar(id);

            if (categoria == null) {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Categoria não encontrada!");
            }

            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Categoria encontrada!", categoria);
        }

        /// <summary>
        /// Retorna todas as categorias cadastradas.
        /// </summary>
        /// <response code="200">Retorna todas as categorias.</response>
        /// <response code="404">Nenhuma categoria encontrada.</response>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("v1/categorias")]
        public ObjectResult Get() {
            var categorias = _categoriaRepository.Listar();

            if (!categorias.Any()) {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Nenhuma categoria encontrada.", categorias);
            }

            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Listagem de Categorias!", categorias);
        }

        /// <summary>
        /// Cadastra uma nova categoria.
        /// </summary>
        /// <response code="201">Retorna a categoria criada.</response>
        /// <response code="400">Requisição inválida, verifique a propriedade "Dado" que conterá os erros de validação.</response>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [Route("v1/categorias/")]
        public ObjectResult Post([FromBody] CategoriaCadastroViewModel categoriaTemp) {
            if (!ModelState.IsValid) {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return ResponseUtils.GenerateObjectResult("Erro ao cadastrar categoria.",
                    ModelState.ListarErros());
            }

            var categoria = new Categoria() {
                Id = 0,
                Nome = categoriaTemp.Nome
            };
            _categoriaRepository.Criar(categoria);
            Response.StatusCode = StatusCodes.Status201Created;
            return ResponseUtils.GenerateObjectResult("Categoria cadastrada com sucesso!", categoria);
        }

        /// <summary>
        /// Edita a categoria específicada pelo ID.
        /// </summary>
        /// <param name="id">ID da Categoria</param> 
        /// <response code="200">Retorna a categoria editada.</response>
        /// <response code="400">Requisição inválida, verifique a propriedade "Dado" que conterá os erros de validação.</response>
        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [Route("v1/categorias/{id}")]
        public ObjectResult Put(int id, [FromBody] CategoriaEdicaoViewModel categoriaTemp) {
            if (id != categoriaTemp.Id) {
                ModelState.AddModelError("Id", "Id da requisição difere do Id da categoria.");
            }

            if (!_categoriaRepository.Existe(categoriaTemp.Id)) {
                ModelState.AddModelError("CategoriaId", "Categoria inexistente.");
            }

            if (!ModelState.IsValid) {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return ResponseUtils.GenerateObjectResult("Erro ao editar categoria.",
                    ModelState.ListarErros());
            }

            var categoria = new Categoria() {
                Id = categoriaTemp.Id,
                Nome = categoriaTemp.Nome
            };
            _categoriaRepository.Editar(categoria);
            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Categoria editada com sucesso!", categoria);
        }

        /// <summary>
        /// Exclui a categoria específicada pelo ID.
        /// </summary>
        /// <param name="id">ID da Categoria</param> 
        /// <response code="200">Retorna a categoria excluída.</response>
        /// <response code="404">Categoria inexistente.</response>
        /// <response code="406">Categoria com relacionamentos que não permitem a exclusão.</response>
        [HttpDelete]
        [Route("v1/categorias/{id}")]
        public ObjectResult Delete(int id) {
            var categoria = _categoriaRepository.Buscar(id);
            if (categoria == null) {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Categoria inexistente.", null);
            }

            try {
                _categoriaRepository.Remover(categoria);
                Response.StatusCode = StatusCodes.Status200OK;
                return ResponseUtils.GenerateObjectResult("Categoria excluída com sucesso!", categoria);
            }
            catch (Exception) {
                Response.StatusCode = StatusCodes.Status406NotAcceptable;
                return ResponseUtils.GenerateObjectResult("Não foi possível excluir a categoria, contate o suporte!",
                    categoria);
            }
        }
    }
}