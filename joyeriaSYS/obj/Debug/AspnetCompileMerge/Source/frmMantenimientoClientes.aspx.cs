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
    public partial class frmMantenimientoClientes : System.Web.UI.Page
    {

        private Clientes objCat = new Clientes();
        protected void Page_Load(object sender, EventArgs e)
        {
            CargarTablaCategoria();
        }

        public int guardarActualizar(int id)
        {
            CLI_CLIENTES temp = new CLI_CLIENTES();
            temp.idCliente = id;
            temp.NombreEncargado = txtNombreEncargado.Text;
            temp.NombreJoyeria = txtNombreJoyeria.Text;
            temp.Provincia = txtProvincia.Text;
            temp.Telefono = txtTelefono.Text;
            temp.Celular = txtCelular.Text;
            temp.Direccion = txtDirección.Text;
            temp.Canton = txtCanton.Text;

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

                dt.Columns.Add("idCliente", typeof(System.String));
                dt.Columns.Add("NombreEncargado", typeof(System.String));
                dt.Columns.Add("NombreJoyeria", typeof(System.String));
                dt.Columns.Add("Celular", typeof(System.String));
                dt.Columns.Add("Telefono", typeof(System.String));
                dt.Columns.Add("Direccion", typeof(System.String));
                dt.Columns.Add("Provincia", typeof(System.String));
                dt.Columns.Add("Canton", typeof(System.String));

                foreach (CLI_CLIENTES r in rows)
                {
                    DataRow fila = dt.NewRow();

                    fila["idCliente"] = r.idCliente;
                    fila["NombreEncargado"] = r.NombreEncargado;
                    fila["NombreJoyeria"] = r.NombreJoyeria;
                    fila["Celular"] = r.Celular;
                    fila["Telefono"] = r.Telefono;
                    fila["Direccion"] = r.Direccion;
                    fila["Provincia"] = r.Provincia;
                    fila["Canton"] = r.Canton;
                    dt.Rows.Add(fila);
                }
                gvwClientes.DataSource = dt;
                gvwClientes.DataBind();
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
        }
        protected void btnInsertarActualizar_Click(object sender, EventArgs e)
        {
            guardarActualizar(Convert.ToInt32(hdfId.Value));
            btnInsertarActualizar.Text = "Guardar";
            txtCanton.Text = "";
            txtCelular.Text = "";
            txtDirección.Text = "";
            txtNombreEncargado.Text = "";
            txtNombreJoyeria.Text = "";
            txtProvincia.Text = "";
            txtTelefono.Text = "";
            hdfId.Value = "-1";
            CargarTablaCategoria();
        }

        protected void gvwCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            hdfId.Value = gvwClientes.SelectedRow.Cells[0].Text;
            txtNombreEncargado.Text = gvwClientes.SelectedRow.Cells[1].Text;
            txtNombreJoyeria.Text = gvwClientes.SelectedRow.Cells[2].Text;
            txtCelular.Text = gvwClientes.SelectedRow.Cells[3].Text;
            txtTelefono.Text = gvwClientes.SelectedRow.Cells[4].Text;
            txtDirección.Text = gvwClientes.SelectedRow.Cells[5].Text;
            txtProvincia.Text = gvwClientes.SelectedRow.Cells[6].Text;
            txtCanton.Text = gvwClientes.SelectedRow.Cells[7].Text;
            btnInsertarActualizar.Text = "Actualizar";
        }
    }
}