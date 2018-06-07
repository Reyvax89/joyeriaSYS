using joyeriaSYS.Controles.clases;
using joyeriaSYS.Enum;
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
using Excel = Microsoft.Office.Interop.Excel;

namespace joyeriaSYS
{
    public partial class frmCrearFactura : System.Web.UI.Page
    {
        private Producto objProd = new Producto();
        private Categoria objCat = new Categoria();
        private Clientes objCli = new Clientes();
        private Factura objFact = new Factura();
        private DetalleFactura objDeF = new DetalleFactura();
        private Usuarios objUsu = new Usuarios();
        private ProcedimientosAlmacenados objProcedimientos = new ProcedimientosAlmacenados();
        private string[,] arregloTemporal = new string[35, 3];
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
                if (!pages.Exists(x => string.Equals(x, "frmCrearFactura", StringComparison.OrdinalIgnoreCase)))
                {
                    Response.Redirect("~/AccesoDenegado.aspx");
                }
            }
            if (!IsPostBack)
            {
                Session["Factura"] = "-1";
                cargarClientes();
                cargarMetales();
                cargarCategorias("1");
                ddlMetal.SelectedValue = "1";
                ddlProducto.Items.Add("-Ninguno-");
                CargarTablaFacturas(txtCriterio.Text);
            }
        }
        #region Eventos
        protected void gvwFacturas_SelectedIndexChanged(object sender, EventArgs e)
        {
            hdfIdFactura.Value = gvwFacturas.SelectedRow.Cells[0].Text;
            //txtCodTabla.Text = gvwFacturas.SelectedRow.Cells[2].Text;
            txtCodFactura.Text = gvwFacturas.SelectedRow.Cells[1].Text;
            var tempFactura = new FAC_FACTURA();
            tempFactura.NoFactura = Convert.ToInt32(gvwFacturas.SelectedRow.Cells[1].Text);
            tempFactura = objFact.ConsultaPorNumeroDeFactura(tempFactura);
            txtFechaFactura.Text = tempFactura.fechaCreacion.ToShortDateString();
            ddlMetal.SelectedValue = tempFactura.idCategoriaMetal.ToString();

            var tempCliente = new CLI_CLIENTES();
            tempCliente.NombreEncargado = gvwFacturas.SelectedRow.Cells[5].Text;
            tempCliente = objCli.ConsultarPorNombre(tempCliente).FirstOrDefault();
            ddlCliente.SelectedValue = tempCliente.idCliente.ToString();
            CargarTablaDetalleFacturas(Convert.ToInt32(hdfIdFactura.Value));
            cargarProductosPorIdCategoria(tempFactura.idCategoriaMetal);
            DynamicHyperLink1.Visible = false;
            DynamicHyperLink1.NavigateUrl = "~/ExcelFacturas/000Machote.xls";
            if(tempFactura.estado == 0)
            {
                btnInsertarActualizar.Enabled = true;
            }else
            {
                btnInsertarActualizar.Enabled = false;
            }
        }
        protected void gvwDetalleFactura_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tempCategoria = new CAT_CATEGORIA();
            tempCategoria.Nombre = gvwDetalleFactura.SelectedRow.Cells[4].Text;
            tempCategoria = objCat.ConsultarPorNombre(tempCategoria).FirstOrDefault();

            var tempProducto = new PRO_PRODUCTO();
            tempProducto.NombreProducto = gvwDetalleFactura.SelectedRow.Cells[2].Text;
            tempProducto.CodigoNumerico = Convert.ToInt32(gvwDetalleFactura.SelectedRow.Cells[3].Text);
            tempProducto.IdCategoria = tempCategoria.idCategoria;
            tempProducto = objProd.ConsultarPorNombreCodigoCategoria(tempProducto).FirstOrDefault();
            
            hdfIdDetalleFactura.Value = gvwDetalleFactura.SelectedRow.Cells[0].Text;
            int noFactura = Convert.ToInt32(gvwDetalleFactura.SelectedRow.Cells[1].Text);
            int idProducto = tempProducto.IdProducto;
            int idCategoria = tempCategoria.idCategoria;
            int Cantidad = Convert.ToInt32(gvwDetalleFactura.SelectedRow.Cells[5].Text);
            
            ddlProducto.SelectedValue = tempProducto.IdProducto.ToString();

            var detFac = new DEF_DETALLE_FACTURA();
            detFac.idDetalleFactura = Convert.ToInt32(hdfIdDetalleFactura.Value);
            actualizarFacturaLuegoDeBorrado(noFactura, idProducto, Cantidad);
            actualizarCantidadProducto(idProducto, Cantidad, false);
            objDeF.Eliminar(detFac);

            CargarTablaDetalleFacturas(Convert.ToInt32(hdfIdFactura.Value));
            CargarTablaFacturas(txtCriterio.Text);
        }
        protected void btnInsertarActualizar_Click(object sender, EventArgs e)
        {
            var idFactura = 0;
            if (Convert.ToInt32(hdfIdFactura.Value) == -1)
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
            nuevoDetalle.CantidadDevuelta = 0;
            actualizarCantidadProducto(nuevoDetalle.idProducto, nuevoDetalle.CantidadProducto, true);
            objDeF.Insertar(nuevoDetalle);
            txtCantidad.Text = "";
            CargarTablaDetalleFacturas(idFactura);
            CargarTablaFacturas(txtCriterio.Text);
        }
        protected void gvwDetalleFactura_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvwDetalleFactura.PageIndex = e.NewPageIndex;
            CargarTablaDetalleFacturas(Convert.ToInt32(hdfIdFactura.Value));
        }
        protected void gvwFacturas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvwFacturas.PageIndex = e.NewPageIndex;
            CargarTablaFacturas(txtCriterio.Text);
        }
        protected void btnNuevaFactura_Click(object sender, EventArgs e)
        {
            txtCodFactura.Text = "";
            txtCantidad.Text = "";
            hdfIdDetalleFactura.Value = "-1";
            hdfIdFactura.Value = "-1";
            Session["Factura"] = "-1";
            btnInsertarActualizar.Enabled = true;

            cargarClientes();
            cargarMetales();
            cargarProductos();
            CargarTablaFacturas(txtCriterio.Text);
            CargarTablaDetalleFacturas(Convert.ToInt32(hdfIdFactura.Value));
            DynamicHyperLink1.Visible = false;
            DynamicHyperLink1.NavigateUrl = "~/ExcelFacturas/000Machote.xls";
        }

        protected void txtCriterio_TextChanged(object sender, EventArgs e)
        {
            CargarTablaFacturas(txtCriterio.Text);
        }

        protected void ddlMetal_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarCategorias(ddlMetal.SelectedValue);
        }

        protected void ddlCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarProductos();
        }
        #endregion
        #region Metodos Privados

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

        private void cargarCategorias(string metal)
        {
            switch (metal)
            {
                case "1":
                case "2":
                    ddlCategoria.Items.Clear();
                    ddlCategoria.Items.Add("-Seleccionar-");
                    ddlCategoria.Items.Add("Cadena");
                    ddlCategoria.Items.Add("Pulsera");
                    ddlCategoria.Items.Add("Dije");
                    ddlCategoria.Items.Add("Juego");
                    ddlCategoria.Items.Add("Arete");
                    ddlCategoria.Items.Add("Anillo");
                    break;
                case "3":
                    ddlCategoria.Items.Clear();
                    ddlCategoria.Items.Add("-Seleccionar-");
                    ddlCategoria.Items.Add("Omega");
                    ddlCategoria.Items.Add("Cadena");
                    ddlCategoria.Items.Add("Pulsera");
                    ddlCategoria.Items.Add("Arete");
                    ddlCategoria.Items.Add("Juego");
                    ddlCategoria.Items.Add("Aro");
                    ddlCategoria.Items.Add("Dije");
                    ddlCategoria.Items.Add("Anillo");
                    break;
                default:
                    ddlCategoria.Items.Clear();
                    ddlCategoria.Items.Add("-Seleccionar-");
                    ddlCategoria.Items.Add("Cadena");
                    ddlCategoria.Items.Add("Pulsera");
                    ddlCategoria.Items.Add("Dije");
                    ddlCategoria.Items.Add("Arete");
                    ddlCategoria.Items.Add("Juego");
                    ddlCategoria.Items.Add("Anillo");
                    break;
            }
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

        private void cargarMetales()
        {
            try
            {
                var dt = new DataTable();
                var rows = objCat.Consultar();

                ddlMetal.DataTextField = "Nombre";
                ddlMetal.DataValueField = "idCategoria";
                dt.Columns.Add("idCategoria", typeof(System.String));
                dt.Columns.Add("Nombre", typeof(System.String));

                foreach (CAT_CATEGORIA r in rows)
                {
                    DataRow fila = dt.NewRow();

                    fila["idCategoria"] = r.idCategoria;
                    fila["Nombre"] = r.Nombre;
                    dt.Rows.Add(fila);
                }
                ddlMetal.DataSource = dt;
                ddlMetal.DataBind();
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
                lblLoad.Text = "Cargando...";
                var dt = new DataTable();
                var rows = objProd.ConsultarPorCategoria(Convert.ToInt32(ddlMetal.SelectedValue));

                ddlProducto.Items.Clear();
                ddlProducto.DataTextField = "NombreProducto";
                ddlProducto.DataValueField = "IdProducto";
                dt.Columns.Add("IdProducto", typeof(System.String));
                dt.Columns.Add("NombreProducto", typeof(System.String));

                foreach (PRO_PRODUCTO r in rows)
                {
                    var tempCategoria = new CAT_CATEGORIA();
                    tempCategoria.idCategoria = r.IdCategoria;
                    tempCategoria = objCat.ConsultarPorId(tempCategoria).FirstOrDefault();

                    // filtrar por nombre seleccionado
                    if (ddlCategoria.SelectedItem.Text.Equals(r.NombreProducto, StringComparison.CurrentCultureIgnoreCase))
                    {
                        DataRow fila = dt.NewRow();

                        fila["IdProducto"] = r.IdProducto;
                        fila["NombreProducto"] = r.NombreProducto + " " + tempCategoria.Nombre + " " + r.CodigoNumerico;
                        dt.Rows.Add(fila);
                    }
                }
                ddlProducto.DataSource = dt;
                ddlProducto.DataBind();
                lblLoad.Text = "";
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
        }
        private void cargarProductosPorIdCategoria(int IdCategoria)
        {
            try
            {
                var dt = new DataTable();
                var rows = objProd.ConsultarPorCategoria(IdCategoria);

                ddlProducto.Items.Clear();
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
                //var rows = objDeF.Consultar();
                gvwDetalleFactura.DataSource = null;
                gvwDetalleFactura.DataBind();

                dt.Columns.Add("idDetalleFactura", typeof(System.String));
                dt.Columns.Add("idFactura", typeof(System.String));
                dt.Columns.Add("idProducto", typeof(System.String));
                dt.Columns.Add("CodProducto", typeof(System.String));
                dt.Columns.Add("idCategoria", typeof(System.String));
                dt.Columns.Add("CantidadProducto", typeof(System.String));

                if (idFactura != -1){
                    //var rows = objDeF.ConsultarPorIdFactura(idFactura, txtCriterio.Text);
                    var tempDetalleFactura = new DEF_DETALLE_FACTURA();
                    tempDetalleFactura.idFactura = idFactura;
                    var rows = objDeF.ConsultarDetalleFacturaPorIdFactura(tempDetalleFactura);

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
                var estadoFactura = new FAC_FACTURA();
                estadoFactura.idFactura = idFactura;
                estadoFactura = objFact.ConsultarPorId(estadoFactura).FirstOrDefault();
                if (estadoFactura.estado == Convert.ToInt32(EstadoFacturas.EnCreacion))
                {
                    this.gvwDetalleFactura.Columns[6].Visible = true;
                }
                else {
                    this.gvwDetalleFactura.Columns[6].Visible = false;
                }
                gvwDetalleFactura.DataSource = dt;
                gvwDetalleFactura.DataBind();
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
        }

        private void CargarTablaFacturas(string criterio)
        {
            try
            {
                var dt = new DataTable();
                var rows = objFact.ConsultarPorNoFactura(criterio);

                dt.Columns.Add("idFactura", typeof(System.String));
                dt.Columns.Add("NoFactura", typeof(System.String));
                //dt.Columns.Add("CodTabla", typeof(System.String));
                dt.Columns.Add("montoFactura", typeof(System.String));
                dt.Columns.Add("estado", typeof(System.String));
                dt.Columns.Add("totalPiezas", typeof(System.String));
                dt.Columns.Add("idCliente", typeof(System.String));
                dt.Columns.Add("idTipoMetal", typeof(System.String));
                dt.Columns.Add("nombreUsuario", typeof(System.String));
                //dt.Columns.Add("fechaCreacion", typeof(System.String));
                //dt.Columns.Add("fechaLiquidacion", typeof(System.String));

                foreach (FAC_FACTURA r in rows)
                {
                    var tempCliente = new CLI_CLIENTES();
                    var tempCategoria = new CAT_CATEGORIA();
                    tempCliente.idCliente = r.idCliente;
                    tempCliente = objCli.ConsultarPorId(tempCliente).FirstOrDefault();
                    tempCategoria.idCategoria = r.idCategoriaMetal;
                    tempCategoria = objCat.ConsultarPorIdCategoria(tempCategoria);

                    DataRow fila = dt.NewRow();

                    fila["idFactura"] = r.idFactura;
                    fila["NoFactura"] = r.NoFactura;
                    //fila["CodTabla"] = r.CodTabla;
                    fila["montoFactura"] = r.montoFactura;
                    fila["estado"] = EstadoFacturaEnLetras(r.estado);
                    fila["totalPiezas"] = r.totalPiezas;
                    fila["idCliente"] = tempCliente.NombreEncargado;
                    fila["idTipoMetal"] = tempCategoria.Nombre;
                    fila["nombreUsuario"] = obtenerNombreUsuarioPorId(r.idUsuario);
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

        private string obtenerNombreUsuarioPorId(int idUsuario)
        {
            var manager = new AspNetUsers();
            manager.Id = idUsuario;
            var usuario = objUsu.ConsultarPorId(manager).FirstOrDefault();
            return usuario.UserName.ToString();
        }

        private void actualizarFacturaYaInsertada(int idFactura, int cantidad, int idProducto)
        {
            var tempFactura = new FAC_FACTURA();
            tempFactura.idFactura = idFactura;
            tempFactura = objFact.ConsultarPorId(tempFactura).FirstOrDefault();
            var fechaCrea = CreacionDeFechaDesdeElTxtFecha();
            tempFactura.montoFactura = tempFactura.montoFactura + calcularMonto(idProducto, cantidad);
            tempFactura.saldo = tempFactura.montoFactura;
            tempFactura.fechaCreacion = fechaCrea;
            tempFactura.fechaLiquidacion = fechaCrea.AddDays(50);
            tempFactura.totalDevuelto = 0;
            tempFactura.totalPiezas = tempFactura.totalPiezas + cantidad;

            tempFactura = objFact.Actualizar(tempFactura);
        }

        private DateTime CreacionDeFechaDesdeElTxtFecha()
        {
            var ArregloFecha = txtFechaFactura.Text.Split('/');
            var dia = Convert.ToInt32(ArregloFecha[1]);
            var mes = Convert.ToInt32(ArregloFecha[0]);
            var año = Convert.ToInt32(ArregloFecha[2]);

            DateTime fechaCrea = new DateTime(año, mes, dia);
            return fechaCrea;
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
        #endregion
        #region Metodos Publicos
        public int guardarFactura()
        {
            var nuevaFactura = new FAC_FACTURA();
            var fechaCreacion = CreacionDeFechaDesdeElTxtFecha();
            nuevaFactura.CodTabla = txtCodFactura.Text;
            nuevaFactura.estado = Convert.ToInt32(EstadoFacturas.EnCreacion);
            nuevaFactura.idCliente = Convert.ToInt32(ddlCliente.SelectedValue);
            nuevaFactura.montoFactura = calcularMonto(Convert.ToInt32(ddlProducto.SelectedValue), Convert.ToInt32(txtCantidad.Text));
            nuevaFactura.NoFactura = Convert.ToInt32(txtCodFactura.Text);
            nuevaFactura.fechaCreacion = fechaCreacion;
            nuevaFactura.fechaLiquidacion = fechaCreacion.AddDays(50);
            nuevaFactura.saldo = nuevaFactura.montoFactura;
            nuevaFactura.totalDevuelto = 0;
            nuevaFactura.totalPiezas = Convert.ToInt32(txtCantidad.Text);
            nuevaFactura.idCategoriaMetal = Convert.ToInt32(ddlMetal.SelectedValue);
            nuevaFactura.idUsuario = Convert.ToInt32(Session["userId"]);

            nuevaFactura = objFact.Insertar(nuevaFactura);

            return nuevaFactura.idFactura;
        }

        private string EstadoFacturaEnLetras(int num)
        {
            if(num == Convert.ToInt32(EstadoFacturas.EnCreacion))
            {
                return "En creación";
            }else if (num == Convert.ToInt32(EstadoFacturas.Finalizada))
            {
                return "Finalizada";
            }
            else{
                return "Cancelada";
            }
        }

        #endregion
        #region Imprimir
        protected void btnFinalizarLaFactura_Click(object sender, EventArgs e)
        {
            //var rows = objDeF.Consultar();
            if (hdfIdFactura.Value == "-1")
            {
                //No imprime nada
                DynamicHyperLink1.Visible = false;
                btnInsertarActualizar.Enabled = true;
            }
            else
            {
                btnInsertarActualizar.Enabled = false;
                var tempIdFacturaDeDetalleFactura = new DEF_DETALLE_FACTURA();
                tempIdFacturaDeDetalleFactura.idFactura = Convert.ToInt32(hdfIdFactura.Value);

                var metal = "";
                var contadorDeFilas = 0;
                var rows = objDeF.ConsultarDetalleFacturaPorIdFactura(tempIdFacturaDeDetalleFactura);
                var datosDeLaFactura = new FAC_FACTURA();
                var tempCategoria = new CAT_CATEGORIA();
                var fechaCreacion = CreacionDeFechaDesdeElTxtFecha();

                datosDeLaFactura.idFactura = Convert.ToInt32(hdfIdFactura.Value);
                datosDeLaFactura = objFact.ConsultarPorId(datosDeLaFactura).FirstOrDefault();
                
                // actulizar el estado de la factura
                if(datosDeLaFactura.estado == Convert.ToInt32(EstadoFacturas.EnCreacion))
                {
                    datosDeLaFactura = actualizarDatosFactura(datosDeLaFactura);
                }

                tempCategoria.idCategoria = datosDeLaFactura.idCategoriaMetal;
                tempCategoria = objCat.ConsultarPorId(tempCategoria).FirstOrDefault();
                metal = tempCategoria.Nombre;
                llenaArregloConCeros();
                // Recorrer las filas.
                foreach (DEF_DETALLE_FACTURA r in rows)
                {
                    //// Crear una fila por cada unidad del producto.
                    var tempProducto = new PRO_PRODUCTO();

                    tempProducto.IdProducto = r.idProducto;
                    tempProducto = objProd.ConsultarPorId(tempProducto).FirstOrDefault();

                    arregloTemporal[contadorDeFilas, 0] = tempProducto.NombreProducto;
                    arregloTemporal[contadorDeFilas, 1] = tempProducto.CodigoNumerico.ToString();
                    arregloTemporal[contadorDeFilas, 2] = r.CantidadProducto.ToString();

                    contadorDeFilas++;

                }
                contadorDeFilas = 0;
                //string sFile = "C:\\joyeriaSYS\\joyeriaSYS\\ExcelFacturas\\000Machote.xls";
                string sFile = "C:\\inetpub\\wwwroot\\joyeriasys\\ExcelFacturas\\000Machote.xls";
                //string sFile = "C:\\inetpub\\wwwroot\\JoyeriaPRUEBAS\\ExcelFacturas\\000Machote.xls";
                //string sTemplate = "C:\\Template.xls";
                object opc = Type.Missing;

                var excelApp = new Excel.Application();
                excelApp.DisplayAlerts = false;
                // Make the object visible.
                //excelApp.Visible = true;

                //var excelBook = new Excel.Workbook();
                //var excelSheet = new Excel.Worksheet();
                var excelBook = excelApp.Workbooks.Open(sFile, opc, opc, opc, opc, opc, opc, opc, opc, opc, opc, opc, opc, opc, opc);
                var excelSheet = (Excel.Worksheet)excelBook.Sheets.get_Item(1);
                try
                {
                    var tempCliente = new CLI_CLIENTES();
                    tempCliente.idCliente = Convert.ToInt32(ddlCliente.SelectedValue);
                    tempCliente = objCli.ConsultarPorIdCliente(tempCliente);
                    //Ponemos la fecha actual, el vendedor y el metal respectivamente.
                    excelSheet.Cells[3, 5] = fechaCreacion;
                    excelSheet.Cells[5, 3] = tempCliente.NombreEncargado;
                    excelSheet.Cells[6, 3] = metal;
                    //Ponemos la descripci+on del producto.
                    for (int i = 8; i < 43; i++)
                    {
                        excelSheet.Cells[i, 2] = arregloTemporal[contadorDeFilas, 0].ToString();
                        excelSheet.Cells[i, 3] = arregloTemporal[contadorDeFilas, 1].ToString();
                        excelSheet.Cells[i, 4] = arregloTemporal[contadorDeFilas, 2].ToString();
                        contadorDeFilas++;
                    }

                    excelSheet.Cells[46, 2] = datosDeLaFactura.montoFactura;
                    excelSheet.Cells[47, 3] = datosDeLaFactura.totalPiezas;
                    excelSheet.Cells[48, 3] = fechaCreacion.AddDays(50);

                    excelSheet.SaveAs("C:\\inetpub\\wwwroot\\joyeriasys\\ExcelFacturas\\" + datosDeLaFactura.NoFactura + ".xls", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, Excel.XlSaveAsAccessMode.xlNoChange, Excel.XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                    //excelSheet.SaveAs("C:\\inetpub\\wwwroot\\JoyeriaPRUEBAS\\ExcelFacturas\\" + datosDeLaFactura.NoFactura + ".xls", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, Excel.XlSaveAsAccessMode.xlNoChange, Excel.XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                    //excelSheet.SaveAs("C:\\joyeriaSYS\\joyeriaSYS\\ExcelFacturas\\" + datosDeLaFactura.NoFactura + ".xls", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, opc, opc, true, false, Excel.XlSaveAsAccessMode.xlNoChange, Excel.XlSaveConflictResolution.xlLocalSessionChanges, opc, opc);
                    //excelApp.Visible = true;

                    //excelSheet.PrintOut();

                    //Marshal.FinalReleaseComObject(excelSheet);
                    excelBook.Close();
                    excelApp.Quit();
                    //System.Runtime.InteropServices.Marshal.ReleaseComObject(excelBook);
                    //System.Runtime.InteropServices.Marshal.ReleaseComObject(excelSheet);
                    //System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

                    DynamicHyperLink1.NavigateUrl = "~/ExcelFacturas/" + datosDeLaFactura.NoFactura + ".xls";
                    DynamicHyperLink1.Visible = true;
                    //string _open = "window.open('/ExcelFacturas/" + datosDeLaFactura.NoFactura + ".xls', '_newtab');";
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), _open, true);

                    //btnImprimir.PostBackUrl = "198.38.93.222/ExcelFacturas/"+ datosDeLaFactura.NoFactura +".xls";
                    //MostrarMensaje("Excel creado");
                    excelBook = null;
                    excelSheet = null;
                    excelApp = null;
                    //System.GC.Collect();
                }
                catch (Exception ex)
                {
                    Console.Error.Write(ex.Message);
                    excelBook.Close();
                    excelApp.Quit();
                }
            }//Fin else
        }

        private FAC_FACTURA actualizarDatosFactura(FAC_FACTURA factura)
        {
            DateTime fechaActual = DateTime.Today;
            var dia = Convert.ToInt32(fechaActual.Day.ToString());
            var mes = Convert.ToInt32(fechaActual.Month.ToString());
            var año = Convert.ToInt32(fechaActual.Year.ToString());
            DateTime fechaCreacion = new DateTime(año, mes, dia);
            var cantidadPiezasFinal = objProcedimientos.SP_CantidadProductosEnLaFactura(factura.NoFactura);
            var saldoFinal = objProcedimientos.SP_SaldoDeFacturaNOCancelada(factura.NoFactura);

            factura.estado = Convert.ToInt32(EstadoFacturas.Finalizada);
            factura.fechaCreacion = fechaCreacion;
            factura.fechaLiquidacion = fechaCreacion.AddDays(50);
            factura.idUsuario = Convert.ToInt32(Session["userId"]);
            factura.totalPiezas = cantidadPiezasFinal;
            factura.saldo = saldoFinal;
            factura.montoFactura = saldoFinal;
            var actualizado = objFact.Actualizar(factura);
            return actualizado;
        }

        private void llenaArregloConCeros()
        {
            for (int i = 0; i < arregloTemporal.GetLength(0); ++i)
            {
                for (int j = 0; j < arregloTemporal.GetLength(1); ++j)
                {
                    arregloTemporal[i, j] = "0";
                }
            }
        }
        #endregion
        
    }//Fin de la clase
}