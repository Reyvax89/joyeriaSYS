using joyeriaSYS.Controles.clases;
using joyeriaSYS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace joyeriaSYS
{
    public partial class frmCrearFactura : System.Web.UI.Page
    {
        private Producto objProd = new Producto();
        private Categoria objCat = new Categoria();
        private Clientes objCli = new Clientes();
        private Factura objFact = new Factura();
        private DetalleFactura objDeF = new DetalleFactura();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["Factura"] = "-1";
                cargarClientes();
                cargarProductos();
                CargarTablaFacturas();
            }
        }
        protected void gvwFacturas_SelectedIndexChanged(object sender, EventArgs e)
        {
            hdfIdFactura.Value = gvwFacturas.SelectedRow.Cells[0].Text;
            //txtCodTabla.Text = gvwFacturas.SelectedRow.Cells[2].Text;
            txtCodFactura.Text = gvwFacturas.SelectedRow.Cells[1].Text;
            var tempCliente = new CLI_CLIENTES();
            tempCliente.NombreEncargado = gvwFacturas.SelectedRow.Cells[5].Text;
            tempCliente = objCli.ConsultarPorNombre(tempCliente).FirstOrDefault();
            ddlCliente.SelectedValue = tempCliente.idCliente.ToString();
            CargarTablaDetalleFacturas(Convert.ToInt32(hdfIdFactura.Value));
        }

        protected void gvwDetalleFactura_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tempProducto = new PRO_PRODUCTO();
            tempProducto.NombreProducto = gvwDetalleFactura.SelectedRow.Cells[2].Text;
            tempProducto = objProd.ConsultarPorNombre(tempProducto).FirstOrDefault();

            var tempCategoria = new CAT_CATEGORIA();
            tempCategoria.Nombre = gvwDetalleFactura.SelectedRow.Cells[4].Text;
            tempCategoria = objCat.ConsultarPorNombre(tempCategoria).FirstOrDefault();

            hdfIdDetalleFactura.Value = gvwDetalleFactura.SelectedRow.Cells[0].Text;
            int noFactura = Convert.ToInt32(gvwDetalleFactura.SelectedRow.Cells[1].Text);
            int idProducto = tempProducto.IdProducto;
            int idCategoria = tempCategoria.idCategoria;
            int Cantidad = Convert.ToInt32(gvwDetalleFactura.SelectedRow.Cells[5].Text);

            ddlProducto.SelectedValue = idProducto.ToString();

            var detFac = new DEF_DETALLE_FACTURA();
            detFac.idDetalleFactura = Convert.ToInt32(hdfIdDetalleFactura.Value);
            actualizarFacturaLuegoDeBorrado(noFactura, idProducto, Cantidad);
            actualizarCantidadProducto(idProducto, Cantidad, false);
            objDeF.Eliminar(detFac);

            CargarTablaDetalleFacturas(Convert.ToInt32(hdfIdFactura.Value));
            CargarTablaFacturas();
        }

        private void actualizarFacturaLuegoDeBorrado(int noFactura, int idProducto, int cantidad)
        {
            var tempFactura = new FAC_FACTURA();
            tempFactura.NoFactura = noFactura;
            tempFactura = objFact.ConsultaPorNumeroDeFactura(tempFactura);
            tempFactura.montoFactura = tempFactura.montoFactura - calcularMonto(idProducto, cantidad);
            tempFactura.totalPiezas = tempFactura.totalPiezas - cantidad;
            tempFactura.saldo = tempFactura.saldo - calcularMonto(idProducto, cantidad);
            objFact.Actualizar(tempFactura);
        }

        private void cargarClientes()
        {
            try
            {
                var dt = new DataTable();
                var rows = objCli.Consultar();

                ddlCliente.DataTextField = "NombreEncargado";
                ddlCliente.DataValueField = "idCliente";
                dt.Columns.Add("idCliente", typeof(System.String));
                dt.Columns.Add("NombreEncargado", typeof(System.String));

                foreach (CLI_CLIENTES r in rows)
                {
                    DataRow fila = dt.NewRow();

                    fila["idCliente"] = r.idCliente;
                    fila["NombreEncargado"] = r.NombreEncargado;
                    dt.Rows.Add(fila);
                }
                ddlCliente.DataSource = dt;
                ddlCliente.DataBind();
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
        }

        private void cargarProductos()
        {
            try
            {
                var dt = new DataTable();
                var rows = objProd.Consultar();

                ddlProducto.DataTextField = "NombreProducto";
                ddlProducto.DataValueField = "IdProducto";
                dt.Columns.Add("IdProducto", typeof(System.String));
                dt.Columns.Add("NombreProducto", typeof(System.String));

                foreach (PRO_PRODUCTO r in rows)
                {
                    var tempCategoria = new CAT_CATEGORIA();
                    tempCategoria.idCategoria = r.IdCategoria;
                    tempCategoria = objCat.ConsultarPorId(tempCategoria).FirstOrDefault();

                    DataRow fila = dt.NewRow();

                    fila["IdProducto"] = r.IdProducto;
                    fila["NombreProducto"] = r.NombreProducto + " " + tempCategoria.Nombre + " " + r.CodigoNumerico;
                    dt.Rows.Add(fila);
                }
                ddlProducto.DataSource = dt;
                ddlProducto.DataBind();
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
        }

        private void CargarTablaDetalleFacturas(int idFactura)
        {
            try
            {
                var dt = new DataTable();
                var rows = objDeF.Consultar();
                gvwDetalleFactura.DataSource = null;
                gvwDetalleFactura.DataBind();

                dt.Columns.Add("idDetalleFactura", typeof(System.String));
                dt.Columns.Add("idFactura", typeof(System.String));
                dt.Columns.Add("idProducto", typeof(System.String));
                dt.Columns.Add("CodProducto", typeof(System.String));
                dt.Columns.Add("idCategoria", typeof(System.String));
                dt.Columns.Add("CantidadProducto", typeof(System.String));
                //dt.Columns.Add("Precio", typeof(System.String));
                //dt.Columns.Add("Inventario", typeof(System.String));

                if (idFactura != -1){
                    rows = objDeF.ConsultarPorIdFactura(idFactura);

                    foreach (DEF_DETALLE_FACTURA r in rows)
                    {
                        var tempCategoria = new CAT_CATEGORIA();
                        var tempProducto = new PRO_PRODUCTO();
                        var tempFactura = new FAC_FACTURA();

                        tempFactura.idFactura = r.idFactura;
                        tempFactura = objFact.ConsultarPorId(tempFactura).FirstOrDefault();
                        tempProducto.IdProducto = r.idProducto;
                        tempProducto = objProd.ConsultarPorId(tempProducto).FirstOrDefault();
                        tempCategoria.idCategoria = tempProducto.IdCategoria;
                        DataRow fila = dt.NewRow();

                        fila["idDetalleFactura"] = r.idDetalleFactura;
                        fila["idFactura"] = tempFactura.NoFactura;
                        fila["idProducto"] = tempProducto.NombreProducto;
                        fila["CodProducto"] = tempProducto.CodigoNumerico;
                        fila["idCategoria"] = objCat.ConsultarPorId(tempCategoria).FirstOrDefault().Nombre;
                        fila["CantidadProducto"] = r.CantidadProducto;
                        dt.Rows.Add(fila);
                    }
                }else
                {
                    DataRow fila = dt.NewRow();

                    fila["idDetalleFactura"] = "---";
                    fila["idFactura"] = "---";
                    fila["idProducto"] = "---";
                    fila["CodProducto"] = "---";
                    fila["idCategoria"] = "---";
                    fila["CantidadProducto"] = "---";
                    dt.Rows.Add(fila);
                }
                
                gvwDetalleFactura.DataSource = dt;
                gvwDetalleFactura.DataBind();
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
        }

        private void CargarTablaFacturas()
        {
            try
            {
                var dt = new DataTable();
                var rows = objFact.Consultar();

                dt.Columns.Add("idFactura", typeof(System.String));
                dt.Columns.Add("NoFactura", typeof(System.String));
                //dt.Columns.Add("CodTabla", typeof(System.String));
                dt.Columns.Add("montoFactura", typeof(System.String));
                dt.Columns.Add("estado", typeof(System.String));
                dt.Columns.Add("totalPiezas", typeof(System.String));
                dt.Columns.Add("idCliente", typeof(System.String));
                //dt.Columns.Add("fechaCreacion", typeof(System.String));
                //dt.Columns.Add("fechaLiquidacion", typeof(System.String));

                foreach (FAC_FACTURA r in rows)
                {
                    var tempCliente = new CLI_CLIENTES();
                    tempCliente.idCliente = r.idCliente;
                    tempCliente = objCli.ConsultarPorId(tempCliente).FirstOrDefault();
                    DataRow fila = dt.NewRow();

                    fila["idFactura"] = r.idFactura;
                    fila["NoFactura"] = r.NoFactura;
                    //fila["CodTabla"] = r.CodTabla;
                    fila["montoFactura"] = r.montoFactura;
                    fila["estado"] = r.estado;
                    fila["totalPiezas"] = r.totalPiezas;
                    fila["idCliente"] = tempCliente.NombreEncargado;
                    //fila["fechaCreacion"] = r.fechaCreacion;
                    //fila["fechaLiquidacion"] = r.fechaLiquidacion;
                    dt.Rows.Add(fila);
                }
                gvwFacturas.DataSource = dt;
                gvwFacturas.DataBind();
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
        }
        protected void btnInsertarActualizar_Click(object sender, EventArgs e)
        {
            var idFactura = 0;
            if(Convert.ToInt32(hdfIdFactura.Value) == -1)
            {
                idFactura = guardarFactura();
                hdfIdFactura.Value = "" + idFactura;
            }
            else
            {
                idFactura = Convert.ToInt32(hdfIdFactura.Value);
                actualizarFacturaYaInsertada(Convert.ToInt32(hdfIdFactura.Value), Convert.ToInt32(txtCantidad.Text), Convert.ToInt32(ddlProducto.SelectedValue));
            }
            var nuevoDetalle = new DEF_DETALLE_FACTURA();
            nuevoDetalle.CantidadProducto = Convert.ToInt32(txtCantidad.Text);
            nuevoDetalle.idFactura = idFactura;
            nuevoDetalle.idProducto = Convert.ToInt32(ddlProducto.SelectedValue);
            actualizarCantidadProducto(nuevoDetalle.idProducto, nuevoDetalle.CantidadProducto, true);
            objDeF.Insertar(nuevoDetalle);
            txtCantidad.Text = "";
            CargarTablaDetalleFacturas(idFactura);
            CargarTablaFacturas();
        }

        private void actualizarFacturaYaInsertada(int idFactura, int cantidad, int idProducto)
        {
            var tempFactura = new FAC_FACTURA();
            tempFactura.idFactura = idFactura;
            tempFactura = objFact.ConsultarPorId(tempFactura).FirstOrDefault();

            tempFactura.montoFactura = tempFactura.montoFactura + calcularMonto(idProducto, cantidad);
            tempFactura.saldo = tempFactura.montoFactura;
            tempFactura.totalDevuelto = 0;
            tempFactura.totalPiezas = tempFactura.totalPiezas + cantidad;

            tempFactura = objFact.Actualizar(tempFactura);
        }

        private void actualizarCantidadProducto(int idProducto, int cantidadProducto, Boolean resta)
        {
            var tempPro = new PRO_PRODUCTO();
            tempPro.IdProducto = idProducto;
            tempPro = objProd.ConsultarPorId(tempPro).FirstOrDefault();
            if (resta)
            {
                tempPro.Inventario = tempPro.Inventario - cantidadProducto;
            }else
            {
                tempPro.Inventario = tempPro.Inventario + cantidadProducto;
            }
            
            objProd.Actualizar(tempPro);
        }

        private decimal calcularMonto(int idProducto, int cantidad)
        {
            var tempProd = new PRO_PRODUCTO();
            tempProd.IdProducto = idProducto;

            tempProd = objProd.ConsultarPorId(tempProd).FirstOrDefault();

            return tempProd.Precio * cantidad;
        }

        public int guardarFactura()
        {
            var nuevaFactura = new FAC_FACTURA();
            nuevaFactura.CodTabla = txtCodFactura.Text;
            nuevaFactura.estado = false;
            nuevaFactura.idCliente = Convert.ToInt32(ddlCliente.SelectedValue);
            nuevaFactura.montoFactura = calcularMonto(Convert.ToInt32(ddlProducto.SelectedValue), Convert.ToInt32(txtCantidad.Text));
            nuevaFactura.NoFactura = Convert.ToInt32(txtCodFactura.Text);
            nuevaFactura.fechaCreacion = DateTime.Now;
            nuevaFactura.fechaLiquidacion = DateTime.Now;
            nuevaFactura.saldo = nuevaFactura.montoFactura;
            nuevaFactura.totalDevuelto = 0;
            nuevaFactura.totalPiezas = Convert.ToInt32(txtCantidad.Text);

            nuevaFactura = objFact.Insertar(nuevaFactura);

            return nuevaFactura.idFactura;
        }

        protected void btnFinalizar_Click(object sender, EventArgs e)
        {
            hdfIdDetalleFactura.Value = "-1";
            hdfIdFactura.Value = "-1";
            if(txtCodFactura.Text != "")
            {
                Session["Factura"] = txtCodFactura.Text;
            }
            Response.Redirect("frmImprimirFacturas.aspx");
        }

        protected void gvwDetalleFactura_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvwDetalleFactura.PageIndex = e.NewPageIndex;
            CargarTablaDetalleFacturas(Convert.ToInt32(hdfIdFactura.Value));
        }

        protected void gvwFacturas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvwFacturas.PageIndex = e.NewPageIndex;
            CargarTablaFacturas();
        }

        protected void btnNuevaFactura_Click(object sender, EventArgs e)
        {
            //cargarClientes();
            //cargarProductos();
            //CargarTablaFacturas();
            //CargarTablaDetalleFacturas(Convert.ToInt32(hdfIdFactura.Value));
            txtCodFactura.Text = "";
            txtCantidad.Text = "";
            hdfIdDetalleFactura.Value = "-1";
            hdfIdFactura.Value = "-1";
            Session["Factura"] = "-1";
        }

        //private void ExportToExcel(string nameReport, GridView wControl)
        //{
        //    HttpResponse response = Response;
        //    StringWriter sw = new StringWriter();
        //    HtmlTextWriter htw = new HtmlTextWriter(sw);
        //    System.Web.UI.Page pageToRender = new System.Web.UI.Page();
        //    HtmlForm form = new HtmlForm();
        //    form.Controls.Add(wControl);
        //    pageToRender.Controls.Add(form);
        //    response.Clear();
        //    response.Buffer = true;
        //    response.ContentType = "application/vnd.ms-excel";
        //    response.AddHeader("Content-Disposition", "attachment;filename=" + nameReport);
        //    response.Charset = "UTF-8";
        //    response.ContentEncoding = Encoding.Default;
        //    pageToRender.RenderControl(htw);
        //    response.Write(sw.ToString());
        //    response.End();
        //}

    }//Fin de la clase
}