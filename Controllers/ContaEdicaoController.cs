using Microsoft.AspNetCore.Mvc;
using cifraspp_API.Data;
using cifraspp_API.Models;

namespace cifraspp_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaEdicaoController : ControllerBase
    {
        // contexto que sera utilizado
        private readonly CifrasContext _context;

        public ContaEdicaoController(CifrasContext context)
        {
            _context = context;
        }

        [HttpGet] 
        public ActionResult<List<ContaEdicao>> GetAll()
        {
            return _context.ContaEdicao.ToList();
        }

        [HttpGet("{idEdicao}")]
        public ActionResult<List<ContaEdicao>> Get(int idEdicao)
        {
            try
            {
                var result = _context.ContaEdicao.Find(idEdicao);
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
        public async Task<ActionResult> post(ContaEdicao model)
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

                _context.ContaEdicao.Add(model);
                if (await _context.SaveChangesAsync() == 1)
                {
                    return Created($"/api/ContaEdicao/{model.idEdicao}", model);
                }
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acesso do Banco de dados");
            }
            return BadRequest(); // se não conseguir incluir, retorna BadRequest
        }

        [HttpDelete("{idEdicao}")] // exclusão de ContaEdicao
        public async Task<ActionResult> delete(int idEdicao)
        {
            try
            {
                var result = _context.ContaEdicao.Find(idEdicao);
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

        [HttpPut("{idEdicao}")]
        public async Task<IActionResult> put(int idEdicao, ContaEdicao dadosContaCifraFavoritaAlt)
        {
            try
            {
                //verifica se existe aluno a ser alterado
                var result = await _context.ContaEdicao.FindAsync(idEdicao);
                if (idEdicao != result.idEdicao)
                {
                    return NotFound();
                }
                result.idCifra = dadosContaCifraFavoritaAlt.idCifra;
                result.idConta = dadosContaCifraFavoritaAlt.idConta;
                await _context.SaveChangesAsync();
                return Created($"/api/ContaEdicao/{dadosContaCifraFavoritaAlt.idEdicao}", dadosContaCifraFavoritaAlt);
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acesso do Banco de dados");
            }

        }


    }
}