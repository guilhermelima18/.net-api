using CursoAPI.Business.Entities;
using CursoAPI.Business.Repositories;
using CursoAPI.Models;
using CursoAPI.Models.Cursos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace CursoAPI.Controllers
{
    [Route("api/v1/cursos")]
    [ApiController]
    [Authorize]
    public class CursoController : ControllerBase
    {
        private readonly ICursoRepository _cursoRepository;

        public CursoController(ICursoRepository cursoRepository)
        {
            _cursoRepository = cursoRepository;
        }

        [SwaggerResponse(statusCode: 201, description: "Sucesso ao cadastrar um curso.", Type = typeof(CursoViewModelInput))]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado.", Type = typeof(ValidaCampoViewModelOutput))]

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Post(CursoViewModelInput cursoViewModelInput)
        {
            Curso curso = new Curso();
            curso.Nome = cursoViewModelInput.Nome;
            curso.Descricao = cursoViewModelInput.Descricao;

            var codigoUsuario = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            curso.CodigoUsuario = codigoUsuario;

            _cursoRepository.Adicionar(curso);
            _cursoRepository.Commit();

            return Created("", cursoViewModelInput);
        }

        [SwaggerResponse(statusCode: 200, description: "Sucesso ao obter os cursos.", Type = typeof(CursoViewModelOutput))]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado.", Type = typeof(ValidaCampoViewModelOutput))]

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get()
        {
            var codigoUsuario = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

            var cursos = _cursoRepository.ObterPorUsuario(codigoUsuario).Select(s => new CursoViewModelOutput()
            {
                Nome = s.Nome,
                Descricao = s.Descricao,
                Login = s.Usuario.Login
            });

            return Ok(cursos);
        }
    }
}
