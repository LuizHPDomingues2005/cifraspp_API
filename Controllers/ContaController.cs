using Microsoft.AspNetCore.Mvc;
using cifraspp_API.Data;
using cifraspp_API.Models;

namespace cifraspp_API.Controllers
{
        [Route("api/[controller]")]
    [ApiController]
    public class ContaController : ControllerBase
    {
        // contexto que sera utilizado
        private CifrasContext _context;

        public ContaController(CifrasContext context)
        {
            context = _context;
        }

        [HttpGet] // get de todas as contas
        public ActionResult<List<Conta>> GetAll()
        {
            return _context.Conta.ToList();
        }

        [HttpGet("{idConta}")] // get de uma conta em específico
        public ActionResult<List<Conta>> Get(int idConta)
        {
            try{
                var result = _context.Conta.Find(idConta);
                if(result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch{
                    return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acesso do Banco de dados");
            }
        }

        [HttpPost] // criação de uma conta
        public async Task<ActionResult> post(Conta model)
        {
            try{
                _context.Add(model);
                if(await _context.SaveChangesAsync() == 1)
                {
                    return Created($"/api/conta/{model.idConta}", model);
                }
            }
            catch{
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acesso do Banco de dados");
            }
            return BadRequest(); // se não conseguir incluir, retorna BadRequest
        }

        [HttpDelete("{idConta}")] // exclusão de conta
        public async Task<ActionResult> delete(int idConta)
        {
            try{
                var result = _context.Conta.Find(idConta);
                if(result == null)
                {
                    return NotFound();
                }
                _context.Remove(result);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch{
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acesso do Banco de dados");
            }
        }

        [HttpPut]
        public async Task<IActionResult> put(int idConta, Conta dadosContaAlt)
        {
            try{
            //verifica se existe aluno a ser alterado
                var result = await _context.Conta.FindAsync(idConta);
                if (idConta != result.idConta)
                {
                    return NotFound();
                }
                result.username = dadosContaAlt.username;
                result.senha    = dadosContaAlt.senha;
                result.email    = dadosContaAlt.email;
                result.qtdComentarios = dadosContaAlt.qtdComentarios;
                await _context.SaveChangesAsync();
                return Created($"/api/conta/{dadosContaAlt.idConta}", dadosContaAlt);
            }
            catch{
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acesso do Banco de dados");
            }

        }


    }
}