using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaApp.Models
{
    public class Venta
    {
        public int IdVenta { get; set; }
        public int ArqueoId { get; set; } // Necesario para registrar la venta
        public DateTime FechaHora { get; set; }
        public int ProductoId { get; set; }
        public string NombreProducto { get; set; } // opcional para mostrar
        public int Cantidad { get; set; }
        public double PrecioUnitario { get; set; }
        public double Total { get; set; }

    }
}
