using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using XepaCommerce.src.dtos;
using XepaCommerce.src.repositorios;
using XepaCommerce.src.servicos;

namespace XepaCommerce.src.Controladores
{
    [ApiController]
    [Route("api/Usuarios")]
    [Produces("application/json")]
    public class UsuarioControlador : ControllerBase
    {
        #region Atributos

        private readonly IUsuario _repositorio;
        public readonly IAutenticacao _servicos;

        #endregion


        #region Construtores

        public UsuarioControlador(IUsuario repositorio, IAutenticacao servico)
        {
            _repositorio = repositorio;
            _servicos = servico;
        }

        #endregion


        #region Métodos

        [HttpGet("id/{idUsuario}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult> PegarUsuarioPeloIdAsync([FromRoute] int idUsuario)
        {
            var usuario = await _repositorio.PegarUsuarioPeloIdAsync(idUsuario);

            if (usuario == null) return NotFound();

            return Ok(usuario);
        }

        [HttpGet]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult> PegarUsuariosPeloNomeAsync([FromQuery] string nomeUsuario)
        {
            var usuarios = await _repositorio.PegarUsuariosPeloNomeAsync(nomeUsuario);

            if (usuarios.Count < 1) return NoContent();

            return Ok(usuarios);
        }

        [HttpGet("email/{emailUsuario}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult> PegarUsuarioPeloEmailAsync([FromRoute] string emailUsuario)
        {
            var usuario = await _repositorio.PegarUsuarioPeloEmailAsync(emailUsuario);

            if (usuario == null) return NotFound();

            return Ok(usuario);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> NovoUsuarioAsync([FromBody] NovoUsuarioDTO usuario)
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                await _servicos.CriarUsuarioSemDuplicarAsync(usuario);

                return Created($"api/Usuarios/email/{usuario.Email}", usuario);
            }
            catch (Exception ext)
            {
                return Unauthorized(ext.Message);

            }
        }

        [HttpPut]
        [Authorize(Roles = "NORMAL,ADMINISTRADOR")]
        public async Task<ActionResult> AtualizarUsuarioAsync([FromBody] AtualizarUsuarioDTO usuario)
        {
            if (!ModelState.IsValid) return BadRequest();

            usuario.Senha = _servicos.CodificarSenha(usuario.Senha);

            await _repositorio.AtualizarUsuarioAsync(usuario);
            return Ok(usuario);
        }

        #endregion


    }
}
