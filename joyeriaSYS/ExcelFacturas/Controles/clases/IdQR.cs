using joyeriaSYS.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace joyeriaSYS.Controles.clases
{
    public class IdQR : interfaz.InterfaceFactura<CQR_CODIGO_QR>
    {
        public CQR_CODIGO_QR Actualizar(CQR_CODIGO_QR objeto)
        {
            var actualizado = new CQR_CODIGO_QR();
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                actualizado = contexto.CQR_CODIGO_QR.Where(cat => cat.idQR == objeto.idQR).FirstOrDefault();
                actualizado.urlImagen = objeto.urlImagen;
                actualizado.idProducto = objeto.idProducto;
                contexto.SaveChanges();
            }
            return actualizado;
        }

        public IEnumerable<CQR_CODIGO_QR> Consultar()
        {
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                return contexto.CQR_CODIGO_QR.ToList();
            }
        }

        public IEnumerable<CQR_CODIGO_QR> ConsultarPorId(CQR_CODIGO_QR objeto)
        {
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                return contexto.CQR_CODIGO_QR.Where(cat => cat.idQR == objeto.idQR).ToList();
            }
        }

        public CQR_CODIGO_QR Eliminar(CQR_CODIGO_QR objeto)
        {
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                contexto.CQR_CODIGO_QR.Remove(objeto);
                contexto.SaveChanges();
            }
            return objeto;
        }

        public bool Existe(CQR_CODIGO_QR objeto)
        {
            throw new NotImplementedException();
        }

        public CQR_CODIGO_QR Insertar(CQR_CODIGO_QR objeto)
        {
            var insertado = new CQR_CODIGO_QR();
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                contexto.CQR_CODIGO_QR.Add(objeto);
                contexto.SaveChanges();
                insertado = contexto.CQR_CODIGO_QR.Where(cat => cat.idQR == objeto.idQR).FirstOrDefault();
            }
            return insertado;
        }
        
    }
}