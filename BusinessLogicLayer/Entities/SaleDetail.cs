using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefontanaTest.Entities
{
    public class SaleDetail
    {
        [Key]
        public long ID_VentaDetalle { get; set; }
        public int Precio_Unitario { get; set; }
        public int Cantidad { get; set; }
        public int TotalLinea { get; set; }
        public int ID_Venta { get; set; }
        public virtual Sale Venta { get; set; }
        public int ID_Producto { get; set; }
        public virtual Product Producto { get; set; }
    }
}
