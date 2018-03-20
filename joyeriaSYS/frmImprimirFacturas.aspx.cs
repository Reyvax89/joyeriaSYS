using joyeriaSYS.Controles.clases;
using joyeriaSYS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace joyeriaSYS
{
    public partial class frmImprimirFacturas : System.Web.UI.Page
    {
        private Producto objProd = new Producto();
        private Clientes objCli = new Clientes();
        private Factura objFact = new Factura();
        private Categoria objCateg = new Categoria();
        private DetalleFactura objDeF = new DetalleFactura();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarFacturas();
                if (Session["Factura"].ToString() != "-1")
                {
                    var tempFactura = new FAC_FACTURA();
                    tempFactura.NoFactura = Convert.ToInt32(Session["Factura"].ToString());
                    tempFactura = objFact.ConsultaPorNumeroDeFactura(tempFactura);
                    ddlFacturas.SelectedIndex = tempFactura.idFactura;
                    CargarTablaDetalleFacturas(tempFactura.idFactura);
                }
                else
                {
                    //Poner alert "Seleccione una factura a imprimir"
                }
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

                dt.Columns.Add("categoria", typeof(System.String));
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
                        var tempProducto = new PRO_PRODUCTO();
                        var tempCategoria = new CAT_CATEGORIA();

                        tempProducto.IdProducto = r.idProducto;
                        tempProducto = objProd.ConsultarPorId(tempProducto).FirstOrDefault();

                        tempCategoria.idCategoria = tempProducto.IdCategoria;
                        tempCategoria = objCateg.ConsultarPorId(tempCategoria).FirstOrDefault();
                        // Crear la fila, asignar valores y agregarla.
                        DataRow fila = dt.NewRow();
                        fila["categoria"] = tempCategoria.Nombre;
                        fila["idProducto"] = tempProducto.CodigoNumerico;
                        // La catidad siempre va ser 1.
                        fila["CantidadProducto"] = r.CantidadProducto;
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
        private void cargarFacturas()
        {
            try
            {
                var dt = new DataTable();
                var rows = objFact.Consultar();

                ddlFacturas.DataTextField = "NoFactura";
                ddlFacturas.DataValueField = "idFactura";
                dt.Columns.Add("idFactura", typeof(System.String));
                dt.Columns.Add("NoFactura", typeof(System.String));

                foreach (FAC_FACTURA r in rows)
                {
                    DataRow fila = dt.NewRow();

                    fila["idFactura"] = r.idFactura;
                    fila["NoFactura"] = r.NoFactura;
                    dt.Rows.Add(fila);
                }
                ddlFacturas.DataSource = dt;
                ddlFacturas.DataBind();
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {

        }
    }
}