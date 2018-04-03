using joyeriaSYS.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace joyeriaSYS.Controles.clases
{
    public class Roles : interfaz.InterfaceFactura<AspNetRoles>
    {
        public AspNetRoles Actualizar(AspNetRoles objeto)
        {
            var actualizado = new AspNetRoles();
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                actualizado = contexto.AspNetRoles.Where(user => user.Id == objeto.Id).FirstOrDefault();
                actualizado.Name = objeto.Name;
                contexto.SaveChanges();
            }
            return actualizado;
        }

        public IEnumerable<AspNetRoles> Consultar()
        {
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                return contexto.AspNetRoles.ToList();
            }
        }

        public IEnumerable<AspNetRoles> ConsultarPorId(AspNetRoles objeto)
        {
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                return contexto.AspNetRoles.Where(user => user.Id == objeto.Id).ToList();
            }
        }

        public AspNetRoles Eliminar(AspNetRoles objeto)
        {
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                contexto.AspNetRoles.Remove(objeto);
                contexto.SaveChanges();
            }
            return objeto;
        }

        public bool Existe(AspNetRoles objeto)
        {
            throw new NotImplementedException();
        }

        public AspNetRoles Insertar(AspNetRoles objeto)
        {
            var insertado = new AspNetRoles();
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                contexto.AspNetRoles.Add(objeto);
                contexto.SaveChanges();
                insertado = contexto.AspNetRoles.Where(user => user.Name == objeto.Name).FirstOrDefault();
            }
            return insertado;
        }
        
    }
}