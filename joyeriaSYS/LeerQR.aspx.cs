﻿using System;
using Gma.QrCodeNet.Encoding;
using System.Drawing;
using System.IO;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System.Drawing.Imaging;
using System.Data;
using joyeriaSYS.Models;
using joyeriaSYS.Controles.clases;
using System.Web.UI;

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
            var codFactura = (txtNumeroFactura.Text != "") ? Convert.ToInt32(txtNumeroFactura.Text) : 0;
            if (codFactura != 0 && codFactura != 0)
            {
                gestionarCargaDatos(codProducto, codFactura);
            }
        }

        protected void txtNumeroFactura_TextChanged(object sender, EventArgs e)
        {
            var codProducto = (txtCodigo.Text != "") ? Convert.ToInt32(txtCodigo.Text) : 0;
            var codFactura = (txtNumeroFactura.Text != "") ? Convert.ToInt32(txtNumeroFactura.Text) : 0;
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

                dt.Columns.Add("idDetalleFactura", typeof(System.String));
                dt.Columns.Add("idFactura", typeof(System.String));
                dt.Columns.Add("idProducto", typeof(System.String));
                dt.Columns.Add("CantidadProducto", typeof(System.String));
                //dt.Columns.Add("Precio", typeof(System.String));
                //dt.Columns.Add("Inventario", typeof(System.String));

                // Recorrer las filas.
                foreach (DEF_DETALLE_FACTURA r in rows)
                {
                    // Crear una fila por cada unidad del producto.
                    int cantidad = Convert.ToInt32(r.CantidadProducto);
                    for (int i = 0; i < cantidad; i++)
                    {
                        // Crear la fila, asignar valores y agregarla.
                        DataRow fila = dt.NewRow();
                        fila["idDetalleFactura"] = r.idDetalleFactura;
                        fila["idFactura"] = r.idFactura;
                        fila["idProducto"] = r.idProducto;
                        // La catidad siempre va ser 1.
                        fila["CantidadProducto"] = "1";
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
    }
}