using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlmacenWS.Models.Response
{
    public class VentaRespuesta
    {
        public int IdCliente { get; set; }
        public decimal Total { get; set; }
        public List<Concepto> Conceptos { get; set; }

        public VentaRespuesta()
        {
            this.Conceptos = new List<Concepto>();
        }
    }

    public class Concepto
    {
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Importe { get; set; }
        public int IdProducto { get; set; }
    }
}
