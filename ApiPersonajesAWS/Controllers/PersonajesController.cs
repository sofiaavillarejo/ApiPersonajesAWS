using ApiPersonajesAWS.Models;
using ApiPersonajesAWS.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPersonajesAWS.Controllers
{
    [Route("api/")]
    [ApiController]
    public class PersonajesController : ControllerBase
    {
        private RepositoryPersonajes repo;

        public PersonajesController(RepositoryPersonajes repo)
        {
            this.repo = repo;
        }

        [HttpGet("Personajes")]
        public async Task<ActionResult<List<Personaje>>> GetPersonajes()
        {
            return await this.repo.GetPersonajesAsync();

        }

        [HttpPost("CreatePersonaje")]
        public async Task<ActionResult> Create(Personaje pers)
        {
            await this.repo.CreatePersonajeAsync(pers.Nombre, pers.Imagen);
            return Ok();
        }
    }
}
