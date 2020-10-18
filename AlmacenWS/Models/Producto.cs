using System;
using System.Collections.Generic;

namespace AlmacenWS.Models
{
    public partial class Producto
    {
        public Producto()
        {
            Concepto = new HashSet<Concepto>();
        }

        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public string DescripcionProducto { get; set; }
        public string PresentacionProducto { get; set; }
        public string MarcaProducto { get; set; }
        public string CodigoProducto { get; set; }
        public decimal PrecioCosto { get; set; }
        public decimal CantidadExistencia { get; set; }
        public int IdCategoria { get; set; }

        public virtual Categoria IdCategoriaNavigation { get; set; }
        public virtual ICollection<Concepto> Concepto { get; set; }
    }
}
