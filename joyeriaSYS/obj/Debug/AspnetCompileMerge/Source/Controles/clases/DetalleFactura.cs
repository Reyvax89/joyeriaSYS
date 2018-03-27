﻿using joyeriaSYS.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

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
                contexto.SaveChanges();
            }
            return actualizado;
        }

        public IEnumerable<DEF_DETALLE_FACTURA> Consultar()
        {
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                return contexto.DEF_DETALLE_FACTURA.ToList();
            }
        }
        
        public IEnumerable<DEF_DETALLE_FACTURA> ConsultarPorId(DEF_DETALLE_FACTURA objeto)
        {
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                return contexto.DEF_DETALLE_FACTURA.Where(cat => cat.idDetalleFactura == objeto.idDetalleFactura).ToList();
            }
        }

        public IEnumerable<DEF_DETALLE_FACTURA> ConsultarPorIdFactura(int idFactura)
        {
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                return contexto.DEF_DETALLE_FACTURA.Where(cat => cat.idFactura == idFactura).ToList();
            }
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