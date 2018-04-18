using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using joyeriaSYS.Models;
using joyeriaSYS.Controles.clases;
using System.Collections.Generic;

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
            }
        }
        protected void CreateUser_Click(object sender, EventArgs e)
        {
            if (validarCampos())
            {
                var manager = new AspNetUsers();
                manager.PasswordHash = txtPassword.Text;
                manager.UserName = txtUserName.Text;
                manager.Nombre = txtName.Text;
                manager.Apellido1 = txtLastname.Text;
                manager.IdRol = obtenerRolSeleccionado();

                var user = objUsu.Insertar(manager);
                if (user != null)
                {
                    // Limpiar los campos.
                    ErrorMessage.Text = "";
                    txtUserName.Text = "";
                    txtName.Text = "";
                    txtLastname.Text = "";
                    // Show confirmation.
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('El usuario fué registrado exitosamente.');", true);
                }
                else
                {
                    // Error messaje.
                    ErrorMessage.Text = "Error. El usuario no se guardó, consulte con soporte de usuario.";
                }
            }
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
    }
}