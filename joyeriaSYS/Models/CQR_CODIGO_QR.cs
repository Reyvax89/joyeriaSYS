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
    
    public partial class CQR_CODIGO_QR
    {
        public int idQR { get; set; }
        public string urlImagen { get; set; }
        public int idProducto { get; set; }
    
        public virtual PRO_PRODUCTO PRO_PRODUCTO { get; set; }
    }
}