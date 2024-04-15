using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Inventario.Models
{
    public class ProductoSalida
    {
        public int Id { get; set; }

        public int IdEntrada { get; set; }

        [JsonIgnore]
        public int IdCategoria { get; set; }

        [JsonIgnore]
        public int IdProducto { get; set; }

        [JsonIgnore]
        public DateTime FechaEntrada { get; set; }

        public int Cantidad { get; set; }

        [JsonIgnore]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecioVentaSalida { get; set; }

        public bool TipoDescuento { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Descuento { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalDescuento { get; set; }

        [JsonIgnore]
        public Entrada? Entrada { get; set; }
    }
}
