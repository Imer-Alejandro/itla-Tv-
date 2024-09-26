using CapaDatos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CapaNegocio.servicios;
using Microsoft.EntityFrameworkCore;

namespace CapaPresentacion.Controllers
{
    public class SeriesController : Controller
    {
        private readonly SerieServicio _serieServicio;
        private readonly GeneroServicio _generoServicio;
        private readonly ProductoraServicio _productoraServicio; 
        public SeriesController(SerieServicio serieServicio, GeneroServicio generoServicio, ProductoraServicio productoraServicio)
        {
            _serieServicio = serieServicio;
            _generoServicio = generoServicio; 
            _productoraServicio = productoraServicio; 
        }

        // Accion para listar todas las series
        public async Task<IActionResult> Series()
        {
            var series = await _serieServicio.ObtenerSeriesAsync();

            // Obtener géneros y productoras
            var generos = await _generoServicio.ObtenerGenerosAsync();
            var productoras = await _productoraServicio.ObtenerProductorasAsync();

            ViewBag.Generos = generos; 
            ViewBag.Productoras = productoras; 

            return View(series); 
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Serie serie)
        {
            // Lógica para convertir el enlace de YouTube al formato embed
            if (!string.IsNullOrEmpty(serie.EnlaceVideo))
            {
                // Extraer el identificador del video de YouTube
                var videoId = ObtenerIdVideoYoutube(serie.EnlaceVideo);

                if (!string.IsNullOrEmpty(videoId))
                {
                    // Formato de YouTube embebido
                    serie.EnlaceVideo = $"https://www.youtube.com/embed/{videoId}";
                }
            }

            // Guardar la nueva serie usando el servicio
            await _serieServicio.CrearSerieAsync(serie);
            return RedirectToAction("Series");
        }

        // Método auxiliar para extraer el ID del video de YouTube
        private string ObtenerIdVideoYoutube(string url)
        {
            Uri uri;
            if (!Uri.TryCreate(url, UriKind.Absolute, out uri))
                return null;

            // Extraer el ID del video desde youtu.be o youtube.com
            if (uri.Host == "youtu.be")
            {
              
                return uri.AbsolutePath.Substring(1); // Elimina la primera barra "/"
            }
            else if (uri.Host.Contains("youtube.com"))
            {
                // https://www.youtube.com/watch?v={videoId}
                var query = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(uri.Query);
                if (query.TryGetValue("v", out var videoId))
                {
                    return videoId.ToString();
                }
            }

            return null;
        }
        // Metodo para editar una serie
        [HttpPost]
        public async Task<IActionResult> EditarBasico(Serie serie)
        {
           
                await _serieServicio.ActualizarSerieBasicoAsync(serie);
                return RedirectToAction("Series");
            
        }
        // Metodo para filtrar series
        [HttpGet]
        public async Task<IActionResult> FiltrarSeries(int? generoId, int? productoraId)
        {
            var series = await _serieServicio.ObtenerSeriesPorFiltroAsync(generoId, productoraId); 

            return Json(series); 
        }
        //Metodo eliminar series
        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            await _serieServicio.EliminarSerieAsync(id);
            return RedirectToAction("Series");
        }

    }
}