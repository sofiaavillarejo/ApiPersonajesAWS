using ApiPersonajesAWS.Data;
using ApiPersonajesAWS.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiPersonajesAWS.Repositories
{
    public class RepositoryPersonajes
    {
        private PersonajesContext context;

        public RepositoryPersonajes(PersonajesContext context)
        {
            this.context = context;
        }

        public async Task<List<Personaje>> GetPersonajesAsync()
        {
            return await this.context.Personajes.ToListAsync();
        }

        private async Task<int> GetMaxIdPersonajeAsync()
        {
            return await this.context.Personajes.MaxAsync(p => p.IdPersonaje) + 1;
        }

        public async Task CreatePersonajeAsync(string nombre, string imagen)
        {
            Personaje p = new Personaje
            {
                IdPersonaje = await this.GetMaxIdPersonajeAsync(),
                Nombre = nombre,
                Imagen = imagen
            };
            await this.context.Personajes.AddAsync(p);
            await this.context.SaveChangesAsync();
        }

        public async Task<Personaje> FindPersonajeAsync(int idPersonaje)
        {
            Personaje pers = await this.context.Personajes.FirstOrDefaultAsync(p => p.IdPersonaje == idPersonaje);
            return pers;
        }

        public async Task UpdatePersonajesAsync (Personaje pers)
        {
            Personaje personaje = await this.FindPersonajeAsync(pers.IdPersonaje);
            if (personaje != null)
            {
                personaje.Nombre = pers.Nombre;
                personaje.Imagen = pers.Imagen;
                await this.context.SaveChangesAsync();
            }else
            {
                throw new Exception("Personaje no encontrado");
            }
        }
    }
}
