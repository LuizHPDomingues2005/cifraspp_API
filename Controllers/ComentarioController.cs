using Microsoft.AspNetCore.Mvc;
using cifraspp_API.Data;
using cifraspp_API.Models;

namespace cifraspp_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComentarioController : ControllerBase
    {
        private readonly CifrasContext _context;

        public ComentarioController(CifrasContext context)
        {
            _context = context;
        }




        [HttpGet]
        public ActionResult<List<Comentario>> GetAll()
        {
            return _context.Comentario.ToList();
        }


        [HttpGet("{idComentario}")]
        public ActionResult<List<Comentario>> Get(int idComentario)
        {
            try
            {
                var result = _context.Comentario.Find(idComentario);
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



        [HttpPost] // criação de um comentario
        public async Task<ActionResult> post(Comentario model)
        {
            try
            {
                _context.Comentario.Add(model);
                if (await _context.SaveChangesAsync() == 1)
                {
                    return Created($"/api/conta/{model.idComentario}", model);
                }
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acesso do Banco de dados");
            }
            return BadRequest(); // se não conseguir incluir, retorna BadRequest
        }



        [HttpDelete("{idComentario}")] // exclusão de comentario
        public async Task<ActionResult> delete(int idComentario)
        {
            try
            {
                var result = _context.Comentario.Find(idComentario);
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



        [HttpPut("{idComentario}")]
        public async Task<IActionResult> put(int idComentario, Comentario dadosComentarioAlt)
        {
            try
            {
                //verifica se existe aluno a ser alterado
                var result = await _context.Comentario.FindAsync(idComentario);
                if (idComentario != result.idComentario)
                {
                    return NotFound();
                }
                result.mensagem = dadosComentarioAlt.mensagem;
                await _context.SaveChangesAsync();
                return Created($"/api/conta/{dadosComentarioAlt.idComentario}", dadosComentarioAlt);
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acesso do Banco de dados");
            }

        }



    }
}