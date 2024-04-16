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
        public async Task<ActionResult<Salida>> AgregarSalida(Salida salida)
        {
            try
            {
                DateTime fechaHoraActual = DateTime.Now;

                salida.FechaFactura = new DateTime(fechaHoraActual.Year, fechaHoraActual.Month, fechaHoraActual.Day, fechaHoraActual.Hour, fechaHoraActual.Minute, 0);

                _context.Salidas.Add(salida);
                await _context.SaveChangesAsync();

                int nuevaSalidaId = salida.Id;

                if (salida.ProductoSalidas != null && salida.ProductoSalidas.Any())
                {
                    foreach (var productoSalida in salida.ProductoSalidas)
                    {
                        productoSalida.IdSalida = nuevaSalidaId;
                        _context.ProductoSalidas.Add(productoSalida);
                    }
                    await _context.SaveChangesAsync();
                }

                return Ok(salida);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al agregar la salida: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("ConsultarTodo")]
        public async Task<ActionResult<IEnumerable<Salida>>> ConsultarSalidas()
        {
            try
            {
                var salidas = await _context.Salidas
                    .Include(s => s.ProductoSalidas)
                    .ToListAsync();

                if (salidas == null || salidas.Count == 0)
                {
                    return NotFound();
                }

                return Ok(salidas);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al consultar las salidas: " + ex.Message);
            }
        }


    }
}
