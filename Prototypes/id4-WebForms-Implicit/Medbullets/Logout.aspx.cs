using System;
using System.Web;

namespace WebApp
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Request.GetOwinContext().Authentication.SignOut();
        }
    }
}