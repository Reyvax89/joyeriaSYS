using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace joyeriaSYS
{
    public partial class Contact : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["username"] == "" || Session["username"] == null)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
        }
    }
}