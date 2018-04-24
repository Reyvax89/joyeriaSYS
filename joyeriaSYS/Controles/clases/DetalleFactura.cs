using joyeriaSYS.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace joyeriaSYS.Controles.clases
{
    public class DetalleFactura : interfaz.InterfaceFactura<DEF_DETALLE_FACTURA>
    {
        public DEF_DETALLE_FACTURA Actualizar(DEF_DETALLE_FACTURA objeto)
        {
            var actualizado = new DEF_DETALLE_FACTURA();
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                actualizado = contexto.DEF_DETALLE_FACTURA.Where(cat => cat.idDetalleFactura == objeto.idDetalleFactura).FirstOrDefault();
                actualizado.CantidadProducto = objeto.CantidadProducto;
                actualizado.idFactura = objeto.idFactura;
                actualizado.idProducto = objeto.idProducto;
                actualizado.CantidadDevuelta = objeto.CantidadDevuelta;
                contexto.SaveChanges();
            }
            return actualizado;
        }

        public IEnumerable<DEF_DETALLE_FACTURA> Consultar()
        {
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                return contexto.DEF_DETALLE_FACTURA.ToList().OrderByDescending(det => det.idDetalleFactura);
            }
        }
        
        public IEnumerable<DEF_DETALLE_FACTURA> ConsultarPorId(DEF_DETALLE_FACTURA objeto)
        {
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                return contexto.DEF_DETALLE_FACTURA.Where(def => def.idDetalleFactura == objeto.idDetalleFactura).ToList().OrderByDescending(det => det.idDetalleFactura);
            }
        }

        public DEF_DETALLE_FACTURA ConsultarPorIdDetalleFactura(DEF_DETALLE_FACTURA objeto)
        {
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                return contexto.DEF_DETALLE_FACTURA.Where(def => def.idDetalleFactura == objeto.idDetalleFactura).FirstOrDefault();
            }
        }

        public IEnumerable<Vista_ProductosPorDetalleFactura> ConsultarPorIdFactura(int idFactura, string criterio)
        {
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                return contexto.Vista_ProductosPorDetalleFactura.Where(vis => SqlFunctions.StringConvert((double)vis.CodigoNumerico).Contains(criterio) && vis.idFactura == idFactura).ToList();
            }// Fin del using
        }

        public class datosDeDetalleFactura
        {
            public int idProductoGlobal { get; set; }
            public int cantidadProductoGlobal { get; set; }
            public string codigoNumerico { get; set; }
        }

        public DEF_DETALLE_FACTURA ConsultarPorIdFacturaYIdProducto(DEF_DETALLE_FACTURA objeto)
        {
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                return contexto.DEF_DETALLE_FACTURA.Where(det => det.idFactura == objeto.idFactura && det.idProducto == objeto.idProducto).FirstOrDefault();
            }
        }

        public DEF_DETALLE_FACTURA Eliminar(DEF_DETALLE_FACTURA objeto)
        {
            try
            {
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                    objeto = contexto.DEF_DETALLE_FACTURA.Where(cat => cat.idDetalleFactura == objeto.idDetalleFactura).FirstOrDefault();
                contexto.DEF_DETALLE_FACTURA.Remove(objeto);
                contexto.SaveChanges();
            }
            }catch(Exception ex)
            {
                var err = ex.Message;
            }
            
            return objeto;
        }

        public bool Existe(DEF_DETALLE_FACTURA objeto)
        {
            throw new NotImplementedException();
        }

        public DEF_DETALLE_FACTURA Insertar(DEF_DETALLE_FACTURA objeto)
        {
            var insertado = new DEF_DETALLE_FACTURA();
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                contexto.DEF_DETALLE_FACTURA.Add(objeto);
                contexto.SaveChanges();
                insertado = contexto.DEF_DETALLE_FACTURA.Where(cat => cat.idFactura == objeto.idFactura && cat.idProducto == objeto.idProducto).FirstOrDefault();
            }
            return insertado;
        }
    }
}