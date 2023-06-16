using DefontanaTest.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Interfaces.Repositories
{
    public interface ISaleDetailRepository
    {
        public DataTable GetSaleDetailsByDays(int days);
    }
}
