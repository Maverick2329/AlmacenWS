using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlmacenWS.Models.Response
{
    public class VentaRespuesta
    {
        [Required]
        [Range(1,Double.MaxValue, ErrorMessage ="El valor del idCliente debe ser mayor a 0")]
        [ExisteCliente(ErrorMessage ="El cliente no existe")]
        public int IdCliente { get; set; }
        public decimal Total { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Deben de existir conceptos")]
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

    #region [ Validaciones ]
    public class ExisteClienteAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            //return base.IsValid(value);
            int idCliente = (int)value;
            using(var db = new Models.Almacen_dbContext())
            {
                if (db.Cliente.Find(idCliente) == null)
                {
                    return false;
                }
            }
            return true;
        }
    }

    #endregion [ Validaciones ]
}
