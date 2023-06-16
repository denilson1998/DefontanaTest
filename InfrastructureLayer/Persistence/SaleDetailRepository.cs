using DefontanaTest.Database;
using DefontanaTest.Entities;
using DomainLayer.Interfaces.Repositories;
using InfrastructureLayer.ADO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayer.Persistence
{
    public class SaleDetailRepository : ISaleDetailRepository
    {
        private SqlConnection propSqlCnt { get; set; }
        private SqlServerConnection objCnt;

        public SaleDetailRepository()
        {
            this.objCnt = new SqlServerConnection();
            this.propSqlCnt = objCnt.prepareConnection();
        }

        public DataTable GetSaleDetailsByDays(int days)
        {
            var objCnt = this.propSqlCnt;
            
            DataTable dtG = new DataTable();

            try
            {
                //string strSQL = @"select f.*, e.mensaje 'mensaje', e.descripcion 'estadoDescripcion' 
                //                  from facturacion_registros f 
                //                  inner join facturacion_estados e on (f.idEstado = e.idEstado) 
                //                  where f.numeroDocumentoIdentidad = '" + objFactura.cIdentidad + "' and  f.numeroFactura = '" + objFactura.nFactura + "' and  f.tipodoc = '" + objFactura.tDoc + "'";

                string strSQL = @"select vta.*, lc.Nombre as NombreLocal, vtad.ID_VentaDetalle, vtad.ID_Producto, prd.Nombre as NombreProducto, prd.ID_Marca, mc.Nombre as NombreMarca, vtad.Cantidad, vtad.Precio_Unitario, prd.Costo_Unitario, vtad.TotalLinea
                                    from VentaDetalle vtad
                                    inner join Venta vta on (vtad.ID_Venta = vta.ID_Venta)
                                    inner join Producto prd on (prd.ID_Producto = vtad.ID_Producto)
                                    inner join Local lc on (lc.ID_Local = vta.ID_Local)
                                    inner join Marca mc on (mc.ID_Marca = prd.ID_Marca)
                                    where vta.Fecha between DATEADD(DAY, -" + days + ", GETDATE()) and GETDATE()";


                objCnt.Open();
                SqlCommand objSqlCmd = new SqlCommand(strSQL, objCnt);
                objSqlCmd.CommandTimeout = 1500;
                //objSqlCmd.ExecuteNonQuery();
                SqlDataAdapter objSqlAdap = new SqlDataAdapter(objSqlCmd);
                objSqlAdap.Fill(dtG);

                return dtG;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
