///*Author : Renelen Verceles
// * Date : 21-04-2020
// * Purpose : Register page to add new user credentials.  This user credentials will be used to authenticate users before using the application
// * Contributors: 

using MyProjectSite1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyProjectSite2
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e) {

        }

        protected void btnSave_Click(object sender, EventArgs e) {
            try {
                //add user to database
               UserData userData = new UserData();

                int iUserID = userData.AddUser(txtEmailID.Text.Trim(), txtPassword.Text.Trim(), txtContactNo.Text.Trim(), txtName.Text.Trim(),rdoGender.SelectedValue.ToString().Trim());
                //if the generated ID is returned, redirect to Login.  Else, throw exception
                if (iUserID > 0)
                {
                    Response.Redirect("Login.aspx");
                }
                else if (userData.ErrorMessage.Trim() != "")
                {

                    throw new Exception(userData.ErrorMessage.Trim());
                }
            }
            catch (CustomException.UserRegistrationFailedException error) {
                Response.Write("<script>alert('" + error.Message + "')</script>");
            }
            catch (System.Exception error2) {
                Response.Write("<script>alert('" + error2.Message + "')</script>");
            }
}

        protected void btnCancel_Click(object sender, EventArgs e) {
            Response.Redirect("Login.aspx");
        }
    }
}