using System;
using Gma.QrCodeNet.Encoding;
using System.Drawing;
using System.IO;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System.Drawing.Imaging;
using System.Data;
using joyeriaSYS.Models;
using joyeriaSYS.Controles.clases;
using System.Web.UI;
using System.Collections.Generic;

namespace joyeriaSYS
{
    public partial class LeerQR : Page
    {
        private Producto objProd = new Producto();
        private Clientes objCli = new Clientes();
        private Factura objFact = new Factura();
        private DetalleFactura objDeF = new DetalleFactura();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarFacturas();
            }
        }

        protected void gvwDetalleFactura_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvwFacturas_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnCalcular_Click(object sender, EventArgs e)
        {

        }

        protected void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            var codProducto = (txtCodigo.Text != "") ? Convert.ToInt32(txtCodigo.Text) : 0;
            var codFactura = (ddlFactura.Text != "") ? Convert.ToInt32(ddlFactura.Text) : 0;
            if (codFactura != 0 && codFactura != 0)
            {
                gestionarCargaDatos(codProducto, codFactura);
            }
        }

        protected void ddlFactura_DropDownChanged(object sender, EventArgs e)
        {
            var codProducto = (txtCodigo.Text != "") ? Convert.ToInt32(txtCodigo.Text) : 0;
            var codFactura = (ddlFactura.Text != "") ? Convert.ToInt32(ddlFactura.Text) : 0;
            if (codFactura != 0 && codFactura != 0)
            {
                gestionarCargaDatos(codProducto, codFactura);
            }
        }

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
                cargarClientes(facturaActual.idCliente);
                txtCodTabla.Text = facturaActual.CodTabla;

                cargarProductos(productoActual.IdProducto);
                CargarTablaDetalleFacturas(facturaActual.idFactura);
                //poner en detalle un campo para ir descontando los productos escaneados

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

                fila["idFactura"] = row.idFactura;
                fila["NoFactura"] = row.NoFactura;
                fila["montoFactura"] = row.montoFactura;
                fila["estado"] = row.estado;
                fila["totalPiezas"] = row.totalPiezas;
                fila["idCliente"] = row.idCliente;
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
                var rows = objFact.Consultar();

                ddlFactura.DataTextField = "NoFactura";
                ddlFactura.DataValueField = "NoFacturaValue";
                dt.Columns.Add("NoFactura", typeof(System.String));
                dt.Columns.Add("NoFacturaValue", typeof(System.String));

                foreach (FAC_FACTURA r in rows)
                {
                    DataRow fila = dt.NewRow();

                    fila["NoFactura"] = r.NoFactura;
                    fila["NoFacturaValue"] = r.NoFactura;
                    dt.Rows.Add(fila);
                }
                ddlFactura.DataSource = dt;
                ddlFactura.DataBind();
                ddlFactura.SelectedValue = ddlFactura.ToString();
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
        }

        private void cargarClientes(int idCliente)
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
                ddlCliente.SelectedValue = idCliente.ToString();
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
        }

        private void cargarProductos(int idProducto)
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
                    DataRow fila = dt.NewRow();

                    fila["IdProducto"] = r.IdProducto;
                    fila["NombreProducto"] = r.NombreProducto;
                    dt.Rows.Add(fila);
                }
                ddlProducto.DataSource = dt;
                ddlProducto.DataBind();
                ddlProducto.SelectedValue = idProducto.ToString();
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
                if (idFactura != -1)
                {
                    rows = objDeF.ConsultarPorIdFactura(idFactura);
                }

                dt.Columns.Add("idFactura", typeof(System.String));
                dt.Columns.Add("idProducto", typeof(System.String));
                dt.Columns.Add("codProducto", typeof(System.String));
                dt.Columns.Add("CantidadProducto", typeof(System.String));
                //dt.Columns.Add("Precio", typeof(System.String));
                //dt.Columns.Add("Inventario", typeof(System.String));

                // Recorrer las filas.
                int posicionConsidencia = -1;
                int contadorGeneral = 0;
                foreach (DEF_DETALLE_FACTURA r in rows)
                {
                    // Crear una fila por cada unidad del producto.
                    int cantidad = Convert.ToInt32(r.CantidadProducto);
                    for (int i = 0; i < cantidad; i++)
                    {
                        // Crear la fila, asignar valores y agregarla.
                        DataRow fila = dt.NewRow();
                        fila["idFactura"] = r.idFactura;
                        fila["idProducto"] = r.idProducto;

                        // Revisar si hay considencia con el codigo que se busca.
                        var productoConsulta = new PRO_PRODUCTO();
                        productoConsulta.IdProducto = r.idProducto;
                        var arrayProducto = objProd.ConsultarPorId(productoConsulta);
                        foreach (PRO_PRODUCTO producto in arrayProducto)
                        {
                            // Cargar el codigo del producto en la fila.
                            fila["codProducto"] = producto.CodigoNumerico;
                            // Ver si hay considencias con el codigo que se busca.
                            var codProducto = (txtCodigo.Text != "") ? Convert.ToInt32(txtCodigo.Text) : 0;
                            if (codProducto == producto.CodigoNumerico)
                            {
                                posicionConsidencia = contadorGeneral;
                            }
                        }
                        // La catidad siempre va ser 1.
                        fila["CantidadProducto"] = "1";
                        dt.Rows.Add(fila);
                        contadorGeneral++;
                    }
                }
                gvwDetalleFactura.DataSource = dt;
                gvwDetalleFactura.DataBind();
                if (posicionConsidencia != -1)
                {
                    gvwDetalleFactura.SelectRow(posicionConsidencia);
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
        }
    }
}