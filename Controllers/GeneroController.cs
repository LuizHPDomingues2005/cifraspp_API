using Microsoft.AspNetCore.Mvc;
using cifraspp_API.Data;
using cifraspp_API.Models;


namespace cifraspp_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class GeneroController : ControllerBase
    {
        private readonly CifrasContext _context;

        public GeneroController(CifrasContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Genero>> GetAll()
        {
            return _context.Genero.ToList();
        }

        [HttpGet("{idGenero}")]
        public ActionResult<List<Genero>> Get(int idGenero)
        {
            try
            {
                var result = _context.Genero.Find(idGenero);
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

        [HttpPost] // criação de um Genero
        public async Task<ActionResult> post(Genero model)
        {
            try
            {
                _context.Genero.Add(model);
                if (await _context.SaveChangesAsync() == 1)
                {
                    return Created($"/api/conta/{model.idGenero}", model);
                }
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acesso do Banco de dados");
            }
            return BadRequest(); // se não conseguir incluir, retorna BadRequest
        }



        [HttpDelete("{idGenero}")] // exclusão de Genero
        public async Task<ActionResult> delete(int idGenero)
        {
            try
            {
                var result = _context.Genero.Find(idGenero);
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

        [HttpPut("{idGenero}")]
        public async Task<IActionResult> put(int idGenero, Genero dadosGeneroAlt)
        {
            try
            {
                //verifica se existe aluno a ser alterado
                var result = await _context.Genero.FindAsync(idGenero);
                if (idGenero != result.idGenero)
                {
                    return NotFound();
                }
                result.nomeGenero = dadosGeneroAlt.nomeGenero;
                result.qtdDeCifras = dadosGeneroAlt.qtdDeCifras;
                await _context.SaveChangesAsync();
                return Created($"/api/conta/{dadosGeneroAlt.idGenero}", dadosGeneroAlt);
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acesso do Banco de dados");
            }

        }



    }
}