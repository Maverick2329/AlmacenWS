using AlmacenWS.Models;
using AlmacenWS.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlmacenWS.Services
{
    public class VentaServicio : IVentaServicio
    {
        public void AddVenta(VentaRespuesta Venta)
        {
                using (Almacen_dbContext db = new Almacen_dbContext())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            Venta venta = new Venta();
                            venta.Total = Venta.Conceptos.Sum(t => t.Cantidad * t.PrecioUnitario);
                            venta.Fecha = DateTime.Now;
                            venta.IdCliente = Venta.IdCliente;
                            db.Venta.Add(venta);
                            db.SaveChanges();

                            foreach (var modelConcepto in Venta.Conceptos)
                            {
                                var concepto = new Models.Concepto();
                                concepto.Cantidad = modelConcepto.Cantidad;
                                concepto.IdProducto = modelConcepto.IdProducto;
                                concepto.PrecioUnitario = modelConcepto.PrecioUnitario;
                                concepto.Importe = modelConcepto.Importe;
                                concepto.IdVenta = venta.IdVenta;
                                db.Concepto.Add(concepto);
                                db.SaveChanges();
                            }
                            transaction.Commit();
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            throw new Exception("Ocurrio un Error en la Inserción");
                        }
                    }
                }           
        }
    }
}
