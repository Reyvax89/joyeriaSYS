//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace joyeriaSYS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Vista_ProductosPorDetalleFactura
    {
        public int idProducto { get; set; }
        public int idFactura { get; set; }
        public int CodigoNumerico { get; set; }
        public int CantidadProducto { get; set; }
        public int CantidadDevuelta { get; set; }
        public int idDetalleFactura { get; set; }
        public string NombreProducto { get; set; }
        public int IdCategoria { get; set; }
    }
}