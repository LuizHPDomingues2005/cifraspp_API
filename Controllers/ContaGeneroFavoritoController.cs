using Microsoft.AspNetCore.Mvc;
using cifraspp_API.Data;
using cifraspp_API.Models;

namespace cifraspp_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaGeneroFavoritoController : ControllerBase
    {
        // contexto que sera utilizado
        private readonly CifrasContext _context;

        public ContaGeneroFavoritoController(CifrasContext context)
        {
            _context = context;
        }

        [HttpGet] 
        public ActionResult<List<ContaGeneroFavorito>> GetAll()
        {
            return _context.ContaGeneroFavorito.ToList();
        }

        [HttpGet("{idGeneroFavorito}")]
        public ActionResult<List<ContaGeneroFavorito>> Get(int idGeneroFavorito)
        {
            try
            {
                var result = _context.ContaGeneroFavorito.Find(idGeneroFavorito);
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
        public async Task<ActionResult> post(ContaGeneroFavorito model)
        {

                int idGenero = model.idGenero;
                int idConta = model.idConta;

                var resultGenero = await _context.Genero.FindAsync(idGenero);
                var resultConta = await _context.Conta.FindAsync(idConta);

                if (resultGenero == null)
                {
                    return NotFound("Genero não encontrado.");
                }
                else if (resultConta == null)
                {
                    return NotFound("Conta não encontrada.");
                }

            try
            {

                _context.ContaGeneroFavorito.Add(model);
                if (await _context.SaveChangesAsync() == 1)
                {
                    return Created($"/api/ContaGeneroFavorito/{model.idGeneroFavorito}", model);
                }
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acesso do Banco de dados");
            }
            return BadRequest(); // se não conseguir incluir, retorna BadRequest
        }

        [HttpDelete("{idGeneroFavorito}")] // exclusão de ContaGeneroFavorito
        public async Task<ActionResult> delete(int idGeneroFavorito)
        {
            try
            {
                var result = _context.ContaGeneroFavorito.Find(idGeneroFavorito);
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

        [HttpPut("{idGeneroFavorito}")]
        public async Task<IActionResult> put(int idGeneroFavorito, ContaGeneroFavorito dadosContaGeneroFavoritoAlt)
        {
            try
            {
                //verifica se existe aluno a ser alterado
                var result = await _context.ContaGeneroFavorito.FindAsync(idGeneroFavorito);
                if (idGeneroFavorito != result.idGeneroFavorito)
                {
                    return NotFound();
                }
                result.idGenero = dadosContaGeneroFavoritoAlt.idGenero;
                result.idConta = dadosContaGeneroFavoritoAlt.idConta;
                await _context.SaveChangesAsync();
                return Created($"/api/ContaGeneroFavorito/{dadosContaGeneroFavoritoAlt.idGeneroFavorito}", dadosContaGeneroFavoritoAlt);
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acesso do Banco de dados");
            }

        }


    }
}