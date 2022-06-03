using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using XepaCommerce.src.dtos;
using XepaCommerce.src.servicos;

namespace XepaCommerce.src.Controladores
{
    [ApiController]
    [Route("api/Autenticacao")]
    [Produces("application/json")]
    public class AutenticacaoControlador : ControllerBase
    {

        #region Atributos

        private readonly IAutenticacao _servicos;

        #endregion

        #region Construtores

        public AutenticacaoControlador(IAutenticacao servicos)
        {
            _servicos = servicos;
        }

        #endregion

        #region Métodos
        /// <summary>
        /// Pegar autorização
        /// </summary>
        /// <param name="autenticacao">string</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /api/Autenticacao
        ///     {
        ///        "email": "AnaPaula@gmail.com",
        ///        "senha": "35626"
        ///     }
        ///
        /// </remarks>
        /// <response code="400">Retorna erro na requisição</response>
        /// <response code="200">Retorna autorizado</response>
        /// <response code="401">Retorna não autorizado</response>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AutorizacaoDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Autenticar([FromBody] AutenticarDTO autenticacao)
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                var autorizacao = await _servicos.PegarAutorizacaoAsync(autenticacao);
                return Ok(autorizacao);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }
        #endregion Métodos
    }
}