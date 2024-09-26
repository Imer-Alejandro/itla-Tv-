using CapaNegocio.servicios;
using CapaDatos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;

namespace CapaPresentacion.Controllers
{
    public class ProductorasController : Controller
    {
        private readonly ProductoraServicio _productoraServicio;

        public ProductorasController(ProductoraServicio productoraServicio)
        {
            _productoraServicio = productoraServicio;
        }

        // Acción para listar todas las productoras
        public async Task<IActionResult> Productoras()
        {
            var productoras = await _productoraServicio.ObtenerProductorasAsync();
            return View(productoras);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Productora productora)
        {
           
                await _productoraServicio.CrearProductoraAsync(productora);
                Console.WriteLine("este es el modelo a enviar " + productora.Nombre); 

                // Redirigir a la acción Productoras para mostrar el listado actualizado
                return RedirectToAction("Productoras");

        }

        [HttpPost]
        public async Task<IActionResult> Editar(Productora productora)
        {
           
                await _productoraServicio.ActualizarProductoraAsync(productora); 
                return RedirectToAction("Productoras");
            
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            await _productoraServicio.EliminarProductoraAsync(id); 
            return RedirectToAction("Productoras");
        }

    }
}
