///*Author : Renelen Verceles
// * Date : 21-04-2020
// * Purpose : To access SQL database, perform database routines regarding login to be used by client classes
// * Contributors: 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
    
namespace MyProjectSite1
{
    public class UserInfo
    {
        public int userID { get; set; }
        public string userName { get; set; }
        public string emailID { get; set; }
        public string contactNo { get; set; }

        public string password { get; set; }
        public string gender { get; set; }
    }
    public class LoginData
    {
        private string sErrorMsg = "";
        public LoginData() {
            sErrorMsg = "";
        }

        public string ErrorMessage {
            get {
                return sErrorMsg;
            }
        }


        private SqlConnection getConnection() {

            SqlConnection connection = null;
            try {
                string connectionString = ConfigurationManager.ConnectionStrings["MyProjectSite1.Properties.Settings.dbConnectionString"].ConnectionString;
                connection = new SqlConnection(connectionString);
                connection.Open();
            }
            catch (SqlException) {

            }
            return connection;
        }

        public UserInfo GetUserInfo(string emailID = "", string password = "") {
            sErrorMsg = "";
            SqlConnection connection = null;
            int iID = 0;
            UserInfo userInfo = null;
            try  {
                //open connection   
                connection = getConnection();
                
                //check if exists, if exists,output the user info. Else, output error message
                if (connection.State == ConnectionState.Open) {
                    string sQuery = "select userID,userEmailID,userName,contactNo,userPassword,gender from tbUsers where ";
                    SqlParameter[] paramColl;
                   
                    if (emailID.Trim() != "" && password.Trim() == "") {
                        sQuery += "userEmailID=@email";

                        paramColl = new SqlParameter[1];
                        paramColl[0] = new SqlParameter("@email", emailID);

                    }
                    else {
                        sQuery += "userEmailID=@email and userPassword=@pwd";

                        paramColl = new SqlParameter[2];
                        paramColl[0] = new SqlParameter("@email", emailID);
                        paramColl[1] = new SqlParameter("@pwd", password);
                    }
                    //execute database query
                    SqlCommand sqlCommand = new SqlCommand(sQuery, connection);
                    sqlCommand.Parameters.AddRange(paramColl);
                    SqlDataReader dataReader = sqlCommand.ExecuteReader();
                    sqlCommand.Dispose();

                    //check output result
                    if (dataReader.HasRows) {
                        dataReader.Read();
                        iID = int.Parse(dataReader["userID"].ToString().Trim());
                        userInfo = new UserInfo();
                        userInfo.userID = iID;
                        userInfo.emailID = dataReader["userEmailID"].ToString().Trim();
                        userInfo.userName = dataReader["userName"].ToString().Trim();
                        userInfo.contactNo = dataReader["contactNo"].ToString().Trim();
                        userInfo.password = dataReader["userPassword"].ToString().Trim();
                        userInfo.gender = dataReader["gender"].ToString().Trim();
                    }
                    else {
                        sErrorMsg = "Invalid email ID and/or password.";
                    }
                    sqlCommand.Dispose();
                }
            }
            catch (Exception ex) {
                Console.Write(ex.Message);

            }
            finally {
                //close connnection
                if (connection != null) {
                    if (connection.State != System.Data.ConnectionState.Closed) {
                        connection.Close();
                    }
                }
            }

            return userInfo;
        }
    }
}