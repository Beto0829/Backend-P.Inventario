using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Inventario.Models
{
    public class Salida
    {
        public int Id { get; set; }

        public int IdCliente { get; set; }

        [JsonIgnore]
        public DateTime FechaFactura { get; set; }

        public List<ProductoSalida>? ProductoSalidas { get; set; }

        [JsonIgnore]
        public Cliente? Cliente { get; set; }

    }
}
