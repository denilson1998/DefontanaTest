using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class SaleDetailModel
    {
        public float ID_Venta { get; set; }
        public float ID_VentaDetalle { get; set; }
        public int Total { get; set; }
        public DateTime Fecha { get; set; }
        public float ID_Local { get; set; }
        public string NombreLocal { get; set; } = string.Empty;
        public float ID_Producto { get; set; }
        public string NombreProducto { get; set; } = string.Empty;
        public float ID_Marca { get; set; }
        public string NombreMarca { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public int Precio_Unitario { get; set; }
        public int Costo_Unitario { get; set; }
        public int TotalLinea { get; set; }
    }
}
