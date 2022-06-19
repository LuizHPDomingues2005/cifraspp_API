using Microsoft.AspNetCore.Mvc;
using cifraspp_API.Data;
using cifraspp_API.Models;

namespace cifraspp_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaCifraFavoritaController : ControllerBase
    {
        // contexto que sera utilizado
        private readonly CifrasContext _context;

        public ContaCifraFavoritaController(CifrasContext context)
        {
            _context = context;
        }

        [HttpGet] 
        public ActionResult<List<ContaCifraFavorita>> GetAll()
        {
            return _context.ContaCifraFavorita.ToList();
        }

        [HttpGet("{idCifraFavorita}")]
        public ActionResult<List<ContaCifraFavorita>> Get(int idCifraFavorita)
        {
            try
            {
                var result = _context.ContaCifraFavorita.Find(idCifraFavorita);
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
        public async Task<ActionResult> post(ContaCifraFavorita model)
        {

                int idCifra = model.idCifra;
                int idConta = model.idConta;

                var resultCifra = await _context.Cifra.FindAsync(idCifra);
                var resultConta = await _context.Conta.FindAsync(idConta);

                if (resultCifra == null)
                {
                    return NotFound("Cifra não encontrada.");
                }
                else if (resultConta == null)
                {
                    return NotFound("Conta não encontrada.");
                }

            try
            {

                _context.ContaCifraFavorita.Add(model);
                if (await _context.SaveChangesAsync() == 1)
                {
                    return Created($"/api/ContaCifraFavorita/{model.idCifraFavorita}", model);
                }
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acesso do Banco de dados");
            }
            return BadRequest(); // se não conseguir incluir, retorna BadRequest
        }

        [HttpDelete("{idCifraFavorita}")] // exclusão de ContaCifraFavorita
        public async Task<ActionResult> delete(int idCifraFavorita)
        {
            try
            {
                var result = _context.ContaCifraFavorita.Find(idCifraFavorita);
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

        [HttpPut("{idCifraFavorita}")]
        public async Task<IActionResult> put(int idCifraFavorita, ContaCifraFavorita dadosContaCifraFavoritaAlt)
        {
            try
            {
                //verifica se existe aluno a ser alterado
                var result = await _context.ContaCifraFavorita.FindAsync(idCifraFavorita);
                if (idCifraFavorita != result.idCifraFavorita)
                {
                    return NotFound();
                }
                result.idCifra = dadosContaCifraFavoritaAlt.idCifra;
                result.idConta = dadosContaCifraFavoritaAlt.idConta;
                await _context.SaveChangesAsync();
                return Created($"/api/ContaCifraFavorita/{dadosContaCifraFavoritaAlt.idCifraFavorita}", dadosContaCifraFavoritaAlt);
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acesso do Banco de dados");
            }

        }


    }
}