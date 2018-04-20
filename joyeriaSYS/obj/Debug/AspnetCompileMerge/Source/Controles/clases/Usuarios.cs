using joyeriaSYS.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace joyeriaSYS.Controles.clases
{
    public class Usuarios : interfaz.InterfaceFactura<AspNetUsers>
    {
        public AspNetUsers Actualizar(AspNetUsers objeto)
        {
            var actualizado = new AspNetUsers();
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                actualizado = contexto.AspNetUsers.Where(user => user.Id == objeto.Id).FirstOrDefault();
                actualizado.PhoneNumber = objeto.PhoneNumber;
                actualizado.PasswordHash = objeto.PasswordHash;
                actualizado.Apellido1 = objeto.Apellido1;
                actualizado.Nombre = objeto.Nombre;
                actualizado.UserName = objeto.UserName;
                actualizado.IdRol = objeto.IdRol;
                contexto.SaveChanges();
            }
            return actualizado;
        }

        public IEnumerable<AspNetUsers> Consultar()
        {
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                return contexto.AspNetUsers.ToList();
            }
        }

        public IEnumerable<AspNetUsers> ConsultarPorId(AspNetUsers objeto)
        {
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                return contexto.AspNetUsers.Where(user => user.Id == objeto.Id).ToList();
            }
        }

        public IEnumerable<AspNetUsers> ConsultarPorUserName(AspNetUsers objeto)
        {
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                return contexto.AspNetUsers.Where(user => user.UserName == objeto.UserName).ToList();
            }
        }

        public AspNetUsers ConsultarPorNombrePassword(AspNetUsers objeto)
        {
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                return contexto.AspNetUsers.Where(user => user.PasswordHash == objeto.PasswordHash && user.UserName == objeto.UserName).FirstOrDefault();
            }
        }

        public AspNetUsers Eliminar(AspNetUsers objeto)
        {
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                contexto.AspNetUsers.Remove(objeto);
                contexto.SaveChanges();
            }
            return objeto;
        }

        public bool Existe(AspNetUsers objeto)
        {
            var resultado = false;
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                if (contexto.AspNetUsers.Where(user => user.UserName == objeto.UserName).Count() > 0)
                {
                    resultado = true;
                }
            }
            return resultado;
        }

        public AspNetUsers Insertar(AspNetUsers objeto)
        {
            var insertado = new AspNetUsers();
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                contexto.AspNetUsers.Add(objeto);
                contexto.SaveChanges();
                insertado = contexto.AspNetUsers.Where(user => user.UserName == objeto.UserName).FirstOrDefault();
            }
            return insertado;
        }
        
    }
}