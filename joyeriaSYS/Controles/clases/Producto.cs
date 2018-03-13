using joyeriaSYS.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace joyeriaSYS.Controles.clases
{
    public class Producto : interfaz.InterfaceFactura<PRO_PRODUCTO>
    {
        public PRO_PRODUCTO Actualizar(PRO_PRODUCTO objeto)
        {
            var actualizado = new PRO_PRODUCTO();
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                actualizado = contexto.PRO_PRODUCTO.Where(cat => cat.IdProducto == objeto.IdProducto).FirstOrDefault();
                actualizado.CodigoNumerico = objeto.CodigoNumerico;
                actualizado.IdCategoria = objeto.IdCategoria;
                actualizado.Inventario = objeto.Inventario;
                actualizado.Precio = objeto.Precio;
                actualizado.NombreProducto = objeto.NombreProducto;
                contexto.SaveChanges();
            }
            return actualizado;
        }

        public IEnumerable<PRO_PRODUCTO> Consultar()
        {
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                return contexto.PRO_PRODUCTO.ToList();
            }
        }

        public Boolean Existe(PRO_PRODUCTO objeto)
        {
            var resultado = false;
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                if(contexto.PRO_PRODUCTO.Where(pro => pro.CodigoNumerico == objeto.CodigoNumerico && pro.IdCategoria == objeto.IdCategoria).Count() > 0){
                    resultado = true;
                }
            }
            return resultado;
        }
        
        public IEnumerable<PRO_PRODUCTO> ConsultarPorId(PRO_PRODUCTO objeto)
        {
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                return contexto.PRO_PRODUCTO.Where(cat => cat.IdProducto == objeto.IdProducto).ToList();
            }
        }

        public PRO_PRODUCTO ConsultarPorCodigoProducto(PRO_PRODUCTO objeto)
        {
            //var temp = new PRO_PRODUCTO();
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                return contexto.PRO_PRODUCTO.Where(pro => pro.CodigoNumerico == objeto.CodigoNumerico).FirstOrDefault();
            }
            //return temp;
        }

        public PRO_PRODUCTO Eliminar(PRO_PRODUCTO objeto)
        {
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                contexto.PRO_PRODUCTO.Remove(objeto);
                contexto.SaveChanges();
            }
            return objeto;
        }
        
        public PRO_PRODUCTO Insertar(PRO_PRODUCTO objeto)
        {
            var insertado = new PRO_PRODUCTO();
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                contexto.PRO_PRODUCTO.Add(objeto);
                contexto.SaveChanges();
                insertado = contexto.PRO_PRODUCTO.Where(cat => cat.NombreProducto == objeto.NombreProducto).FirstOrDefault();
            }
            return insertado;
        }
        
    }
}