using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefontanaTest.Entities
{
    public class Brand
    {
        [Key]
        public long ID_Marca { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }
}
