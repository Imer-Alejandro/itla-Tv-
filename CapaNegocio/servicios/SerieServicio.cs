using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CapaNegocio.servicios
{
    public class SerieServicio
    {
        private readonly practicasP3Context _contexto;

        public SerieServicio(practicasP3Context contexto)
        {
            _contexto = contexto;
        }

        // Metodo para obtener todas las series usando un SP
        public async Task<IEnumerable<Serie>> ObtenerSeriesAsync()
        {
            return await _contexto.Series.FromSqlRaw("EXEC sp_ObtenerSerie").ToListAsync();
        }

        // Metodo para obtener una serie por ID usando un SP
        public async Task<Serie> ObtenerSeriePorIdAsync(int id)
        {
            var parametro = new SqlParameter("@Id", id);
            return await _contexto.Series.FromSqlRaw("EXEC sp_ObtenerSeriePorId @Id", parametro).FirstOrDefaultAsync();
        }

        // Metodo para crear una nueva serie usando un SP
        public async Task CrearSerieAsync(Serie serie)
        {
            var parametros = new[]
            {
                new SqlParameter("@Nombre", serie.Nombre),
                new SqlParameter("@Portada", serie.Portada),
                new SqlParameter("@EnlaceVideo", serie.EnlaceVideo),
                new SqlParameter("@ProductoraId", serie.ProductoraId),
                new SqlParameter("@GeneroId", serie.GeneroId)
            };

            await _contexto.Database.ExecuteSqlRawAsync("EXEC sp_CrearSerie @Nombre, @Portada, @EnlaceVideo, @ProductoraId, @GeneroId", parametros);
        }

        // Metodo para actualizar una serie usando un SP
        public async Task ActualizarSerieBasicoAsync(Serie serie)
        {
            var parametros = new[]
            {
            new SqlParameter("@Id", serie.Id),
            new SqlParameter("@Nombre", serie.Nombre),
            new SqlParameter("@Portada", serie.Portada),
            new SqlParameter("@EnlaceVideo", serie.EnlaceVideo)
        };

            await _contexto.Database.ExecuteSqlRawAsync("EXEC sp_ActualizarSerieBasico @Id, @Nombre, @Portada, @EnlaceVideo", parametros);
        }

        // Metodo para eliminar una serie usando un SP
        public async Task EliminarSerieAsync(int id)
        {
            var parametro = new SqlParameter("@Id", id);
            await _contexto.Database.ExecuteSqlRawAsync("EXEC sp_EliminarSerie @Id", parametro);
        }

        // Metodo para optener listado de series por genero y productora usando un SP

        public async Task<IEnumerable<Serie>> ObtenerSeriesPorFiltroAsync(int? generoId, int? productoraId)
        {
            var parametros = new List<SqlParameter>
            {
                new SqlParameter("@GeneroId", (object)generoId ?? DBNull.Value),
                new SqlParameter("@ProductoraId", (object)productoraId ?? DBNull.Value)
            };

            return await _contexto.Series.FromSqlRaw("EXEC sp_ObtenerSeriesPorFiltro @GeneroId, @ProductoraId", parametros.ToArray()).ToListAsync();
        }
    }
}
