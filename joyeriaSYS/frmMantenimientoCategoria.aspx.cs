
using joyeriaSYS.Controles.clases;
using joyeriaSYS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace joyeriaSYS
{
    public partial class frmMantenimientoCategoria : System.Web.UI.Page
    {
        private Categoria objCat = new Categoria();
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
                if (!pages.Exists(x => string.Equals(x, "frmMantenimientoCategoria", StringComparison.OrdinalIgnoreCase)))
                {
                    Response.Redirect("~/AccesoDenegado.aspx");
                }
            }
            CargarTablaCategoria();
            this.Title = "Administrar Metales";
        }

        protected void Reset_Click(object sender, EventArgs e)
        {
            txtNombre.Text = "";
        }

        public int guardarActualizar(int id)
        {
            CAT_CATEGORIA temp = new CAT_CATEGORIA();
            temp.idCategoria = id;
            temp.Nombre = txtNombre.Text;
            temp.Codigo = "---";

            if (objCat.ConsultarPorId(temp).Count() > 0)
            {
                objCat.Actualizar(temp);
            }
            else
            {
                objCat.Insertar(temp);
            }
            return 1;
        }

        public void CargarTablaCategoria()
        {
            try
            {
                var dt = new DataTable();
                var rows = objCat.Consultar();

                dt.Columns.Add("idCategoria", typeof(System.String));
                dt.Columns.Add("Nombre", typeof(System.String));
                //dt.Columns.Add("Codigo", typeof(System.String));

                foreach (CAT_CATEGORIA r in rows)
                {
                    DataRow fila = dt.NewRow();

                    fila["idCategoria"] = r.idCategoria;
                    fila["Nombre"] = r.Nombre;
                    //fila["Codigo"] = r.Codigo;
                    dt.Rows.Add(fila);
                }
                gvwCategoria.DataSource = dt;
                gvwCategoria.DataBind();
            }
            catch(Exception ex)
            {
                var err = ex.Message;
            }
        }
        

        protected void btnInsertarActualizar_Click(object sender, EventArgs e)
        {
            guardarActualizar(Convert.ToInt32(hdfId.Value));
            btnInsertarActualizar.Text = "Guardar";
            //txtCodigo.Text = "";
            txtNombre.Text = "";
            hdfId.Value = "-1";
            CargarTablaCategoria();
        }

        protected void gvwCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            hdfId.Value = gvwCategoria.SelectedRow.Cells[0].Text;
            txtNombre.Text = gvwCategoria.SelectedRow.Cells[1].Text;
            //txtCodigo.Text = gvwCategoria.SelectedRow.Cells[2].Text;
            btnInsertarActualizar.Text = "Actualizar";
        }
    }
}