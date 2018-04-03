using joyeriaSYS.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace joyeriaSYS.Controles.clases
{
    public class Categoria : interfaz.InterfaceFactura<CAT_CATEGORIA>
    {
        public CAT_CATEGORIA Actualizar(CAT_CATEGORIA objeto)
        {
            var actualizado = new CAT_CATEGORIA();
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                actualizado = contexto.CAT_CATEGORIA.Where(cat => cat.idCategoria == objeto.idCategoria).FirstOrDefault();
                actualizado.Nombre = objeto.Nombre;
                //actualizado.Codigo = objeto.Codigo;
                contexto.SaveChanges();
            }
            return actualizado;
        }

        public IEnumerable<CAT_CATEGORIA> Consultar()
        {
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                return contexto.CAT_CATEGORIA.ToList();
            }
        }

        public IEnumerable<CAT_CATEGORIA> ConsultarPorId(CAT_CATEGORIA objeto)
        {
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                return contexto.CAT_CATEGORIA.Where(cat => cat.idCategoria == objeto.idCategoria).ToList();
            }
        }

        public IEnumerable<CAT_CATEGORIA> ConsultarPorNombre(CAT_CATEGORIA objeto)
        {
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                return contexto.CAT_CATEGORIA.Where(cat => cat.Nombre == objeto.Nombre).ToList();
            }
        }

        public CAT_CATEGORIA Eliminar(CAT_CATEGORIA objeto)
        {
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                contexto.CAT_CATEGORIA.Remove(objeto);
                contexto.SaveChanges();
            }
            return objeto;
        }

        public bool Existe(CAT_CATEGORIA objeto)
        {
            var resultado = false;
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                if (contexto.CAT_CATEGORIA.Where(cat => cat.Nombre == objeto.Nombre).Count() > 0)
                {
                    resultado = true;
                }
            }
            return resultado;
        }

        public CAT_CATEGORIA Insertar(CAT_CATEGORIA objeto)
        {
            var insertado = new CAT_CATEGORIA();
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                contexto.CAT_CATEGORIA.Add(objeto);
                contexto.SaveChanges();
                insertado = contexto.CAT_CATEGORIA.Where(cat => cat.Nombre == objeto.Nombre).FirstOrDefault();
            }
            return insertado;
        }
        
    }
}