using joyeriaSYS.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace joyeriaSYS.Controles.clases
{
    public class Clientes : interfaz.InterfaceFactura<CLI_CLIENTES>
    {
        public CLI_CLIENTES Actualizar(CLI_CLIENTES objeto)
        {
            var actualizado = new CLI_CLIENTES();
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                actualizado = contexto.CLI_CLIENTES.Where(cat => cat.idCliente == objeto.idCliente).FirstOrDefault();
                actualizado.NombreEncargado = objeto.NombreEncargado;
                actualizado.NombreJoyeria = objeto.NombreJoyeria;
                actualizado.Provincia = objeto.Provincia;
                actualizado.Telefono = objeto.Telefono;
                actualizado.Canton = objeto.Canton;
                actualizado.Celular = objeto.Celular;
                actualizado.Direccion = objeto.Direccion;
                contexto.SaveChanges();
            }
            return actualizado;
        }

        public IEnumerable<CLI_CLIENTES> Consultar()
        {
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                return contexto.CLI_CLIENTES.ToList();
            }
        }

        public IEnumerable<CLI_CLIENTES> ConsultarPorId(CLI_CLIENTES objeto)
        {
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                return contexto.CLI_CLIENTES.Where(cat => cat.idCliente == objeto.idCliente).ToList();
            }
        }

        public IEnumerable<CLI_CLIENTES> ConsultarPorNombre(CLI_CLIENTES objeto)
        {
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                return contexto.CLI_CLIENTES.Where(cat => cat.NombreEncargado == objeto.NombreEncargado).ToList();
            }
        }

        public CLI_CLIENTES Eliminar(CLI_CLIENTES objeto)
        {
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                contexto.CLI_CLIENTES.Remove(objeto);
                contexto.SaveChanges();
            }
            return objeto;
        }

        public bool Existe(CLI_CLIENTES objeto)
        {
            var resultado = false;
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                if (contexto.CLI_CLIENTES.Where(cli => cli.NombreEncargado == objeto.NombreEncargado).Count() > 0)
                {
                    resultado = true;
                }
            }
            return resultado;
        }

        public CLI_CLIENTES Insertar(CLI_CLIENTES objeto)
        {
            var insertado = new CLI_CLIENTES();
            using (JoyeriaEntities contexto = new JoyeriaEntities())
            {
                contexto.CLI_CLIENTES.Add(objeto);
                contexto.SaveChanges();
                insertado = contexto.CLI_CLIENTES.Where(cat => cat.NombreJoyeria == objeto.NombreJoyeria).FirstOrDefault();
            }
            return insertado;
        }
        
    }
}