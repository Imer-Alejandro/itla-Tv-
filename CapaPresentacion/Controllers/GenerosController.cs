using CapaNegocio.servicios;
using CapaDatos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CapaPresentacion.Controllers
{
    public class GenerosController : Controller
    {
        private readonly GeneroServicio _generoServicio;

        public GenerosController(GeneroServicio generoServicio)
        {
            _generoServicio = generoServicio;
        }

        // Acción para listar todos los géneros
      
        public async Task<IActionResult> Generos()
        {
            var generos = await _generoServicio.ObtenerGenerosAsync();
            return View(generos);
        }

        // Acción para crear un nuevo género
        [HttpPost]
        public async Task<IActionResult> Crear(Genero genero)
        {
            
                await _generoServicio.CrearGeneroAsync(genero);
                return RedirectToAction(nameof(Generos));
          
        }

        // Acción para editar un género
        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var genero = await _generoServicio.ObtenerGeneroPorIdAsync(id);
            return View(genero);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Genero genero)
        {
            
                await _generoServicio.ActualizarGeneroAsync(genero);
                return RedirectToAction(nameof(Generos));
        }

        // Acción para eliminar un género
        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            await _generoServicio.EliminarGeneroAsync(id);
            return RedirectToAction(nameof(Generos));
        }


    }
}
