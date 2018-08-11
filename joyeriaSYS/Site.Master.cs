using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;

namespace joyeriaSYS
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["username"] != "" && Session["username"] != null)
            {
                menuPrincipal.InnerHtml = "";
                lblUserName.Text = "Bienvenido " + (string)Session["username"];
                btnSession.Text = "Cerrar sesión";
                var userIDSession = Session["idRol"].ToString();
                if (Convert.ToInt32(userIDSession) == 1)
                {
                    menuPrincipal.InnerHtml = "<li class='<%:Page.Title=='Administrar Metales'?'active':''%>" +
                        "    <a runat = 'server' href='frmMantenimientoCategoria.aspx'>Adm.Metales</a> " +
                        "</li> " +
                        "<li class='<%:Page.Title=='Administrar clientes'?'active':''%>" +
                        "	<a runat = 'server' href='frmMantenimientoClientes.aspx'>Adm.clientes</a>" +
                        "</li>" +
                        "<li class='<%:Page.Title=='Administrar inventario'?'active':''%>" +
                        "	<a runat = 'server' href='QR.aspx'>Adm.inventario</a>" +
                        "</li>" +
                        "<li class='<%:Page.Title=='Crear factura'?'active':''%>" +
                        "	<a runat = 'server' href='frmCrearFactura.aspx'>Crear factura</a>" +
                        "</li>" +
                        "<li class='<%:Page.Title=='Liquidar factura'?'active':''%>" +
                        "	<a runat = 'server' href='LeerQR.aspx'>Liquidar factura</a>" +
                        "</li>" +
                        "<li class='<%:Page.Title=='Ver Facturas Liquidadas'?'active':''%>" +
                        "	<a runat = 'server' href='frmLeerFacturaCanceladas.aspx'>Facturas Liquidadas</a>" +
                        "</li>" +
                        "<li class='<%:Page.Title=='Register'?'active':''%>" +
                        "	<a runat = 'server' href='Account/Register.aspx'>Adm.usuarios</a>" +
                        "</li>";
                }
                else if (Convert.ToInt32(userIDSession) == 2)
                {
                    menuPrincipal.InnerHtml = "<li class='<%:Page.Title=='Crear factura'?'active':''%>" +
                            "	<a runat = 'server' href='frmCrearFactura.aspx'>Crear factura</a>" +
                            "</li>" +
                            "<li class='<%:Page.Title=='Liquidar factura'?'active':''%>" +
                            "	<a runat = 'server' href='LeerQR.aspx'>Liquidar factura</a>" +
                            "</li>" +
                            "<li class='<%:Page.Title=='Ver Facturas Liquidadas'?'active':''%>" +
                            "	<a runat = 'server' href='frmLeerFacturaCanceladas.aspx'>Facturas Liquidadas</a>" +
                            "</li>";
                }
                else if (Convert.ToInt32(userIDSession) == 3)
                {
                    menuPrincipal.InnerHtml = "<li class='<%:Page.Title=='Liquidar factura'?'active':''%>" +
                            "	<a runat = 'server' href='LeerQR.aspx'>Liquidar factura</a>" +
                            "</li>";
                }
            }
        }

        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {

        }
    }

}