using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using XepaCommerce.src.dtos;
using XepaCommerce.src.modelos;
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

        /// <summary>
        /// Pegar usuario pelo Id
        /// </summary>
        /// <param name="idUsuario">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna o usuario</response>
        /// <response code="404">Usuario não existente</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UsuarioModelo))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("id/{idUsuario}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult> PegarUsuarioPeloIdAsync([FromRoute] int idUsuario)
        {
            var usuario = await _repositorio.PegarUsuarioPeloIdAsync(idUsuario);

            if (usuario == null) return NotFound();

            return Ok(usuario);
        }


        /// <summary>
        /// Pegar usuario pelo Nome
        /// </summary>
        /// <param name="nomeUsuario">string</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna o usuario</response>
        /// <response code="204">Nome não existe</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UsuarioModelo))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        [Authorize(Roles = "NORMAL,ADMINISTRADOR")]
        [HttpGet]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult> PegarUsuariosPeloNomeAsync([FromQuery] string nomeUsuario)
        {
            var usuarios = await _repositorio.PegarUsuariosPeloNomeAsync(nomeUsuario);

            if (usuarios.Count < 1) return NoContent();

            return Ok(usuarios);
        }


        /// <summary>
        /// Pegar usuario pelo Email
        /// </summary>
        /// <param name="emailUsuario">string</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna o usuario</response>
        /// <response code="404">Email não existente</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UsuarioModelo))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("email/{emailUsuario}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult> PegarUsuarioPeloEmailAsync([FromRoute] string emailUsuario)
        {
            var usuario = await _repositorio.PegarUsuarioPeloEmailAsync(emailUsuario);

            if (usuario == null) return NotFound();

            return Ok(usuario);
        }


        /// <summary>
        /// Criar novo Usuario
        /// </summary>
        /// <param name="usuario">NovoUsuarioDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /api/Usuarios
        ///     {
        ///        "nome": "Ana Paula",
        ///        "email": "AnaPaula@gmail.com",
        ///        "senha": "35626",
        ///        "endereco": "Rua Tamarindo , 71",
        ///        "tipo": "NORMAL"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Retorna usuario criado</response>
        /// <response code="400">Erro na requisição</response>
        /// <response code="401">E-mail ja cadastrado</response>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UsuarioModelo))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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


        /// <summary>
        /// Atualizar Usuario
        /// </summary>
        /// <param name="usuario">AtualizarUsuarioDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     PUT /api/Usuarios
        ///     {
        ///        "id": 1,    
        ///        "nome": "Ana",
        ///        "senha": "78282",
        ///        "endereco": "Rua Guiomar , 456"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Retorna usuario atualizado</response>
        /// <response code="400">Erro na requisição</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
