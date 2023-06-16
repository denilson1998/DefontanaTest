using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefontanaTest.Entities
{
    public class Sale
    {
        [Key]
        public long ID_Venta { get; set; }
        public int Total { get; set; }
        public DateTime Fecha { get; set; }
        public int ID_Local { get; set; }
        public virtual Local Local { get; set; }
    }
}
