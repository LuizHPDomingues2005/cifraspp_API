using Microsoft.AspNetCore.Mvc;
using cifraspp_API.Data;
using cifraspp_API.Models;

namespace cifraspp_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CifraController : ControllerBase
    {
        // contexto que sera utilizado
        private readonly CifrasContext _context;

        public CifraController(CifrasContext context)
        {
            _context = context;
        }




        [HttpGet] // get de todas as cifras
        public ActionResult<List<Cifra>> GetAll()
        {
            return _context.Cifra.ToList();
        }

        [ActionName("idCifra")]
        [HttpGet("{idCifra}")] // get de uma cifra em específico
        public ActionResult<List<Cifra>> GetById(int idCifra)
        {
            try
            {
                var result = _context.Cifra.Find(idCifra);
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

        [ActionName("nomeMusica")]
        [HttpGet("{nomeMusica}")] // get de uma conta em específico
        public ActionResult<List<Cifra>> GetByNome(string nomeMusica)
        {
            try
            {
                var result = _context.Cifra.Where(c => c.nomeMusica == nomeMusica);
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


        [HttpPost] // criação de uma cifra
        public async Task<ActionResult> post(Cifra model)
        {
                int idCantor = model.idCantor;
                int idGenero = model.idGenero;

                var resultCantor = await _context.Cantor.FindAsync(idCantor);
                var resultGenero = await _context.Genero.FindAsync(idGenero);

                if (resultCantor == null)
                {
                    return NotFound("Cantor não encontrado.");
                }
                else if (resultGenero == null)
                {
                    return NotFound("Genero não encontrado.");
                }
                model.dataCriada = DateTime.Now;
                model.dataEditada = DateTime.Now;
            try
            {


                    _context.Cifra.Add(model);
                    if (await _context.SaveChangesAsync() == 1)
                    {
                        return Created($"/api/cifra/{model.idCifra}", model);
                    }
                
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acesso do Banco de dados");
            }
            return BadRequest(); // se não conseguir incluir, retorna BadRequest
        }

        [HttpDelete("{idCifra}")] // exclusão de cifra
        public async Task<ActionResult> delete(int idCifra)
        {
            try
            {
                var result = _context.Cifra.Find(idCifra);
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

        [HttpPut("{idCifra}")]
        public async Task<IActionResult> put(int idCifra, Cifra dadosCifraAlt)
        {
            try
            {

                int idCantor = dadosCifraAlt.idCantor;
                int idGenero = dadosCifraAlt.idGenero;


                // verifica se cantor e genero existem

                var resultCantor = await _context.Cantor.FindAsync(idCantor);
                var resultGenero = await _context.Genero.FindAsync(idGenero);

                if (resultCantor == null)
                {
                    return NotFound("Cantor não encontrado.");
                }
                else if (resultGenero == null)
                {
                    return NotFound("Genero não encontrado.");
                }
                else
                {

                    //verifica se existe cifra a ser alterada
                    var result = await _context.Cifra.FindAsync(idCifra);
                    if (idCifra != result.idCifra)
                    {
                        return NotFound();
                    }
                    result.idCantor = dadosCifraAlt.idCantor;
                    result.idGenero = dadosCifraAlt.idGenero;
                    result.nomeMusica = dadosCifraAlt.nomeMusica;
                    result.dataEditada = dadosCifraAlt.dataEditada;
                    result.letraEAcordes = dadosCifraAlt.letraEAcordes;
                    result.linkMusica = dadosCifraAlt.linkMusica;
                    await _context.SaveChangesAsync();
                    return Created($"/api/cifra/{dadosCifraAlt.idCifra}", dadosCifraAlt);
                }
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acesso do Banco de dados");
            }

        }



    }
}