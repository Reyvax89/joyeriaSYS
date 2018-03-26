using System;
using System.Web;
using System.Web.UI;
using joyeriaSYS.Models;
using joyeriaSYS.Controles.clases;

namespace joyeriaSYS.Account
{
    public partial class Login : Page
    {
        private Usuarios objUsu = new Usuarios();

        protected void Page_Load(object sender, EventArgs e)
        {
            var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
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

                if(result != null)
                {
                        Response.Redirect(Request.QueryString["ReturnUrl"]);
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