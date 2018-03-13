using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using joyeriaSYS.Models;
using joyeriaSYS.Controles.clases;

namespace joyeriaSYS.Account
{
    public partial class Register : Page
    {
        private Usuarios objUsu = new Usuarios();

        protected void CreateUser_Click(object sender, EventArgs e)
        {
            var manager = new AspNetUsers();
            manager.PasswordHash = txtPassword.Text;
            manager.UserName = txtUserName.Text;

            var user = objUsu.Insertar(manager);
            if (user != null)
            {
                Response.Redirect(Request.QueryString["ReturnUrl"]);
            }
            else 
            {
                ErrorMessage.Text = "El usuario no se guardó, consulte con soporte de usuario.";
            }
        }
    }
}