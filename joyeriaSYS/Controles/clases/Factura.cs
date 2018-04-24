using joyeriaSYS.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;

namespace joyeriaSYS.Controles.clases
{
    public class Factura : interfaz.InterfaceFactura<FAC_FACTURA>
    {
        public FAC_FACTURA Actualizar(FAC_FACTURA objeto)
        {
            var actualizado = new FAC_FACTURA();
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                actualizado = contexto.FAC_FACTURA.Where(cat => cat.idFactura == objeto.idFactura).FirstOrDefault();
                actualizado.CodTabla = objeto.CodTabla;
                actualizado.estado = objeto.estado;
                actualizado.fechaCreacion = objeto.fechaCreacion;
                actualizado.fechaLiquidacion = objeto.fechaLiquidacion;
                actualizado.montoFactura = objeto.montoFactura;
                actualizado.NoFactura = objeto.NoFactura;
                actualizado.saldo = objeto.saldo;
                actualizado.totalDevuelto = objeto.totalDevuelto;
                actualizado.totalPiezas = objeto.totalPiezas;
                actualizado.idCliente = objeto.idCliente;
                actualizado.idCategoriaMetal = objeto.idCategoriaMetal;
                contexto.SaveChanges();
            }
            return actualizado;
        }

        public IEnumerable<FAC_FACTURA> Consultar()
        {
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                return contexto.FAC_FACTURA.ToList().OrderByDescending(fac => fac.idFactura);
            }
        }

        public IEnumerable<FAC_FACTURA> ConsultarFacturasFinalizadas()
        {
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                return contexto.FAC_FACTURA.Where(pro => pro.estado==1).ToList().OrderByDescending(fac => fac.NoFactura);
            }
        }

        public IEnumerable<FAC_FACTURA> ConsultarPorNoFactura(string criterio)
        {
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                return contexto.FAC_FACTURA.Where(fac => SqlFunctions.StringConvert((double)fac.NoFactura).Contains(criterio)).ToList();
            }
        }

        public FAC_FACTURA ConsultaPorNumeroDeFactura(FAC_FACTURA objeto)
        {
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                return contexto.FAC_FACTURA.Where(fac => fac.NoFactura == objeto.NoFactura).FirstOrDefault();
            }
        }

        public IEnumerable<FAC_FACTURA> ConsultarPorId(FAC_FACTURA objeto)
        {
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                return contexto.FAC_FACTURA.Where(fac => fac.idFactura == objeto.idFactura).ToList();
            }
        }

        public FAC_FACTURA Eliminar(FAC_FACTURA objeto)
        {
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                contexto.FAC_FACTURA.Remove(objeto);
                contexto.SaveChanges();
            }
            return objeto;
        }
        
        public FAC_FACTURA Insertar(FAC_FACTURA objeto)
        {
            var insertado = new FAC_FACTURA();
            try
            {
                using (JoyeriaEntities contexto = new JoyeriaEntities())
                {
                    contexto.FAC_FACTURA.Add(objeto);
                    contexto.SaveChanges();
                    insertado = contexto.FAC_FACTURA.Where(cat => cat.idFactura == objeto.idFactura).FirstOrDefault();
                }
            }catch (Exception ex)
            {
                var err = ex.Message;
            }
            return insertado;
        }

        public bool Existe(FAC_FACTURA objeto)
        {
            throw new NotImplementedException();
        }
    }
}