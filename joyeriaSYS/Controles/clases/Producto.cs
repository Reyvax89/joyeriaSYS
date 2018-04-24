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
    public class Producto : interfaz.InterfaceFactura<PRO_PRODUCTO>
    {
        public PRO_PRODUCTO Actualizar(PRO_PRODUCTO objeto)
        {
            var actualizado = new PRO_PRODUCTO();
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                try
            {
            
                actualizado = contexto.PRO_PRODUCTO.Where(cat => cat.IdProducto == objeto.IdProducto).FirstOrDefault();
                actualizado.CodigoNumerico = objeto.CodigoNumerico;
                actualizado.IdCategoria = objeto.IdCategoria;
                actualizado.Inventario = objeto.Inventario;
                actualizado.Precio = objeto.Precio;
                actualizado.NombreProducto = objeto.NombreProducto;
                    contexto.SaveChanges();
                    return actualizado;
                }
            catch (Exception ex)
            {
                    throw ex;
            }
            }
        }

        //public IEnumerable<PRO_PRODUCTO> Consultar(string nombre, int categoria, string codigo)
        public IEnumerable<PRO_PRODUCTO> Consultar(string criterio)
        {
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                return contexto.PRO_PRODUCTO.Where(pro => pro.NombreProducto.Contains(criterio) || SqlFunctions.StringConvert((double)pro.IdCategoria).Contains(criterio) || SqlFunctions.StringConvert((double)pro.CodigoNumerico).Contains(criterio)).ToList().OrderByDescending(pro => pro.IdProducto);
            }
        }
        public IEnumerable<PRO_PRODUCTO> ConsultarPorCategoria(int idCategoria)
        {
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                if(idCategoria == 4)
                {
                    return contexto.PRO_PRODUCTO.Where(pro => pro.IdCategoria == 1 || pro.IdCategoria == 2).ToList().OrderByDescending(pro => pro.IdProducto);
                }
                else
                {
                    return contexto.PRO_PRODUCTO.ToList().Where(pro => pro.IdCategoria == idCategoria).OrderByDescending(pro => pro.IdProducto);
                }
            }
        }

        public Boolean Existe(PRO_PRODUCTO objeto)
        {
            var resultado = false;
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                if(contexto.PRO_PRODUCTO.Where(pro => pro.CodigoNumerico == objeto.CodigoNumerico && pro.IdCategoria == objeto.IdCategoria && pro.NombreProducto == objeto.NombreProducto).Count() > 0){
                    resultado = true;
                }
            }
            return resultado;
        }
        
        public IEnumerable<PRO_PRODUCTO> ConsultarPorId(PRO_PRODUCTO objeto)
        {
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                return contexto.PRO_PRODUCTO.Where(pro => pro.IdProducto == objeto.IdProducto).ToList();
            }
        }

        public PRO_PRODUCTO ConsultarPorIdProducto(PRO_PRODUCTO objeto)
        {
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                return contexto.PRO_PRODUCTO.Where(pro => pro.IdProducto == objeto.IdProducto).FirstOrDefault();
            }
        }

        public PRO_PRODUCTO ConsultarPorIdCategoria(PRO_PRODUCTO objeto)
        {
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                return contexto.PRO_PRODUCTO.Where(cat => cat.IdProducto == objeto.IdProducto).FirstOrDefault();
            }
        }

        public IEnumerable<PRO_PRODUCTO> ConsultarPorNombre(PRO_PRODUCTO objeto)
        {
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                return contexto.PRO_PRODUCTO.Where(cat => cat.NombreProducto == objeto.NombreProducto).ToList();
            }
        }
        public IEnumerable<PRO_PRODUCTO> ConsultarPorNombreCodigoCategoria(PRO_PRODUCTO objeto)
        {
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                return contexto.PRO_PRODUCTO.Where(pro => pro.NombreProducto == objeto.NombreProducto && pro.IdCategoria == objeto.IdCategoria && pro.CodigoNumerico == objeto.CodigoNumerico).ToList();
            }
        }

        public PRO_PRODUCTO ConsultarProductoPorNombreCodigoCategoria(PRO_PRODUCTO objeto)
        {
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                return contexto.PRO_PRODUCTO.Where(pro => pro.NombreProducto == objeto.NombreProducto && pro.IdCategoria == objeto.IdCategoria && pro.CodigoNumerico == objeto.CodigoNumerico).FirstOrDefault();
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

        public IEnumerable<PRO_PRODUCTO> Consultar()
        {
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                return contexto.PRO_PRODUCTO.ToList().OrderByDescending(pro => pro.IdProducto);
            }
        }
    }
}