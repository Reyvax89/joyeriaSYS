using joyeriaSYS.Controles.clases;
using joyeriaSYS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Excel = Microsoft.Office.Interop.Excel;

namespace joyeriaSYS
{
    public partial class frmImprimirFacturas : System.Web.UI.Page
    {
        private Producto objProd = new Producto();
        private Clientes objCli = new Clientes();
        private Factura objFact = new Factura();
        private Categoria objCateg = new Categoria();
        private DetalleFactura objDeF = new DetalleFactura();
        //private Excel objExcel = new Excel();

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
                var dt = new System.Data.DataTable();
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
                    //// Crear una fila por cada unidad del producto.
                    //int cantidad = Convert.ToInt32(r.CantidadProducto);
                    //for (int i = 0; i < cantidad; i++)
                    //{
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
                    //}
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
                var dt = new System.Data.DataTable();
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
            string sFile = "C:\\Users\\cerva\\Desktop\\Copia de facturero ACERO.xls";
            string sTemplate = "C:\\Template.xls";
            object opc = Type.Missing;

            var excelApp = new Excel.Application();
            // Make the object visible.
            //excelApp.Visible = true;

            var excelBook = excelApp.Workbooks.Open(sFile, opc, opc, opc, opc, opc, opc, opc, opc, opc, opc, opc, opc, opc, opc);
            var excelSheet = (Excel.Worksheet)excelBook.Sheets.get_Item(1);

            //Ponemos la fecha actual, el vendedor y el metal respectivamente.
            excelSheet.Cells[3, 5] = "03/03/1989";
            excelSheet.Cells[5, 3] = "Bryan";
            excelSheet.Cells[6, 3] = "Platino";
            //Ponemos la descripci+on del producto.
            for(int i = 8; i< 42; i++)
            {
                excelSheet.Cells[i, 2] = "Arete";
            }
            //Ponemos el codigo del producto.
            for (int i = 8; i < 42; i++)
            {
                excelSheet.Cells[i, 3] = "44";
            }
            //Ponemos la cantidad de producto por factura.
            for (int i = 8; i < 42; i++)
            {
                excelSheet.Cells[i, 4] = "00";
            }

            excelSheet.SaveAs("C:\\Users\\cerva\\Desktop\\BRYANExcel.xls", opc, opc, opc, opc, opc, opc, opc, opc, opc);

            excelApp.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelBook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelSheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

            MostrarMensaje("Excel creado");
            excelBook = null;
            excelSheet = null;
            excelApp = null;
            System.GC.Collect();
        }

        public void MostrarMensaje(string Mensaje)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + Mensaje + "');", true);
        }
    }
}