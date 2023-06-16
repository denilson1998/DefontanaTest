using DefontanaTest.Entities;
using DomainLayer.Interfaces.Repositories;
using DomainLayer.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer
{
    internal interface IApplication
    {
        void Run();
    }

    internal class Application : IApplication
    {
        private readonly ILogger _logger;
        private readonly ISaleDetailRepository _saleDetailRepository;
        public Application(ILogger<Application> logger, ISaleDetailRepository saleDetailRepository)
        {
            _logger = logger;
            _saleDetailRepository = saleDetailRepository;
        }

        public void Run()
        {
            _logger.LogInformation("Logging for Defontana Console App!");

            Console.Write("Escriba el Numero de Días para la Busqueda de Ventas, luego presione Enter: ");

            string days = Console.ReadLine();

            DataTable saleDetailTable = _saleDetailRepository.GetSaleDetailsByDays(Convert.ToInt32(days));

            List<SaleDetailModel> saleDetails = new List<SaleDetailModel>();

            foreach (DataRow saleDetail in saleDetailTable.Rows)
            {
                SaleDetailModel objSaleDetail = new SaleDetailModel();

                objSaleDetail.ID_Venta = Convert.ToInt64(saleDetail["ID_Venta"]);
                objSaleDetail.ID_VentaDetalle = Convert.ToInt64(saleDetail["ID_VentaDetalle"]);
                objSaleDetail.Total = (int)saleDetail["Total"];
                objSaleDetail.Fecha = (DateTime)saleDetail["Fecha"];
                objSaleDetail.ID_Local = Convert.ToInt64(saleDetail["ID_Local"]);
                objSaleDetail.NombreLocal = Convert.ToString(saleDetail["NombreLocal"])!;
                objSaleDetail.ID_Producto = Convert.ToInt64(saleDetail["ID_Producto"]);
                objSaleDetail.NombreProducto = Convert.ToString(saleDetail["NombreProducto"])!;
                objSaleDetail.ID_Marca = Convert.ToInt64(saleDetail["ID_Marca"]);
                objSaleDetail.NombreMarca = Convert.ToString(saleDetail["NombreMarca"])!;
                objSaleDetail.Cantidad = (int)saleDetail["Cantidad"];
                objSaleDetail.Precio_Unitario = (int)saleDetail["Precio_Unitario"];
                objSaleDetail.Costo_Unitario = (int)saleDetail["Costo_Unitario"];
                objSaleDetail.TotalLinea = (int)saleDetail["TotalLinea"];
                
                saleDetails.Add(objSaleDetail);
                Console.WriteLine("-------------------------------------------------------------");
                Console.WriteLine($"ID_Venta: {objSaleDetail.ID_Venta}" + " | " +
                    $"ID_VentaDetalle: {objSaleDetail.ID_VentaDetalle}" + " | " +
                    $"Total: {objSaleDetail.Total}" + " | " +
                    $"Fecha: {objSaleDetail.Fecha}" + " | " +
                    $"ID_Local: {objSaleDetail.ID_Local}" + " | " +
                    $"NombreLocal: {objSaleDetail.NombreLocal}" + " | " +
                    $"ID_Producto: {objSaleDetail.ID_Producto}" + " | " +
                    $"NombreProducto: {objSaleDetail.NombreProducto}" + " | " +
                    $"ID_Marca: {objSaleDetail.ID_Marca}" + " | " +
                    $"NombreMarca: {objSaleDetail.NombreMarca}" + " | " +
                    $"Cantidad: {objSaleDetail.Cantidad}" + " | " +
                    $"Precio_Unitario: {objSaleDetail.Precio_Unitario}" + " | " +
                    $"Costo_Unitario: {objSaleDetail.Costo_Unitario}" + " | " +
                    $"TotalLinea: {objSaleDetail.TotalLinea}");
            }

            Console.WriteLine("/////////////////////////////////////////////////////////////////////////////////////////////////////");
            Console.WriteLine("/////////////////////////////////////////////////////////////////////////////////////////////////////");
            Console.WriteLine("/////////////////////////////////////////////////////////////////////////////////////////////////////");
            Console.WriteLine("/////////////////////////////////////////////////////////////////////////////////////////////////////");
            Console.WriteLine("/////////////////////////////////////////////////////////////////////////////////////////////////////");
            Console.WriteLine("/////////////////////////////////////////////////////////////////////////////////////////////////////");
            Console.WriteLine("2:El total de ventas de los últimos 30 días (monto total y cantidad total de ventas).///////////////////////");
            
            Console.WriteLine($"Total Ventas(30 días): { saleDetails.Sum(s => s.TotalLinea)}");
            
            Console.WriteLine($"Cantidad Total Ventas(30 días): {saleDetails.Count()}");

            var day = 0;
            var month = 0;
            var hour = "";
            var amount = 0;
            
            var result = saleDetails.OrderByDescending(v => v.Total).Take(1).ToList();

            day = result[0].Fecha.Day;
            month = result[0].Fecha.Month;
            hour = result[0].Fecha.ToString("HH:mm:ss");
            amount = result[0].Total;

            Console.WriteLine("3:El día y hora en que se realizó la venta con el monto más alto (y cuál es aquel monto).////////////////////////////////////////////");
            
            Console.WriteLine($"Dia y Hora Venta con Monto Mas Alto");

            Console.WriteLine($"Total: {amount} | Dia: {day} | Mes: {month} | Hora: {hour}");

            Console.WriteLine("4:Indicar cuál es el producto con mayor monto total de ventas.////////////////////////////////////////////////////////////////////////");

            var result4 = saleDetails
                .GroupBy(p => p.ID_Producto)
                .OrderByDescending(s => s.Sum(l => l.TotalLinea))
                .Select(r => new
                {
                    ID_Producto = r.Key,
                    NombreProducto = r.First().NombreProducto,
                    MontoTotalVentaProducto = r.Sum(v => v.TotalLinea)
                }).Take(1).ToList();

            Console.WriteLine($"ID_Producto: {result4[0].ID_Producto} | NombreProducto: {result4[0].NombreProducto} | MontoTotalVentaProducto: {result4[0].MontoTotalVentaProducto}");

            Console.WriteLine("5:Indicar el local con mayor monto de ventas.//////////////////////////////////////////////////////////////////////");

            var result5 = saleDetails
                .GroupBy(p => p.ID_Local)
                .OrderByDescending(s => s.Sum(l => l.TotalLinea))
                .Select(r => new
                {
                    ID_Local = r.Key,
                    NombreLocal = r.First().NombreLocal,
                    MontoTotalVentaLocal = r.Sum(v => v.TotalLinea)
                }).Take(1).ToList();

            Console.WriteLine($"ID_Local: {result5[0].ID_Local} | NombreLocal: {result5[0].NombreLocal} | MontoTotalVentaLocal: {result5[0].MontoTotalVentaLocal}");

            Console.WriteLine("6:¿Cuál es la marca con mayor margen de ganancias?///////////////////////////////////////////////////////////////////////////////////");

            var result6 = saleDetails
                .GroupBy(p => p.ID_Marca)
                .OrderByDescending(s => s.Sum(l => l.TotalLinea - (l.Cantidad * l.Costo_Unitario)))
                .Select(r => new
                {
                    ID_Marca = r.Key,
                    NombreMarca = r.First().NombreMarca,
                    Ganancia = r.Sum(v => v.TotalLinea - (v.Cantidad * v.Costo_Unitario))
                }).Take(1).ToList();

            Console.WriteLine($"ID_Marca: {result6[0].ID_Marca} | NombreMarca: {result6[0].NombreMarca} | Ganancia: {result6[0].Ganancia}");

            Console.WriteLine("7:¿Cómo obtendrías cuál es el producto que más se vende en cada local?//////////////////////////////////////////////////////////////////////");

            var result7 = saleDetails
                        .GroupBy(p => p.ID_Local)
                        .Select(g => new
                        {
                            ID_Local = g.Key,
                            NombreLocal = g.First().NombreLocal,
                            Productos = g.OrderByDescending(p => p.Cantidad)
                                         .Select(p => new
                                         {
                                             ID_Producto = p.ID_Producto,
                                             NombreProducto = p.NombreProducto,
                                             CantidadVecesVendido = p.Cantidad
                                         })
                                         .First()
                        })
                        .ToList();

            foreach (var item in result7)
            {
                Console.WriteLine("---------------------------------");
                Console.WriteLine($"ID_Local: {item.ID_Local} | NombreLocal: {item.NombreLocal} | ID_Producto: {item.Productos.ID_Producto} | NombreProducto: {item.Productos.NombreProducto}");
            }

            Console.Write("Presione Enter para Finalizar el Programa");

            Console.ReadLine();
        }
    }
}
