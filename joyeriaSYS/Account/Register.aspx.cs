using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using joyeriaSYS.Models;
using joyeriaSYS.Controles.clases;
using System.Collections.Generic;
using System.Data;

namespace joyeriaSYS.Account
{
    public partial class Register : Page
    {
        private Usuarios objUsu = new Usuarios();
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
                if (!pages.Exists(x => string.Equals(x, "Register", StringComparison.OrdinalIgnoreCase)))
                {
                    Response.Redirect("~/AccesoDenegado.aspx");
                }
            }
            if (!IsPostBack)
            {
                ddlRol.Items.Add("-Seleccionar-");
                ddlRol.Items.Add("Administrador");
                ddlRol.Items.Add("Facturador");
                CargarTablaUsuarios();
            }
        }
        protected void CreateUser_Click(object sender, EventArgs e)
        {
            if (validarCampos())
            {
                var manager = new AspNetUsers();
                manager.Id = Convert.ToInt32(hdfId.Value);
                manager.PasswordHash = txtPassword.Text;
                manager.UserName = txtUserName.Text;
                manager.Nombre = txtName.Text;
                manager.Apellido1 = txtLastname.Text;
                manager.IdRol = obtenerRolSeleccionado();
      
                if (objUsu.ConsultarPorId(manager).Count() > 0)
                {
                    var user = objUsu.Actualizar(manager);
                    validarResultado(user);
                }
                else
                {
                    if (objUsu.ConsultarPorUserName(manager).Count() > 0)
                    {
                        ErrorMessage.Text = "Error. El nombre de usuario ya esta en uso.";
                    }
                    else
                    {
                        var user = objUsu.Insertar(manager);
                        validarResultado(user);
                    }
                }

            }
        }

        private void validarResultado(AspNetUsers user)
        {
            if (user != null)
            {
                // Limpiar los campos.
                ErrorMessage.Text = "";
                txtUserName.Text = "";
                txtName.Text = "";
                txtLastname.Text = "";
                hdfId.Value = "";
                ddlRol.ClearSelection();
                ddlRol.Items.FindByValue("-Seleccionar-").Selected = true;
                CargarTablaUsuarios();
                btnInsertarActualizar.Text = "Guardar";
                // Show confirmation.
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('El usuario fué registrado exitosamente.');", true);
            }
            else
            {
                // Error messaje.
                ErrorMessage.Text = "Error. El usuario no se guardó, consulte con soporte de usuario.";
            }
        }

        protected void gvwUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            hdfId.Value = gvwUsuario.SelectedRow.Cells[0].Text;
            txtName.Text = gvwUsuario.SelectedRow.Cells[1].Text;
            txtUserName.Text = gvwUsuario.SelectedRow.Cells[2].Text;
            txtLastname.Text = gvwUsuario.SelectedRow.Cells[3].Text;
            ddlRol.SelectedItem.Text = gvwUsuario.SelectedRow.Cells[4].Text;

            btnInsertarActualizar.Text = "Actualizar";
        }

        private int obtenerRolSeleccionado()
        {
            if (ddlRol.SelectedItem.Text == "Administrador")
            {
                return 1;
            }
            if (ddlRol.SelectedItem.Text == "Facturador")
            {
                return 2;
            }
            return 2;
        }

        private string obtenerNombreRol(int rol)
        {
            if (rol == 1)
            {
                return "Administrador";
            }
            if (rol == 2)
            {
                return "Facturador";
            }
            return "Nulo";
        }

        private bool validarCampos()
        {
            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                ErrorMessage.Text = "Error. Las contraseñas no coinciden.";
                return false;
            }
            if (txtName.Text == "")
            {
                ErrorMessage.Text = "Error. Debe ingresar un nombre.";
                return false;
            }
            if (txtPassword.Text == "" || txtConfirmPassword.Text == "")
            {
                ErrorMessage.Text = "Error. Los campos de contraseña no pueden estar vacios.";
                return false;
            }
            if (ddlRol.SelectedItem.Text == "-Seleccionar-")
            {
                ErrorMessage.Text = "Error. Debe seleccionar un rol.";
                return false;
            }
            return true;
        }

        public void CargarTablaUsuarios()
        {
            try
            {
                var dt = new DataTable();
                var rows = objUsu.Consultar();

                dt.Columns.Add("idUsuario", typeof(System.String));
                dt.Columns.Add("mombre", typeof(System.String));
                dt.Columns.Add("mombreUsuario", typeof(System.String));
                dt.Columns.Add("apellido", typeof(System.String));
                dt.Columns.Add("rol", typeof(System.String));

                foreach (AspNetUsers r in rows)
                {
                    DataRow fila = dt.NewRow();

                    fila["idUsuario"] = r.Id;
                    fila["mombre"] = r.Nombre;
                    fila["mombreUsuario"] = r.UserName;
                    fila["apellido"] = r.Apellido1;
                    fila["rol"] = obtenerNombreRol(r.IdRol.GetValueOrDefault());
                    dt.Rows.Add(fila);
                }
                gvwUsuario.DataSource = dt;
                gvwUsuario.DataBind();
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
        }
    }
}