using Microsoft.AspNetCore.Mvc;
using cifraspp_API.Data;
using cifraspp_API.Models;

namespace cifraspp_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaCantorFavoritoController : ControllerBase
    {
        // contexto que sera utilizado
        private readonly CifrasContext _context;

        public ContaCantorFavoritoController(CifrasContext context)
        {
            _context = context;
        }

        [HttpGet] 
        public ActionResult<List<ContaCantorFavorito>> GetAll()
        {
            return _context.ContaCantorFavorito.ToList();
        }

        [HttpGet("{idCantorFavorito}")]
        public ActionResult<List<ContaCantorFavorito>> Get(int idCantorFavorito)
        {
            try
            {
                var result = _context.ContaCantorFavorito.Find(idCantorFavorito);
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
        public async Task<ActionResult> post(ContaCantorFavorito model)
        {

                int idCantor = model.idCantor;
                int idConta = model.idConta;

                var resultCantor = await _context.Cantor.FindAsync(idCantor);
                var resultConta = await _context.Conta.FindAsync(idConta);

                if (resultCantor == null)
                {
                    return NotFound("Cifra não encontrada.");
                }
                else if (resultConta == null)
                {
                    return NotFound("Conta não encontrada.");
                }

            try
            {

                _context.ContaCantorFavorito.Add(model);
                if (await _context.SaveChangesAsync() == 1)
                {
                    return Created($"/api/ContaCantorFavorito/{model.idCantorFavorito}", model);
                }
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acesso do Banco de dados");
            }
            return BadRequest(); // se não conseguir incluir, retorna BadRequest
        }

        [HttpDelete("{idCantorFavorito}")] // exclusão de ContaCantorFavorito
        public async Task<ActionResult> delete(int idCantorFavorito)
        {
            try
            {
                var result = _context.ContaCantorFavorito.Find(idCantorFavorito);
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

        [HttpPut("{idCantorFavorito}")]
        public async Task<IActionResult> put(int idCantorFavorito, ContaCantorFavorito dadosContaCantorFavoritoAlt)
        {
            try
            {
                //verifica se existe aluno a ser alterado
                var result = await _context.ContaCantorFavorito.FindAsync(idCantorFavorito);
                if (idCantorFavorito != result.idCantorFavorito)
                {
                    return NotFound();
                }
                result.idCantor = dadosContaCantorFavoritoAlt.idCantor;
                result.idConta = dadosContaCantorFavoritoAlt.idConta;
                await _context.SaveChangesAsync();
                return Created($"/api/ContaCantorFavorito/{dadosContaCantorFavoritoAlt.idCantorFavorito}", dadosContaCantorFavoritoAlt);
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acesso do Banco de dados");
            }

        }


    }
}