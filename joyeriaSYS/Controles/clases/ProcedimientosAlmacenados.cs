using joyeriaSYS.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace joyeriaSYS.Controles.clases
{
    public class ProcedimientosAlmacenados
    {
        public int SP_CantidadProductosEnLaFactura(int NoFactura)
        {
            var Cantidad = 0;
            try
            {
                using (JoyeriaEntities contexto = new JoyeriaEntities())
                {
                    var sqlQuery = "SP_CantidadProductosEnLaFactura @NoFactura";
                    SqlParameter[] sqlParams;

                    sqlParams = new SqlParameter[]
                    {
                new SqlParameter { ParameterName = "@NoFactura",  Value =NoFactura , Direction = System.Data.ParameterDirection.Input}
                    };

                    Cantidad = contexto.Database.SqlQuery<int>(sqlQuery, sqlParams).SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
            return Cantidad;
        }

        public decimal SP_SaldoDeFacturaCancelada(int NoFactura)
        {
            decimal Cantidad = 0;
            try
            {
                using (JoyeriaEntities contexto = new JoyeriaEntities())
                {
                    var sqlQuery = "SP_SaldoDeFacturaCancelada @NoFactura";
                    SqlParameter[] sqlParams;

                    sqlParams = new SqlParameter[]
                    {
                new SqlParameter { ParameterName = "@NoFactura",  Value =NoFactura , Direction = System.Data.ParameterDirection.Input}
                    };

                    Cantidad = contexto.Database.SqlQuery<decimal>(sqlQuery, sqlParams).SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
            return Cantidad;
        }

        public decimal SP_SaldoDeFacturaCanceladaSINActualizarLaFacturaAun(int NoFactura)
        {
            decimal Cantidad = 0;
            try
            {
                using (JoyeriaEntities contexto = new JoyeriaEntities())
                {
                    var sqlQuery = "SP_SaldoDeFacturaCanceladaSINActualizarLaFacturaAun @NoFactura";
                    SqlParameter[] sqlParams;

                    sqlParams = new SqlParameter[]
                    {
                new SqlParameter { ParameterName = "@NoFactura",  Value =NoFactura , Direction = System.Data.ParameterDirection.Input}
                    };

                    Cantidad = contexto.Database.SqlQuery<decimal>(sqlQuery, sqlParams).SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
            return Cantidad;
        }

        public decimal SP_SaldoDeFacturaNOCancelada(int NoFactura)
        {
            decimal Cantidad = 0;
            try
            {
                using (JoyeriaEntities contexto = new JoyeriaEntities())
                {
                    var sqlQuery = "SP_SaldoDeFacturaNOCancelada @NoFactura";
                    SqlParameter[] sqlParams;

                    sqlParams = new SqlParameter[]
                    {
                new SqlParameter { ParameterName = "@NoFactura",  Value =NoFactura , Direction = System.Data.ParameterDirection.Input}
                    };

                    Cantidad = contexto.Database.SqlQuery<decimal>(sqlQuery, sqlParams).SingleOrDefault();
                }
            }catch (Exception ex)
            {
                var err = ex.Message;
            }
            
            return Cantidad;
        }
    }
}