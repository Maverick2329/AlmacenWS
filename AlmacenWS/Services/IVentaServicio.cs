using AlmacenWS.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlmacenWS.Services
{
    public interface IVentaServicio
    {
        public void AddVenta(VentaRespuesta Venta);
    }
}
