using Inventario.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalidasController : ControllerBase
    {
        private readonly MiDbContext _context;

        public SalidasController(MiDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("Agregar")]
        public async Task<ActionResult<Salida>> CreateSalida(Salida salida)
        {
            // Verifica si los productos especificados en la salida están disponibles en las entradas previas
            foreach (var productoSalida in salida.ProductoSalidas)
            {
                var entrada = await _context.Entradas.FindAsync(productoSalida.IdEntrada);
                if (entrada == null)
                {
                    return BadRequest($"El producto con ID {productoSalida.IdProducto} no existe en las entradas.");
                }

                // Llena automáticamente los datos de ProductoSalida con los datos de la entrada
                productoSalida.IdCategoria = entrada.IdCategoria;
                productoSalida.IdProducto = entrada.IdProducto;
                productoSalida.FechaEntrada = entrada.FechaEntrada;
                productoSalida.PrecioVentaSalida = entrada.PrecioVenta;

                // Asigna la entrada al ProductoSalida
                productoSalida.Entrada = entrada;
            }

            // Agrega la nueva salida a la base de datos
            _context.Salidas.Add(salida);
            await _context.SaveChangesAsync();

            return Ok("Se guardó exitosamente");
        }

    }
}
