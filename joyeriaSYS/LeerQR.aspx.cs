using System;
using Gma.QrCodeNet.Encoding;
using System.Drawing;
using System.IO;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System.Drawing.Imaging;
using System.Data;
using joyeriaSYS.Models;
using joyeriaSYS.Controles.clases;

namespace joyeriaSYS
{
    public partial class LeerQR : System.Web.UI.Page
    {
        private Producto objProd = new Producto();
        private Clientes objCli = new Clientes();
        private Factura objFact = new Factura();
        private DetalleFactura objDeF = new DetalleFactura();
        protected void Page_Load(object sender, EventArgs e)
        {
            

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
            var codProducto = Convert.ToInt32(txtCodigo.Text.Split(';')[0]);
            var codFactura = 0;
            if(txtNumeroFactura.Text == "")
            {
                txtNumeroFactura.Text = "";
            }else
            {
                codFactura = Convert.ToInt32(txtNumeroFactura.Text);
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
            }
            //poner en detalle un campo para ir descontando los productos escaneados
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

                dt.Columns.Add("idDetalleFactura", typeof(System.String));
                dt.Columns.Add("idFactura", typeof(System.String));
                dt.Columns.Add("idProducto", typeof(System.String));
                dt.Columns.Add("CantidadProducto", typeof(System.String));
                //dt.Columns.Add("Precio", typeof(System.String));
                //dt.Columns.Add("Inventario", typeof(System.String));

                foreach (DEF_DETALLE_FACTURA r in rows)
                {
                    DataRow fila = dt.NewRow();

                    fila["idDetalleFactura"] = r.idDetalleFactura;
                    fila["idFactura"] = r.idFactura;
                    fila["idProducto"] = r.idProducto;
                    fila["CantidadProducto"] = r.CantidadProducto;
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
    }
}