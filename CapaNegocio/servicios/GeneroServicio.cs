using CapaDatos;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CapaNegocio.servicios
{
    public class GeneroServicio
    {
        private readonly practicasP3Context _contexto;

        public GeneroServicio(practicasP3Context contexto)
        {
            _contexto = contexto;
        }

        // Método para obtener todos los géneros usando un SP
        public async Task<IEnumerable<Genero>> ObtenerGenerosAsync()
        {
            return await _contexto.Generos.FromSqlRaw("EXEC sp_ObtenerGeneros").ToListAsync();
        }

        // Método para obtener un género por ID usando un SP
        public async Task<Genero> ObtenerGeneroPorIdAsync(int id)
        {
            var parametro = new SqlParameter("@Id", id);
            return await _contexto.Generos.FromSqlRaw("EXEC sp_ObtenerGeneroPorId @Id", parametro).FirstOrDefaultAsync();
        }

        // Método para crear un nuevo género usando un SP
        public async Task CrearGeneroAsync(Genero genero)
        {
            var parametros = new[]
            {
                new SqlParameter("@Nombre", genero.Nombre)
            };

            await _contexto.Database.ExecuteSqlRawAsync("EXEC sp_CrearGenero @Nombre", parametros);
        }

        // Método para actualizar un género usando un SP
        public async Task ActualizarGeneroAsync(Genero genero)
        {
            var parametros = new[]
            {
                new SqlParameter("@Id", genero.Id),
                new SqlParameter("@Nombre", genero.Nombre)
            };

            await _contexto.Database.ExecuteSqlRawAsync("EXEC sp_ActualizarGenero @Id, @Nombre", parametros);
        }

        // Método para eliminar un género usando un SP
        public async Task EliminarGeneroAsync(int id)
        {
            var parametro = new SqlParameter("@Id", id);
            await _contexto.Database.ExecuteSqlRawAsync("EXEC sp_EliminarGenero @Id", parametro);
        }
    }
}
