using System;
using System.Collections.Generic;

namespace AlmacenWS.Models
{
    public partial class Categoria
    {
        public Categoria()
        {
            Producto = new HashSet<Producto>();
        }

        public int IdCategoria { get; set; }
        public string NombreCategoria { get; set; }
        public string DescripcionCategoria { get; set; }

        public virtual ICollection<Producto> Producto { get; set; }
    }
}
