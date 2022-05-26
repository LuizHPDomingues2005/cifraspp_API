using Microsoft.AspNetCore.Mvc;

namespace cifraspp_API.Controllers
{
    [ApiController]
        [Route("/")]
    public class HomeController
    {
        [HttpGet]
        public IActionResult Inicio()
        {
            return new ContentResult
            {
                ContentType = "text/html",
                Content = "<h1>Funciona!!!!</h1>"
            };
        }
        
    }
}