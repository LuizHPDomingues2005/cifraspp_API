using Microsoft.AspNetCore.Mvc;
using cifraspp_API.Data;
using cifraspp_API.Models;

namespace cifraspp_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaComentarioController : ControllerBase
    {
        // contexto que sera utilizado
        private readonly CifrasContext _context;

        public ContaComentarioController(CifrasContext context)
        {
            _context = context;
        }

        [HttpGet] 
        public ActionResult<List<ContaComentario>> GetAll()
        {
            return _context.ContaComentario.ToList();
        }

        [HttpGet("{idContaComentario}")]
        public ActionResult<List<ContaComentario>> Get(int idContaComentario)
        {
            try
            {
                var result = _context.ContaComentario.Find(idContaComentario);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acesso do Banco de dados");
            }
        }

        [HttpPost]
        public async Task<ActionResult> post(ContaComentario model)
        {

                int idComentario = model.idComentario;
                int idConta = model.idConta;

                var resultComentario = await _context.Comentario.FindAsync(idComentario);
                var resultConta = await _context.Conta.FindAsync(idConta);

                if (resultComentario == null)
                {
                    return NotFound("Comentario não encontrado.");
                }
                else if (resultConta == null)
                {
                    return NotFound("Conta não encontrada.");
                }

            try
            {

                _context.ContaComentario.Add(model);
                if (await _context.SaveChangesAsync() == 1)
                {
                    return Created($"/api/ContaComentario/{model.idContaComentario}", model);
                }
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acesso do Banco de dados");
            }
            return BadRequest(); // se não conseguir incluir, retorna BadRequest
        }

        [HttpDelete("{idContaComentario}")] // exclusão de ContaComentario
        public async Task<ActionResult> delete(int idContaComentario)
        {
            try
            {
                var result = _context.ContaComentario.Find(idContaComentario);
                if (result == null)
                {
                    return NotFound();
                }
                _context.Remove(result);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acesso do Banco de dados");
            }
        }

        [HttpPut("{idContaComentario}")]
        public async Task<IActionResult> put(int idContaComentario, ContaComentario dadosContaComentarioAlt)
        {
            try
            {
                //verifica se existe aluno a ser alterado
                var result = await _context.ContaComentario.FindAsync(idContaComentario);
                if (idContaComentario != result.idContaComentario)
                {
                    return NotFound();
                }
                result.idComentario = dadosContaComentarioAlt.idComentario;
                result.idConta = dadosContaComentarioAlt.idConta;
                await _context.SaveChangesAsync();
                return Created($"/api/ContaComentario/{dadosContaComentarioAlt.idContaComentario}", dadosContaComentarioAlt);
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acesso do Banco de dados");
            }

        }


    }
}