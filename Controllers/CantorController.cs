using Microsoft.AspNetCore.Mvc;
using cifraspp_API.Data;
using cifraspp_API.Models;


namespace cifraspp_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CantorController : ControllerBase
    {
        private readonly CifrasContext _context;

        public CantorController(CifrasContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Cantor>> GetAll()
        {
            return _context.Cantor.ToList();
        }

        [HttpGet("{idCantor}")]
        public ActionResult<List<Cantor>> Get(int idCantor)
        {
            try
            {
                var result = _context.Cantor.Find(idCantor);
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

        [HttpPost] // criação de um cantor
        public async Task<ActionResult> post(Cantor model)
        {
            try
            {
                _context.Cantor.Add(model);
                if (await _context.SaveChangesAsync() == 1)
                {
                    return Created($"/api/conta/{model.idCantor}", model);
                }
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acesso do Banco de dados");
            }
            return BadRequest(); // se não conseguir incluir, retorna BadRequest
        }



        [HttpDelete("{idCantor}")] // exclusão de cantor
        public async Task<ActionResult> delete(int idCantor)
        {
            try
            {
                var result = _context.Cantor.Find(idCantor);
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

        [HttpPut("{idCantor}")]
        public async Task<IActionResult> put(int idCantor, Cantor dadosCantorAlt)
        {
            try
            {
                //verifica se existe aluno a ser alterado
                var result = await _context.Cantor.FindAsync(idCantor);
                if (idCantor != result.idCantor)
                {
                    return NotFound();
                }
                result.nomeCantor = dadosCantorAlt.nomeCantor;
                result.qtdDeCifras = dadosCantorAlt.qtdDeCifras;
                await _context.SaveChangesAsync();
                return Created($"/api/conta/{dadosCantorAlt.idCantor}", dadosCantorAlt);
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acesso do Banco de dados");
            }

        }



    }
}