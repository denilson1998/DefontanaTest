using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefontanaTest.Entities
{
    public class Product
    {
        [Key]
        public long ID_Producto { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public int Costo_Unitario { get; set; }
        public int ID_Marca { get; set; }
        public virtual Brand Marca { get; set; }
    }
}
