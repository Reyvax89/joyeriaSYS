﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class JoyeriaEntities : DbContext
    {
        public JoyeriaEntities()
            : base("name=JoyeriaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<CAT_CATEGORIA> CAT_CATEGORIA { get; set; }
        public virtual DbSet<CLI_CLIENTES> CLI_CLIENTES { get; set; }
        public virtual DbSet<CQR_CODIGO_QR> CQR_CODIGO_QR { get; set; }
        public virtual DbSet<PRO_PRODUCTO> PRO_PRODUCTO { get; set; }
        public virtual DbSet<DEF_DETALLE_FACTURA> DEF_DETALLE_FACTURA { get; set; }
        public virtual DbSet<Vista_ProductosPorDetalleFactura> Vista_ProductosPorDetalleFactura { get; set; }
        public virtual DbSet<FAC_FACTURA> FAC_FACTURA { get; set; }
    }
}
