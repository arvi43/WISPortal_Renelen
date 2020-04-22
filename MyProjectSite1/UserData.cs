///*Author : Renelen Verceles
// * Date : 21-04-2020
// * Purpose : The business layer class to handle user data routines, this consumes the DataAccess Layer and is being used by the client
// * Contributors:

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyProjectSite1
{
    public class UserData
    {
       
        string errMessage = "";
        public string ErrorMessage {
            get {
                return errMessage;
            }
            set {
                errMessage = value;
            }
        }

        public UserInfo GetUserInfo(string emailID="", string password="") {
            LoginData loginData = new LoginData();
            UserInfo userInfo = loginData.GetUserInfo(emailID.Trim(), password.Trim());
            errMessage = loginData.ErrorMessage;
            return userInfo;
        }

        public int AddUser(string emailID, string password,  string userName, string contactNo, string gender) {
            DataAccessLayer dataLayer = new DataAccessLayer();
            int iUserID = dataLayer.AddUser(emailID.Trim(), password.Trim(), contactNo.Trim(), userName.Trim(),gender.Trim() );
            errMessage = dataLayer.ErrorMessage;
            return iUserID;
        }

    }
}