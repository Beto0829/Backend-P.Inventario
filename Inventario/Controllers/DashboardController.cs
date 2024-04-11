using Inventario.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;

namespace Inventario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly MiDbContext _context;

        public DashboardController(MiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("TarjetaClientes")]
        public async Task<ActionResult<int>> ConsultarClientes()
        {
            var clientes = await _context.Clientes.ToListAsync();
            int tarjetaClientes = clientes.Count;

            if (clientes == null || tarjetaClientes == 0)
            {
                return NotFound("No existen los datos que buscas");
            }
            else
            {
                return Ok(tarjetaClientes);
            }
        }

        [HttpGet]
        [Route("TarjetaProveedors")]
        public async Task<ActionResult<int>> ConsultarProveedors()
        {
            var proveedors = await _context.Proveedors.ToListAsync();
            int tarjetaProveedors = proveedors.Count;

            if (proveedors == null || tarjetaProveedors == 0)
            {
                return NotFound("No existen los datos que buscas");
            }
            else
            {
                return Ok(tarjetaProveedors);
            }
        }

        [HttpGet]
        [Route("TarjetaProductos")]
        public async Task<ActionResult<int>> ConsultarProductos()
        {
            var productos = await _context.Productos.ToListAsync();
            int tarjetaProducto = productos.Count;

            if (productos == null || tarjetaProducto == 0)
            {
                return NotFound("No existen los datos que buscas");
            }
            else
            {
                return Ok(tarjetaProducto);
            }
        }

        ///Falta factura

        [HttpGet]
        [Route("TarjetaExistenciasTotales")]
        public async Task<ActionResult<int>> ConsultarExistenciasTotales()
        {
            var existencias = await _context.Entradas.ToListAsync();

            if (existencias == null || !existencias.Any())
            {
                return NotFound("No existen los datos que buscas");
            }
            else
            {
                int sumaExistenciaInicial = existencias.Sum(e => e.ExistenciaInicial);
                return Ok(sumaExistenciaInicial);
            }
        }

        //Falta Existencias vendidas

        [HttpGet]
        [Route("TarjetaExistenciasActuales")]
        public async Task<ActionResult<int>> ConsultarExistenciasActuales()
        {
            var existencias = await _context.Entradas.ToListAsync();

            if (existencias == null || !existencias.Any())
            {
                return NotFound("No existen los datos que buscas");
            }
            else
            {
                int sumaExistenciaActuales = existencias.Sum(e => e.ExistenciaActual);
                return Ok(sumaExistenciaActuales);
            }
        }

        //Falta importe vendido

        //Falta importe pagado

        //Falta importe restante

        //Falta beneficio bruto

        //Falta beneficio Neto

    }
}
