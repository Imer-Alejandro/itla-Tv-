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
    public class ProductoraServicio
    {
        private readonly practicasP3Context _contexto;

        public ProductoraServicio(practicasP3Context contexto)
        {
            _contexto = contexto;
        }

        // Método para obtener todas las productoras usando un SP
        public async Task<IEnumerable<Productora>> ObtenerProductorasAsync()
        {
            return await _contexto.Productoras.FromSqlRaw("EXEC sp_ObtenerProductoras").ToListAsync();
        }

        // Método para obtener una productora por ID usando un SP
        public async Task<Productora> ObtenerProductoraPorIdAsync(int id)
        {
            var parametro = new SqlParameter("@Id", id);
            return await _contexto.Productoras.FromSqlRaw("EXEC sp_ObtenerProductoraPorId @Id", parametro).FirstOrDefaultAsync();
        }

        // Método para crear una nueva productora usando un SP
        public async Task CrearProductoraAsync(Productora productora)
        {
            var parametros = new[]
            {
                new SqlParameter("@Nombre", productora.Nombre)
            };

            await _contexto.Database.ExecuteSqlRawAsync("EXEC sp_CrearProductora @Nombre", parametros);
        }

        // Método para actualizar una productora usando un SP
        public async Task ActualizarProductoraAsync(Productora productora)
        {
            var parametros = new[]
            {
                new SqlParameter("@Id", productora.Id),
                new SqlParameter("@Nombre", productora.Nombre)
            };

            await _contexto.Database.ExecuteSqlRawAsync("EXEC sp_ActualizarProductora @Id, @Nombre", parametros);
        }

        // Método para eliminar una productora usando un SP
        public async Task EliminarProductoraAsync(int id)
        {
            var parametro = new SqlParameter("@Id", id);
            await _contexto.Database.ExecuteSqlRawAsync("EXEC sp_EliminarProductora @Id", parametro);
        }
    }
}
