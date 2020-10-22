using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlmacenWS.Models;
using AlmacenWS.Models.Response;
using AlmacenWS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AlmacenWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VentaController : ControllerBase
    {
        private IVentaServicio _venta;
        public VentaController(IVentaServicio venta)
        {
            this._venta = venta;
        }
        public IActionResult AddVenta(VentaRespuesta Venta)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                using (Almacen_dbContext db = new Almacen_dbContext())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            _venta.AddVenta(Venta);
                            respuesta.Exito = 1;
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = ex.Message;
            }
            return Ok(respuesta);
        }
    }
}
