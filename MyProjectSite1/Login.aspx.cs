///*Author : Renelen Verceles
// * Date : 21-04-2020
// * Purpose : Login page to authenticate users before using the application
// * Contributors: 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyProjectSite1
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e) {

        }

        protected void btnOK_Click(object sender, EventArgs e) {
            try {
                //check user info from database, then create persistent cookie and redirect to Default page
                LoginData dataLayer = new LoginData();
                UserInfo userInfo = dataLayer.GetUserInfo(txtEmailID.Text.Trim(), txtPassword.Text.Trim());
                if (userInfo != null) {
                    HttpCookie aCookie = new HttpCookie("aUserID");
                    aCookie.Values["aUserID"] = userInfo.userID.ToString().Trim();
                    aCookie.Values["aUserName"] = userInfo.userName.ToString().Trim(); 
                    aCookie.Expires.AddDays(3);
                    Response.Cookies.Add(aCookie);
                    Session["userID"] = userInfo.userID.ToString().Trim();
                    Session["userInfo"] = userInfo;

                    Response.Redirect("Default.aspx");
                }
                else {
                    throw new Exception("Invalid Email ID and Password combination.");
                }
            }
            catch (Exception error) {
                Response.Write("<script>alert('" + error.Message + "')</script>");
            }
        }
    }
}