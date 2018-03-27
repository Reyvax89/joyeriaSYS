using joyeriaSYS.Controles.clases;
using joyeriaSYS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
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
        private string[,] arregloTemporal = new string[35,3];
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
                    ddlFacturas.SelectedValue = tempFactura.idFactura.ToString();
                    CargarTablaDetalleFacturas(tempFactura.idFactura);
                }
                else
                {
                    CargarTablaDetalleFacturas(Convert.ToInt32(ddlFacturas.SelectedValue));
                }
            }

        }

        private void CargarTablaDetalleFacturas(int idFactura)
        {
            try
            {
                var dt = new System.Data.DataTable();
                var rows = objDeF.Consultar();
                //var contadorDeFilas = 0;
                gvwDetalleFactura.DataSource = null;
                gvwDetalleFactura.DataBind();
                if (idFactura != -1)
                {
                    rows = objDeF.ConsultarPorIdFactura(idFactura);
                }
                
                dt.Columns.Add("categoria", typeof(System.String));
                dt.Columns.Add("idProducto", typeof(System.String));
                dt.Columns.Add("CantidadProducto", typeof(System.String));

                // Recorrer las filas.
                foreach (DEF_DETALLE_FACTURA r in rows)
                {
                    //// Crear una fila por cada unidad del producto.
                        var tempProducto = new PRO_PRODUCTO();
                        var tempCategoria = new CAT_CATEGORIA();

                        tempProducto.IdProducto = r.idProducto;
                        tempProducto = objProd.ConsultarPorId(tempProducto).FirstOrDefault();

                        tempCategoria.idCategoria = tempProducto.IdCategoria;
                        tempCategoria = objCateg.ConsultarPorId(tempCategoria).FirstOrDefault();
                        // Crear la fila, asignar valores y agregarla.
                        DataRow fila = dt.NewRow();
                        fila["categoria"] = tempProducto.NombreProducto + " " + tempCategoria.Nombre;
                        fila["idProducto"] = tempProducto.CodigoNumerico;
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
            //var rows = objDeF.Consultar();
            var metal = "";
            var contadorDeFilas = 0;
            var rows = objDeF.ConsultarPorIdFactura(Convert.ToInt32(ddlFacturas.SelectedValue));
            var datosDeLaFactura = new FAC_FACTURA();
            datosDeLaFactura.idFactura = Convert.ToInt32(ddlFacturas.SelectedValue);
            datosDeLaFactura = objFact.ConsultarPorId(datosDeLaFactura).FirstOrDefault();
            llenaArregloConCeros();
            // Recorrer las filas.
            foreach (DEF_DETALLE_FACTURA r in rows)
            {
                //// Crear una fila por cada unidad del producto.
                var tempProducto = new PRO_PRODUCTO();
                var tempCategoria = new CAT_CATEGORIA();

                tempProducto.IdProducto = r.idProducto;
                tempProducto = objProd.ConsultarPorId(tempProducto).FirstOrDefault();

                tempCategoria.idCategoria = tempProducto.IdCategoria;
                tempCategoria = objCateg.ConsultarPorId(tempCategoria).FirstOrDefault();
               
                arregloTemporal[contadorDeFilas, 0] = tempProducto.NombreProducto;
                arregloTemporal[contadorDeFilas, 1] = tempProducto.CodigoNumerico.ToString();
                arregloTemporal[contadorDeFilas, 2] = r.CantidadProducto.ToString();
                metal = tempCategoria.Nombre;
                contadorDeFilas++;

            }
            contadorDeFilas = 0;
            //string sFile = "C:\\Users\\cerva\\Desktop\\000Machote.xls";
            string sFile = "C:\\Excel facturas\\000Machote.xls";
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
                //Ponemos la fecha actual, el vendedor y el metal respectivamente.
                excelSheet.Cells[3, 5] = DateTime.Now.Date;
                excelSheet.Cells[5, 3] = "Bryan";
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
                excelSheet.Cells[48, 3] = DateTime.Now.Date.AddDays(50);

                //excelSheet.SaveAs("C:\\Users\\cerva\\Desktop\\BRYANExcel.xls", opc, opc, opc, opc, opc, opc, opc, opc, opc);
                //excelSheet.SaveAs("C:\\Excel facturas\\Bryan.xls", opc, opc, opc, opc, opc, opc, opc, opc, opc);
                excelSheet.SaveAs("C:\\Excel facturas\\Bryan.xls", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, Excel.XlSaveAsAccessMode.xlNoChange, Excel.XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                //excelSheet.SaveAs("C:\\Users\\cerva\\Desktop\\BRYANExcel.xls", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, Excel.XlSaveAsAccessMode.xlNoChange, Excel.XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                //excelApp.Visible = true;
                excelSheet.PrintOut(
        Type.Missing, Type.Missing, Type.Missing, Type.Missing,
        Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                Marshal.FinalReleaseComObject(excelSheet);
                excelBook.Close();
                excelApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelBook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelSheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

                MostrarMensaje("Excel creado");
                excelBook = null;
                excelSheet = null;
                excelApp = null;
                System.GC.Collect();
            }catch (Exception ex)
            {
                Console.Error.Write(ex.Message);
                excelBook.Close();
                excelApp.Quit();
            }
            
        }

        public void MostrarMensaje(string Mensaje)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + Mensaje + "');", true);
        }

        protected void ddlFacturas_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarTablaDetalleFacturas(Convert.ToInt32(ddlFacturas.SelectedValue));
        }
    }
}