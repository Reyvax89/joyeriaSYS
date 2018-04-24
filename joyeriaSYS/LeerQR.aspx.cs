using System;
using Gma.QrCodeNet.Encoding;
using System.Drawing;
using System.IO;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System.Drawing.Imaging;
using System.Data;
using System.Web.UI;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using joyeriaSYS.Controles.clases;
using joyeriaSYS.Models;
using joyeriaSYS.Enum;

namespace joyeriaSYS
{
    public partial class LeerQR : Page
    {
        private Producto objProd = new Producto();
        private Clientes objCli = new Clientes();
        private Factura objFact = new Factura();
        private Categoria objCat = new Categoria();
        private DetalleFactura objDeF = new DetalleFactura();
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["username"] == "" || Session["username"] == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                List<string> pages = new List<string>();
                pages = (List<string>)Session["paginas"];
                if (!pages.Exists(x => string.Equals(x, "LeerQR", StringComparison.OrdinalIgnoreCase)))
                {
                    Response.Redirect("~/AccesoDenegado.aspx");
                }
            }
            if (!IsPostBack)
            {
                cargarFacturas();
                Session["marcados"] = new List<int> { };
                Session["catidadesActuales"] = new List<int> { };
            }
        }
        #region Eventos
        protected void btnCalcular_Click(object sender, EventArgs e)
        {
            try
            {
                var tempFactura = new FAC_FACTURA();
                tempFactura.NoFactura = Convert.ToInt32(ddlFactura.SelectedValue);
                tempFactura = objFact.ConsultaPorNumeroDeFactura(tempFactura);
                var SaldoActual = tempFactura.saldo;

                foreach (GridViewRow mRow in gvwDetalleFactura.Rows)
                {
                    CheckBox mCheck = (CheckBox)mRow.FindControl("cbSelect");
                    if (mCheck != null)
                    {
                        if (mCheck.Checked)
                        {
                            var tempProducto = new PRO_PRODUCTO();
                            tempProducto.CodigoNumerico = Convert.ToInt32(mRow.Cells[1].Text);
                            tempProducto = objProd.ConsultarPorCodigoProducto(tempProducto);
                            tempProducto.Inventario = tempProducto.Inventario + 1;
                            SaldoActual = SaldoActual - tempProducto.Precio;
                            txtSaldoActual.Text = SaldoActual.ToString();
                            Session["SaldoActual"] = SaldoActual;
                            var tempDetalleFactura = new DEF_DETALLE_FACTURA();
                            tempDetalleFactura.idFactura = tempFactura.idFactura;
                            tempDetalleFactura.idProducto = tempProducto.IdProducto;
                            tempDetalleFactura = objDeF.ConsultarPorIdFacturaYIdProducto(tempDetalleFactura);
                            tempDetalleFactura.CantidadDevuelta = tempDetalleFactura.CantidadDevuelta + 1;

                            objProd.Actualizar(tempProducto);
                            objDeF.Actualizar(tempDetalleFactura);
                            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + mRow.Cells[2].Text + "')", true);
                        }
                    }
                }
                tempFactura.saldo = SaldoActual;
                tempFactura.estado = Convert.ToInt32(EstadoFacturas.Cancelada);
                tempFactura.totalDevuelto = tempFactura.saldo - SaldoActual;
                objFact.Actualizar(tempFactura);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                throw ex;
            }
        }

        //protected void txtCodigo_TextChanged(object sender, EventArgs e)
        //{
        //    var codProducto = (txtCodigo.Text != "") ? Convert.ToInt32(txtCodigo.Text) : 0;
        //    var codFactura = (ddlFactura.Text != "") ? Convert.ToInt32(ddlFactura.Text) : 0;
        //    if (codFactura != 0 && codProducto != 0)
        //    {
        //        gestionarCargaDatos(codProducto, codFactura);
        //        txtCodigo.Text = "";
        //    }
        //}

        protected void ddlFactura_DropDownChanged(object sender, EventArgs e)
        {
            //var codProducto = (txtCodigo.Text != "") ? Convert.ToInt32(txtCodigo.Text) : 0;
            var codFactura = (ddlFactura.Text != "") ? Convert.ToInt32(ddlFactura.Text) : 0;
            if (codFactura != 0)
            {
                //if(codProducto != 0)
                //{
                //    gestionarCargaDatos(codProducto, codFactura);
                //}else
                //{
                    gestionarCargaDatos(codFactura);
                //}
            }
        }
        protected void btnCalcularSaldo_Click(object sender, EventArgs e)
        {
            try
            {
                var tempFactura = new FAC_FACTURA();
                tempFactura.NoFactura = Convert.ToInt32(ddlFactura.SelectedValue);
                tempFactura = objFact.ConsultaPorNumeroDeFactura(tempFactura);
                var SaldoActual = tempFactura.saldo;

                foreach (GridViewRow mRow in gvwDetalleFactura.Rows)
                {
                    CheckBox mCheck = (CheckBox)mRow.FindControl("cbSelect");
                    if (mCheck != null)
                    {
                        if (mCheck.Checked)
                        {
                            var tempProducto = new PRO_PRODUCTO();
                            tempProducto.CodigoNumerico = Convert.ToInt32(mRow.Cells[1].Text);
                            tempProducto = objProd.ConsultarPorCodigoProducto(tempProducto);
                            SaldoActual = SaldoActual - tempProducto.Precio;
                            txtSaldoActual.Text = SaldoActual.ToString();
                            Session["SaldoActual"] = SaldoActual;
                            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + mRow.Cells[2].Text + "')", true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                throw ex;
            }
        }
        
        #endregion
        #region Metodos privados
        private void gestionarCargaDatos(int codProducto, int codFactura)
        {
            try
            {
                var facturaActual = new FAC_FACTURA();
                var detFacturaActual = new DEF_DETALLE_FACTURA();
                var productoActual = new PRO_PRODUCTO();
                productoActual.CodigoNumerico = codProducto;
                productoActual = objProd.ConsultarPorCodigoProducto(productoActual);
                facturaActual.NoFactura = codFactura;
                facturaActual = objFact.ConsultaPorNumeroDeFactura(facturaActual);
                detFacturaActual.idFactura = facturaActual.idFactura;
                detFacturaActual.idProducto = productoActual.IdProducto;
                detFacturaActual = objDeF.ConsultarPorIdFacturaYIdProducto(detFacturaActual);

                txtSaldoActual.Text = Session["SaldoActual"].ToString();
                CargarTablaDetalleFacturas(facturaActual.idFactura, txtCriterio.Text);
                //poner en detalle un campo para ir descontando los productos escaneados

                cargarTablaFacturas(codFactura);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
        }
        private void gestionarCargaDatos(int codFactura)
        {
            try
            {
                var facturaActual = new FAC_FACTURA();
                var detFacturaActual = new DEF_DETALLE_FACTURA();
                facturaActual.NoFactura = codFactura;
                facturaActual = objFact.ConsultaPorNumeroDeFactura(facturaActual);
                
                CargarTablaDetalleFacturas(facturaActual.idFactura, txtCriterio.Text);
                txtSaldoActual.Text = facturaActual.saldo.ToString();

                //poner en detalle un campo para ir descontando los productos escaneados
                Session["SaldoActual"] = facturaActual.saldo.ToString();
                cargarTablaFacturas(codFactura);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
        }
        
        private void cargarTablaFacturas(int codFactura)
        {
            try
            {
                var facturaActual = new FAC_FACTURA();
                facturaActual.NoFactura = codFactura;
                var dt = new DataTable();
                var row = objFact.ConsultaPorNumeroDeFactura(facturaActual);

                dt.Columns.Add("idFactura", typeof(System.String));
                dt.Columns.Add("NoFactura", typeof(System.String));
                dt.Columns.Add("montoFactura", typeof(System.String));
                dt.Columns.Add("estado", typeof(System.String));
                dt.Columns.Add("totalPiezas", typeof(System.String));
                dt.Columns.Add("idCliente", typeof(System.String));
                //dt.Columns.Add("fechaCreacion", typeof(System.String));
                //dt.Columns.Add("fechaLiquidacion", typeof(System.String));

                DataRow fila = dt.NewRow();
                var tempCliente = new CLI_CLIENTES();
                tempCliente.idCliente = row.idCliente;
                tempCliente = objCli.ConsultarPorIdCliente(tempCliente);

                fila["idFactura"] = row.idFactura;
                fila["NoFactura"] = row.NoFactura;
                fila["montoFactura"] = row.montoFactura;
                fila["estado"] = row.estado;
                fila["totalPiezas"] = row.totalPiezas;
                fila["idCliente"] = tempCliente.NombreEncargado;
                //fila["fechaCreacion"] = r.fechaCreacion;
                //fila["fechaLiquidacion"] = r.fechaLiquidacion;
                dt.Rows.Add(fila);
                gvwFacturas.DataSource = dt;
                gvwFacturas.DataBind();
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
        }

        private void cargarFacturas()
        {
            try
            {
                var dt = new DataTable();
                var rows = objFact.ConsultarFacturasFinalizadas();

                ddlFactura.DataTextField = "NoFactura";
                ddlFactura.DataValueField = "NoFacturaValue";
                dt.Columns.Add("NoFactura", typeof(System.String));
                dt.Columns.Add("NoFacturaValue", typeof(System.String));
                DataRow filaInicio = dt.NewRow();

                filaInicio["NoFactura"] = "Seleccione una factura";
                filaInicio["NoFacturaValue"] = -1;
                dt.Rows.Add(filaInicio);
                foreach (FAC_FACTURA r in rows)
                {
                    DataRow fila = dt.NewRow();

                    fila["NoFactura"] = r.NoFactura;
                    fila["NoFacturaValue"] = r.NoFactura;
                    dt.Rows.Add(fila);
                }
                ddlFactura.DataSource = dt;
                ddlFactura.DataBind();
                ddlFactura.SelectedValue = "-1";
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
        }
        
        private void CargarTablaDetalleFacturas(int idFactura, string criterio)
        {
            try
            {
                var dt = new DataTable();
                var rows = objDeF.ConsultarPorIdFactura(-1,"");
                gvwDetalleFactura.DataSource = null;
                gvwDetalleFactura.DataBind();
                if (idFactura != -1)
                {
                    rows = objDeF.ConsultarPorIdFactura(idFactura, criterio);
                }

                //dt.Columns.Add("idFactura", typeof(System.String));
                dt.Columns.Add("idProducto", typeof(System.String));
                dt.Columns.Add("codProducto", typeof(System.String));
                dt.Columns.Add("CantidadProducto", typeof(System.String));
                dt.Columns.Add("Regresado", typeof(System.String));
                
                foreach (Vista_ProductosPorDetalleFactura r in rows)
                {
                    // Crear una fila por cada unidad del producto.
                    int cantidadDeProductos = r.CantidadProducto;
                    int cantidadDeDevueltos = r.CantidadDevuelta;
                 
                    for (int i = 0; i < cantidadDeProductos; i++)
                    {
                        var tempCategoria = new CAT_CATEGORIA();

                        tempCategoria.idCategoria = r.IdCategoria;
                        tempCategoria = objCat.ConsultarPorIdCategoria(tempCategoria);
                        // Crear la fila, asignar valores y agregarla.
                        DataRow fila = dt.NewRow();
                        //fila["idFactura"] = r.idFactura;
                        fila["idProducto"] = r.NombreProducto +", "+ tempCategoria.Nombre;
                        
                        fila["codProducto"] = r.CodigoNumerico;
                        // La catidad siempre va ser 1.
                        fila["CantidadProducto"] = "1";
                        // Ver si esta marcado.
                        if(cantidadDeDevueltos > 0)
                        {
                            fila["Regresado"] = "true";
                            cantidadDeDevueltos--;
                        }else
                        {
                            fila["Regresado"] = "false";
                        }
                        dt.Rows.Add(fila);
                    }
                }
                gvwDetalleFactura.DataSource = dt;
                gvwDetalleFactura.DataBind();
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
        }
        #endregion
        #region Metodos publicos
        public List<int> getListaMarcados ()
        {
            return Session["marcados"] as List<int>;
        }

        public void setListaMarcados(List<int> marcados)
        {
            Session["marcados"] = marcados;
        }
        public bool revisarNumeroEnListaMarcados(int numero)
        {
            List<int> marcados = Session["marcados"] as List<int>;
            foreach (int i in marcados)
            {
                if (i == numero)
                {
                    return true;
                }
            }
            return false;
        }
        public List<int> getListaCantidades()
        {
            return Session["catidadesActuales"] as List<int>;
        }

        public void setListaCantidades(List<int> cantidades)
        {
            Session["catidadesActuales"] = cantidades;
        }

        public List<double> getListaProductosDevueltos()
        {
            return Session["valorDelProducto"] as List<double>;
        }

        public void setListaProductosDevueltos(List<double> ValorDevuelto)
        {
            Session["valorDelProducto"] = ValorDevuelto;
        }
        #endregion

        protected void txtCriterio_TextChanged(object sender, EventArgs e)
        {
            CargarTablaDetalleFacturas(Convert.ToInt32(ddlFactura.SelectedValue), txtCriterio.Text);
        }
    }
}