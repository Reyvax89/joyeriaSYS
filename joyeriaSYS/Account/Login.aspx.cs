using System;
using System.Web;
using System.Web.UI;
using joyeriaSYS.Models;
using joyeriaSYS.Controles.clases;
using System.Collections.Generic;

namespace joyeriaSYS.Account
{
    public partial class Login : Page
    {
        private Usuarios objUsu = new Usuarios();

        protected void Page_Load(object sender, EventArgs e)
        {
            var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            if ((string)Session["username"] != "" && Session["username"] != null)
            {
                Session["username"] = null;
            }
        }

        protected void LogIn(object sender, EventArgs e)
        {
            if (IsValid)
            {
                // Validate the user password
                var manager = new AspNetUsers();
                manager.PasswordHash = txtPassword.Text;
                manager.UserName = txtUserName.Text;

                // This doen't count login failures towards account lockout
                // To enable password failures to trigger lockout, change to shouldLockout: true
                var result = objUsu.ConsultarPorNombrePassword(manager);
                if (result != null)
                {
                    // Set the session.
                    Session["username"] = result.UserName;
                    Session["userId"] = result.Id;
                    // Verificar el rol.
                    List<string> pages = new List<string>();
                    if (result.IdRol == 1)
                    {
                        // Paginas que puede ver un admin.
                        pages.Add("Default");
                        pages.Add("About");
                        pages.Add("Contact");
                        pages.Add("frmCrearFactura");
                        pages.Add("frmImprimirFacturas");
                        pages.Add("frmMantenimientoCategoria");
                        pages.Add("frmMantenimientoClientes");
                        pages.Add("LeerQR");
                        pages.Add("QR");
                        pages.Add("Register");
                        Session["paginas"] = pages;
                    }
                    if (result.IdRol == 2)
                    {
                        // Paginas que puede ver un facturador
                        pages.Add("Default");
                        pages.Add("frmCrearFactura");
                        pages.Add("LeerQR");
                        pages.Add("frmImprimirFacturas");
                        Session["paginas"] = pages;
                    }
                   
                    Response.Redirect("/");
                }
                else
                {
                    FailureText.Text = "Usuario o correo inválido.";
                    ErrorMessage.Visible = true;
                }
            }
        }
    }
}