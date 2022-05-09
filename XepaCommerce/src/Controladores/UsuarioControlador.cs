using Microsoft.AspNetCore.Mvc;
using XepaCommerce.src.dtos;
using XepaCommerce.src.repositorios;

namespace XepaCommerce.src.Controladores
{
    [ApiController]
    [Route("api/Usuarios")]
    [Produces("application/json")]
    public class UsuarioControlador : ControllerBase
    {
        #region Atributos

        private readonly IUsuario _repositorio;

        #endregion


        #region Construtores

        public UsuarioControlador(IUsuario repositorio)
        {
            _repositorio = repositorio;
        }

        #endregion


        #region Métodos

        [HttpGet("id/{idUsuario}")]
        public IActionResult PegarUsuarioPeloId([FromRoute] int idUsuario)
        {
            var usuario = _repositorio.PegarUsuarioPeloId(idUsuario);

            if (usuario == null) return NotFound();

            return Ok(usuario);
        }

        [HttpGet]
        public IActionResult PegarUsuariosPeloNome([FromQuery] string nomeUsuario)
        {
            var usuarios = _repositorio.PegarUsuariosPeloNome(nomeUsuario);

            if (usuarios.Count < 1) return NoContent();

            return Ok(usuarios);
        }

        [HttpGet("email/{emailUsuario}")]
        public IActionResult PegarUsuarioPeloEmail([FromRoute] string emailUsuario)
        {
            var usuario = _repositorio.PegarUsuarioPeloEmail(emailUsuario);

            if (usuario == null) return NotFound();

            return Ok(usuario);
        }

        [HttpPost]
        public IActionResult NovoUsuario([FromBody] NovoUsuarioDTO usuario)
        {
            if (!ModelState.IsValid) return BadRequest();

            _repositorio.NovoUsuario(usuario);
            return Created($"api/Usuarios/email/{usuario.Email}", usuario);
        }

        [HttpPut]
        public IActionResult AtualizarUsuario([FromBody] AtualizarUsuarioDTO usuario)
        {
            if (!ModelState.IsValid) return BadRequest();

            _repositorio.AtualizarUsuario(usuario);
            return Ok(usuario);
        }

        #endregion


    }
}
