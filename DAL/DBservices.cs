﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using Server.Moodle;
using System.Globalization;

namespace Server.Moodle.DAL
{
    public class DBservices
    {

        public DBservices()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        //--------------------------------------------------------------------------------------------------
        // This method creates a connection to the database according to the connectionString name in the web.config 
        //--------------------------------------------------------------------------------------------------
        public SqlConnection connect(String conString)
        {

            // read the connection string from the configuration file
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json").Build();
            string cStr = configuration.GetConnectionString("myProjDB");
            SqlConnection con = new SqlConnection(cStr);
            con.Open();
            return con;
        }
        //--------------------------------------------------------------------------------------------------
        // This method get all the users 
        //--------------------------------------------------------------------------------------------------
        public List<UserMusic> GetAllUsers()
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateCommandWithStoredProcedure("Proj_SP_GetAllUsers", con, null);             // create the command
            var returnParameter = cmd.Parameters.Add("@returnValue", SqlDbType.Int);

            returnParameter.Direction = ParameterDirection.ReturnValue;


            List<UserMusic> UserList = new List<UserMusic>();

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    UserMusic u = new UserMusic(
                        Convert.ToString(dataReader["Id"]),
                        Convert.ToString(dataReader["First"]),
                        Convert.ToString(dataReader["Last"]),
                        Convert.ToString(dataReader["Email"]),
                        Convert.ToString(dataReader["Password"]),
                        Convert.ToString(dataReader["ImgUrl"]),
                        Convert.ToDateTime(dataReader["RegistrationDate"])
                    );
                    UserList.Add(u);
                }
                return UserList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
                // note that the return value appears only after closing the connection
                var result = returnParameter.Value;
            }

        }
        //--------------------------------------------------------------------------------------------------
        // This method get user by email        
        //--------------------------------------------------------------------------------------------------
        public UserMusic GetUserByEmail(string email)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }


            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@Email", email);


            cmd = CreateCommandWithStoredProcedure("Proj_SP_GetUserByEmail", con, paramDic);             // create the command
            var returnParameter = cmd.Parameters.Add("@returnValue", SqlDbType.Int);

            returnParameter.Direction = ParameterDirection.ReturnValue;



            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    UserMusic u = new UserMusic(
                        Convert.ToString(dataReader["Id"]),
                        Convert.ToString(dataReader["First"]),
                        Convert.ToString(dataReader["Last"]),
                        Convert.ToString(dataReader["Email"]),
                        Convert.ToString(dataReader["Password"]),
                        Convert.ToString(dataReader["ImgUrl"]),
                        Convert.ToDateTime(dataReader["RegistrationDate"])
                    );
                    return u;
                }
                throw new Exception("User doesnt exists");
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
                // note that the return value appears only after closing the connection
                var result = returnParameter.Value;
            }

        }

        //--------------------------------------------------------------------------------------------------
        // This method get check if user exists        
        //--------------------------------------------------------------------------------------------------
        public UserMusic CheckUserExists(string email, string password)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }


            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@Email", email);
            paramDic.Add("@Password", password);


            cmd = CreateCommandWithStoredProcedure("Proj_SP_CheckIfUserExists", con, paramDic);             // create the command
            var returnParameter = cmd.Parameters.Add("@returnValue", SqlDbType.Int);

            returnParameter.Direction = ParameterDirection.ReturnValue;



            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    UserMusic u = new UserMusic(
                        Convert.ToString(dataReader["Id"]),
                        Convert.ToString(dataReader["First"]),
                        Convert.ToString(dataReader["Last"]),
                        Convert.ToString(dataReader["Email"]),
                        Convert.ToString(dataReader["Password"]),
                        Convert.ToString(dataReader["ImgUrl"]),
                        Convert.ToDateTime(dataReader["RegistrationDate"])
                    );
                    return u;
                }
                throw new Exception("user doesnt exists");
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
                // note that the return value appears only after closing the connection
                var result = returnParameter.Value;
            }
        }
        //--------------------------------------------------------------------------------------------------
        // This method insert user        
        //--------------------------------------------------------------------------------------------------

        public bool InsertOrUpdateUser(UserMusic user)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@First", user.First);
            paramDic.Add("@Last", user.Last);
            paramDic.Add("@Email", user.Email);
            paramDic.Add("@Password", user.Password);
            paramDic.Add("@ImgUrl", user.ImgUrl);
            paramDic.Add("@RegistrationDate", DateTime.Now);
            cmd = CreateCommandWithStoredProcedure("Proj_SP_CreateOrUpdateUser", con, paramDic);             // create the command
                                                                                                // Set up the output parameter
            SqlParameter isSuccessParam = new SqlParameter("@IsSuccess", SqlDbType.Bit);
            isSuccessParam.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(isSuccessParam);


            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                //int numEffected = Convert.ToInt32(cmd.ExecuteScalar()); // returning the id
                bool isSuccess = (bool)isSuccessParam.Value;
                return isSuccess;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        //--------------------------------------------------------------------------------------------------
        // This method set key and date 
        //--------------------------------------------------------------------------------------------------
        public bool SetKeyAndDate(string key, DateTime date, string Email)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@Key", key);
            paramDic.Add("@Expired_key", date);
            paramDic.Add("@Email", Email);



            cmd = CreateCommandWithStoredProcedure("Proj_SP_SetKeyAndExpiredDateByEmail", con, paramDic);             // create the command


            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return Convert.ToBoolean(numEffected) ? Convert.ToBoolean(numEffected) : throw new Exception("Something wrong");

            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }
        //--------------------------------------------------------------------------------------------------
        // This method change the password 
        //--------------------------------------------------------------------------------------------------
        public bool ChangePassword(string email,string password, string passwordToChange)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@Email", email);
            paramDic.Add("@Password", password);
            paramDic.Add("@PasswordToChange", passwordToChange);



            cmd = CreateCommandWithStoredProcedure("Proj_SP_ChangePassword", con, paramDic);             // create the command


            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return Convert.ToBoolean(numEffected) ? Convert.ToBoolean(numEffected) : throw new Exception("Something wrong");

            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        //--------------------------------------------------------------------------------------------------
        // this method check the key and the date 
        //--------------------------------------------------------------------------------------------------
        public UserMusic checkIfKeyCorrect(string key, string email)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }


            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@Key", key);
            paramDic.Add("@Expired_key", DateTime.Now);
            paramDic.Add("@Email", email);


            cmd = CreateCommandWithStoredProcedure("Proj_SP_CheckKeyAndDate", con, paramDic);             // create the command
            var returnParameter = cmd.Parameters.Add("@returnValue", SqlDbType.Int);

            returnParameter.Direction = ParameterDirection.ReturnValue;





            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    UserMusic u = new UserMusic(
                        Convert.ToString(dataReader["Id"]),
                        Convert.ToString(dataReader["First"]),
                        Convert.ToString(dataReader["Last"]),
                        Convert.ToString(dataReader["Email"]),
                        Convert.ToString(dataReader["Password"]),
                        Convert.ToString(dataReader["ImgUrl"]),
                        Convert.ToDateTime(dataReader["RegistrationDate"])
                    );
                    return u;
                }
                throw new Exception("Key is wrong or expired");

            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
                // note that the return value appears only after closing the connection
                var result = returnParameter.Value;
            }

        }

        //--------------------------------------------------------------------------------------------------
        // This method Delete a user by email 
        //--------------------------------------------------------------------------------------------------
        public bool DeleteUser(string email)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@Email", email);



            cmd = CreateCommandWithStoredProcedure("Proj_SP_DeleteUser", con, paramDic);             // create the command


            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return Convert.ToBoolean(numEffected) ? Convert.ToBoolean(numEffected) : throw new Exception("User Not found");
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }
        // khaled add this:  **** ******************



        //--------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------

        //---------------------------------------------------------------------------------
        // Create the SqlCommand using a stored procedure
        //---------------------------------------------------------------------------------
        private SqlCommand CreateCommandWithStoredProcedure(String spName, SqlConnection con, Dictionary<string, object> paramDic)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            if (paramDic != null)
                foreach (KeyValuePair<string, object> param in paramDic)
                {
                    cmd.Parameters.AddWithValue(param.Key, param.Value);

                }


            return cmd;
        }

    }
}
