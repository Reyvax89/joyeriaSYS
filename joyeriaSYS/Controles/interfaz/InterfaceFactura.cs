using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace joyeriaSYS.Controles.interfaz
{
        interface InterfaceFactura<T>
        {
            T Insertar(T objeto);
            T Eliminar(T objeto);
            T Actualizar(T objeto);
            IEnumerable<T> ConsultarPorId(T objeto);
            IEnumerable<T> Consultar();
            Boolean Existe(T objeto);
        }
    }
