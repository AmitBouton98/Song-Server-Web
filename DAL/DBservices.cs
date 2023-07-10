using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using Server.Moodle;
using System.Globalization;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Xml.Linq;

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
        // start amit
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
        //--------------------------------------------------------------------------------------------------
        // This method add favorite song to user by id 
        //--------------------------------------------------------------------------------------------------
        public bool AddFavoriteSong(string UserId, string SongId)
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
            paramDic.Add("@UserId", UserId);
            paramDic.Add("@SongId", SongId);
            cmd = CreateCommandWithStoredProcedure("Proj_SP_AddFavoriteSong", con, paramDic);             // create the command
                                                                                                             // Set up the output parameter
            //SqlParameter isSuccessParam = new SqlParameter("@IsSuccess", SqlDbType.Bit);
            //isSuccessParam.Direction = ParameterDirection.Output;
            //cmd.Parameters.Add(isSuccessParam);


            try
            {
                //int numEffected = cmd.ExecuteNonQuery(); // execute the command
                int numEffected = Convert.ToInt32(cmd.ExecuteScalar()); // returning the id
                //bool isSuccess = (bool)isSuccessParam.Value;
                return numEffected == 0 ? true : false ;
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
        // This method delete favorite song to user by id 
        //--------------------------------------------------------------------------------------------------
        public bool DeleteFavoriteSong(string UserId, string SongId)
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
            paramDic.Add("@UserId", UserId);
            paramDic.Add("@SongId", SongId);
            cmd = CreateCommandWithStoredProcedure("Proj_SP_DeleteFavoriteSong", con, paramDic);             // create the command
                                                                                                          // Set up the output parameter
                                                                                                          //SqlParameter isSuccessParam = new SqlParameter("@IsSuccess", SqlDbType.Bit);
                                                                                                          //isSuccessParam.Direction = ParameterDirection.Output;
                                                                                                          //cmd.Parameters.Add(isSuccessParam);


            try
            {
                //int numEffected = cmd.ExecuteNonQuery(); // execute the command
                int numEffected = Convert.ToInt32(cmd.ExecuteScalar()); // returning the id
                //bool isSuccess = (bool)isSuccessParam.Value;
                return numEffected == 0 ? true : false;
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
        // This method get favotie song by user id         
        //--------------------------------------------------------------------------------------------------
        public List<SongMusic> GetFavoriteSongByUserId(string UserId)
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
            paramDic.Add("@UserId", UserId);


            cmd = CreateCommandWithStoredProcedure("Proj_SP_GetFavoriteSongByUserId", con, paramDic);             // create the command
            var returnParameter = cmd.Parameters.Add("@returnValue", SqlDbType.Int);

            returnParameter.Direction = ParameterDirection.ReturnValue;



            try
            {
                List<SongMusic> SongMusicList = new List<SongMusic>();
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {


                    SongMusic u = new SongMusic(
                        Convert.ToString(dataReader["Id"]),
                        Convert.ToString(dataReader["ArtistName"]),
                        Convert.ToString(dataReader["Name"]),
                        Convert.ToString(dataReader["Likes"]),
                        Convert.ToString(dataReader["LyricLink"]),
                        Convert.ToString(dataReader["PlayLink"])
                    );
                    SongMusicList.Add(u);
                }
                return SongMusicList;
                //throw new Exception("There is no favotie song for this user");
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
        // This method add favorite artist to user by id 
        //--------------------------------------------------------------------------------------------------
        public bool AddFavoriteArtist(string UserId, string ArtistName)
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
            paramDic.Add("@UserId", UserId);
            paramDic.Add("@ArtistName", ArtistName);
            cmd = CreateCommandWithStoredProcedure("Proj_SP_AddFavoriteArtist", con, paramDic);             // create the command
                                                                                                          // Set up the output parameter
                                                                                                          //SqlParameter isSuccessParam = new SqlParameter("@IsSuccess", SqlDbType.Bit);
                                                                                                          //isSuccessParam.Direction = ParameterDirection.Output;
                                                                                                          //cmd.Parameters.Add(isSuccessParam);


            try
            {
                //int numEffected = cmd.ExecuteNonQuery(); // execute the command
                int numEffected = Convert.ToInt32(cmd.ExecuteScalar()); // returning the id
                //bool isSuccess = (bool)isSuccessParam.Value;
                return numEffected == 0 ? true : false;
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
        // This method delete favorite artist to user by id 
        //--------------------------------------------------------------------------------------------------
        public bool DeleteFavoriteArtist(string UserId, string ArtistName)
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
            paramDic.Add("@UserId", UserId);
            paramDic.Add("@ArtistName", ArtistName);
            cmd = CreateCommandWithStoredProcedure("Proj_SP_DeleteFaviriteArtist", con, paramDic);             // create the command
                                                                                                             // Set up the output parameter
                                                                                                             //SqlParameter isSuccessParam = new SqlParameter("@IsSuccess", SqlDbType.Bit);
                                                                                                             //isSuccessParam.Direction = ParameterDirection.Output;
                                                                                                             //cmd.Parameters.Add(isSuccessParam);


            try
            {
                //int numEffected = cmd.ExecuteNonQuery(); // execute the command
                int numEffected = Convert.ToInt32(cmd.ExecuteScalar()); // returning the id
                //bool isSuccess = (bool)isSuccessParam.Value;
                return numEffected == 0 ? true : false;
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
        // This method get favotie song by user id         
        //--------------------------------------------------------------------------------------------------
        public List<ArtistMusic> GetFavoriteArtistByUserId(string UserId)
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
            paramDic.Add("@UserId", UserId);


            cmd = CreateCommandWithStoredProcedure("Proj_SP_GetFavoriteArtistsByUserId", con, paramDic);             // create the command
            var returnParameter = cmd.Parameters.Add("@returnValue", SqlDbType.Int);

            returnParameter.Direction = ParameterDirection.ReturnValue;



            try
            {
                List<ArtistMusic> ArtistMusicList = new List<ArtistMusic>();
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {


                    ArtistMusic u = new ArtistMusic(
                        Convert.ToString(dataReader["ArtistName"]),
                        Convert.ToString(dataReader["Likes"])
                    );
                    ArtistMusicList.Add(u);
                }
                return ArtistMusicList;
                //throw new Exception("There is no favotie song for this user");
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
        // get the number of appearance in user favorite by given artist
        //--------------------------------------------------------------------------------------------------
        public int GetTheNumberOfAppearanceInUserByGivenArtist(string ArtistName)
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
            paramDic.Add("@ArtistName", ArtistName);


            cmd = CreateCommandWithStoredProcedure("Proj_SP_GetNumberOfAppearanceInUsersByGivenArtist", con, paramDic);             // create the command
            var returnParameter = cmd.Parameters.Add("@returnValue", SqlDbType.Int);

            returnParameter.Direction = ParameterDirection.ReturnValue;



            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    return Convert.ToInt32(dataReader["NumberOfAppearances"]);

                }
                throw new Exception("There is no favoties for this artist");
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
        // get the number of appearance in user favorite by given song
        //--------------------------------------------------------------------------------------------------
        public int GetTheNumberOfAppearanceInUserByGivenSong(string SongId)
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
            paramDic.Add("@SongId", SongId);


            cmd = CreateCommandWithStoredProcedure("Proj_SP_GetNumberOfAppearanceInUsersByGivenSong", con, paramDic);             // create the command
            var returnParameter = cmd.Parameters.Add("@returnValue", SqlDbType.Int);

            returnParameter.Direction = ParameterDirection.ReturnValue;



            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    return Convert.ToInt32(dataReader["NumberOfAppearances"]);

                }
                throw new Exception("There is no favoties for this artist");
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
        // This method get top 5 favotie song by user id         
        //--------------------------------------------------------------------------------------------------
        public List<SongMusic> GetTop5SongsForUser(string UserId)
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
            paramDic.Add("@UserId", UserId);


            cmd = CreateCommandWithStoredProcedure("Proj_SP_GetTop5SongsForUser", con, paramDic);             // create the command
            var returnParameter = cmd.Parameters.Add("@returnValue", SqlDbType.Int);

            returnParameter.Direction = ParameterDirection.ReturnValue;



            try
            {
                List<SongMusic> SongMusicList = new List<SongMusic>();
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {


                    SongMusic u = new SongMusic(
                        Convert.ToString(dataReader["Id"]),
                        Convert.ToString(dataReader["ArtistName"]),
                        Convert.ToString(dataReader["Name"]),
                        Convert.ToString(dataReader["Likes"]),
                        Convert.ToString(dataReader["LyricLink"]),
                        Convert.ToString(dataReader["PlayLink"])
                    );
                    SongMusicList.Add(u);
                }
                return SongMusicList;
                //throw new Exception("There is no favotie song for this user");
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
        // create or update Number of players        
        //--------------------------------------------------------------------------------------------------

        public bool CreateOrUpdateNumberOfPlayed(string SongId, string UserId)
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
            paramDic.Add("@SongId", SongId);
            paramDic.Add("@UserId", UserId);

            cmd = CreateCommandWithStoredProcedure("Proj_SP_CreateOrUpdateNumberOfPlayed", con, paramDic);             // create the command
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
        // This method get top 5 song for artist
        //--------------------------------------------------------------------------------------------------
        public List<SongMusic> GetTop5SongsForArtist(string ArtistName)
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
            paramDic.Add("@ArtistName", ArtistName);


            cmd = CreateCommandWithStoredProcedure("Proj_SP_GetTop5SongsForArtist", con, paramDic);             // create the command
            var returnParameter = cmd.Parameters.Add("@returnValue", SqlDbType.Int);

            returnParameter.Direction = ParameterDirection.ReturnValue;



            try
            {
                List<SongMusic> SongMusicList = new List<SongMusic>();
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    SongMusic u = new SongMusic(
                                            Convert.ToString(dataReader["Id"]),
                                            Convert.ToString(dataReader["ArtistName"]),
                                            Convert.ToString(dataReader["Name"]),
                                            Convert.ToString(dataReader["Likes"]),
                                            Convert.ToString(dataReader["LyricLink"]),
                                            Convert.ToString(dataReader["PlayLink"])
                                        );
                    SongMusicList.Add(u);
                }
                return SongMusicList;
                //throw new Exception("There is no favotie song for this user");
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
        // This method get global 10 songs
        //--------------------------------------------------------------------------------------------------
        public List<SongMusic> GetTop10GlobalSongs()
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
            //paramDic.Add("@ArtistName", ArtistName);


            cmd = CreateCommandWithStoredProcedure("Proj_SP_GetTop10GlobalSongs", con, null);             // create the command
            var returnParameter = cmd.Parameters.Add("@returnValue", SqlDbType.Int);

            returnParameter.Direction = ParameterDirection.ReturnValue;



            try
            {
                List<SongMusic> SongMusicList = new List<SongMusic>();
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    SongMusic u = new SongMusic(
                                            Convert.ToString(dataReader["Id"]),
                                            Convert.ToString(dataReader["ArtistName"]),
                                            Convert.ToString(dataReader["Name"]),
                                            Convert.ToString(dataReader["Likes"]),
                                            Convert.ToString(dataReader["LyricLink"]),
                                            Convert.ToString(dataReader["PlayLink"])
                                        );
                    SongMusicList.Add(u);
                }
                return SongMusicList;
                //throw new Exception("There is no favotie song for this user");
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
        // get the number of played for given artist
        //--------------------------------------------------------------------------------------------------
        public int GetNumberOfPlayedForGivenArtist(string ArtistName)
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
            paramDic.Add("@ArtistName", ArtistName);


            cmd = CreateCommandWithStoredProcedure("Proj_SP_GetNumberOfPlayedForGivenArtist", con, paramDic);             // create the command
            var returnParameter = cmd.Parameters.Add("@returnValue", SqlDbType.Int);

            returnParameter.Direction = ParameterDirection.ReturnValue;



            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    return Convert.ToInt32(dataReader["TotalPlays"]);

                }
                throw new Exception("There is no played for this artist");
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
        // get the number of appearance in user favorite by given song
        //--------------------------------------------------------------------------------------------------
        public int GetTheNumberPlayedForGivenSong(string SongId)
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
            paramDic.Add("@SongId", SongId);


            cmd = CreateCommandWithStoredProcedure("Proj_SP_GetTheNumberPlayedForGivenSong", con, paramDic);             // create the command
            var returnParameter = cmd.Parameters.Add("@returnValue", SqlDbType.Int);

            returnParameter.Direction = ParameterDirection.ReturnValue;



            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    return Convert.ToInt32(dataReader["TotalPlays"]);

                }
                throw new Exception("There is no played for this song");
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
        // end amit

        // khaled add this:  **** ****************** KHALEDFLAG.


        //--------------------------------------------------------------------------------------------------
        // This method insert artist        
        //--------------------------------------------------------------------------------------------------
        public bool InsertArtistOrUpdate(ArtistMusic artist)
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
            paramDic.Add("@ArtistName", artist.ArtistName);
            paramDic.Add("@Likes", artist.Likes);
            // create the command
            cmd = CreateCommandWithStoredProcedure("Proj_SP_CreateOrUpdateArtist", con, paramDic);             
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
        // This method Delete a artist by name 
        //--------------------------------------------------------------------------------------------------
        public bool DeleteArtist(string name)
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
            paramDic.Add("@ArtistName", name);
            // create the command
            cmd = CreateCommandWithStoredProcedure("Proj_SP_DeleteArtist", con, paramDic);            
            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return Convert.ToBoolean(numEffected) ? Convert.ToBoolean(numEffected) : throw new Exception("Artist Not found");
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
        // This method get artist by name        
        //--------------------------------------------------------------------------------------------------
        public ArtistMusic GetArtistByName(string name)
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
            paramDic.Add("@ArtistName", name);


            cmd = CreateCommandWithStoredProcedure("Proj_SP_GetArtistByName", con, paramDic);             // create the command
            var returnParameter = cmd.Parameters.Add("@returnValue", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                        ArtistMusic a = new ArtistMusic(
                            Convert.ToString(dataReader["ArtistName"]),
                            Convert.ToString(dataReader["Likes"])
                        );
                    return a;
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
        // This method get all the artists 
        //--------------------------------------------------------------------------------------------------
        public List<ArtistMusic> GetAllArtists()
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
            // create the command
            cmd = CreateCommandWithStoredProcedure("Proj_SP_GetAllArtists", con, null);             
            var returnParameter = cmd.Parameters.Add("@returnValue", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;
            List<ArtistMusic> ArtistsList = new List<ArtistMusic>();

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    ArtistMusic a = new ArtistMusic(
                        Convert.ToString(dataReader["ArtistName"]),
                        Convert.ToString(dataReader["Likes"])
                    );
                    ArtistsList.Add(a);
                }
                return ArtistsList;
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
        // This method insert song        
        //--------------------------------------------------------------------------------------------------
        public bool InsertSongOrUpdate(SongMusic song)
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
            paramDic.Add("@Id", song.Id);
            paramDic.Add("@ArtistName", song.ArtistName);
            paramDic.Add("@Name", song.Name);
            paramDic.Add("@Likes", song.Likes);
            paramDic.Add("@LyricLink", song.LyricLink);
            paramDic.Add("@PlayLink", song.PlayLink);

            // create the command
            cmd = CreateCommandWithStoredProcedure("Proj_SP_CreateOrUpdateSong", con, paramDic);
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
        // This method Delete a song by id 
        //--------------------------------------------------------------------------------------------------
        public bool DeleteSong(string id)
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
            try
            {
                paramDic.Add("@Id", Convert.ToInt32(id));
            }
            catch
            {
                throw new Exception("Id Not in correct format");
            }
            // create the command
            cmd = CreateCommandWithStoredProcedure("Proj_SP_DeleteSong", con, paramDic);
            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return Convert.ToBoolean(numEffected) ? Convert.ToBoolean(numEffected) : throw new Exception("song Not found");
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
        // This method get song by id        
        //--------------------------------------------------------------------------------------------------
        public SongMusic GetSongById(string id)
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
            try
            {
                paramDic.Add("@Id", Convert.ToInt32(id));
            }
            catch
            {
                throw new Exception("Id Not in correct format");
            }


            cmd = CreateCommandWithStoredProcedure("Proj_SP_GetSongById", con, paramDic);             // create the command
            var returnParameter = cmd.Parameters.Add("@returnValue", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    SongMusic s = new SongMusic(
                        Convert.ToString(dataReader["Id"]),
                        Convert.ToString(dataReader["ArtistName"]),
                        Convert.ToString(dataReader["Name"]),
                        Convert.ToString(dataReader["Likes"]),
                        Convert.ToString(dataReader["LyricLink"]),
                        Convert.ToString(dataReader["PlayLink"])
                    );
                    return s;
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
        // This method get song by names        
        //--------------------------------------------------------------------------------------------------
        public SongMusic GetSongByName(string name)
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
            paramDic.Add("@Name", name);
           
            


            cmd = CreateCommandWithStoredProcedure("Proj_SP_GetSongByName", con, paramDic);             // create the command
            var returnParameter = cmd.Parameters.Add("@returnValue", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    SongMusic s = new SongMusic(
                        Convert.ToString(dataReader["Id"]),
                        Convert.ToString(dataReader["ArtistName"]),
                        Convert.ToString(dataReader["Name"]),
                        Convert.ToString(dataReader["Likes"]),
                        Convert.ToString(dataReader["LyricLink"]),
                        Convert.ToString(dataReader["PlayLink"])
                    );
                    return s;
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
        // This method get all the songs by artist name 
        //--------------------------------------------------------------------------------------------------
        public List<SongMusic> GetAllArtistSongs(string name)
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
            paramDic.Add("@ArtistName", name);
            // create the command
            cmd = CreateCommandWithStoredProcedure("Proj_SP_GetArtistSongs", con, paramDic);
            var returnParameter = cmd.Parameters.Add("@returnValue", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;
            List<SongMusic> SongsList = new List<SongMusic>();

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    SongMusic s = new SongMusic(
                       Convert.ToString(dataReader["Id"]),
                       Convert.ToString(dataReader["ArtistName"]),
                       Convert.ToString(dataReader["Name"]),
                       Convert.ToString(dataReader["Likes"]),
                       Convert.ToString(dataReader["LyricLink"]),
                       Convert.ToString(dataReader["PlayLink"])
                   );
                    SongsList.Add(s);
                }
                return SongsList;
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
        // This method get all the songs
        //--------------------------------------------------------------------------------------------------
        public List<SongMusic> GetAllSongs()
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
   
            // create the command
            cmd = CreateCommandWithStoredProcedure("Proj_SP_GetAllSongs", con, null);
            var returnParameter = cmd.Parameters.Add("@returnValue", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;
            List<SongMusic> SongsList = new List<SongMusic>();

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dataReader.Read())
                {
                    SongMusic s = new SongMusic(
                       Convert.ToString(dataReader["Id"]),
                       Convert.ToString(dataReader["ArtistName"]),
                       Convert.ToString(dataReader["Name"]),
                       Convert.ToString(dataReader["Likes"]),
                       Convert.ToString(dataReader["LyricLink"]),
                       Convert.ToString(dataReader["PlayLink"])
                   );
                    SongsList.Add(s);
                }
                return SongsList;
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
        // This method insert song comment        
        //--------------------------------------------------------------------------------------------------
        public bool InsertSongCommentOrUpdate(SongComment songComment)
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
            paramDic.Add("@UserId", songComment.UserId);
            paramDic.Add("@Id", songComment.Id);
            paramDic.Add("@Comment",songComment.Text);
            paramDic.Add("@CreateDate", songComment.CreateDate);
            paramDic.Add("@SongId", songComment.SongId);
            // create the command
            cmd = CreateCommandWithStoredProcedure("Proj_SP_CreateOrUpdateCommentForSong", con, paramDic);
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
        // This method get all the songs
        //--------------------------------------------------------------------------------------------------
        public List<SongComment> GetAllSongComments(string songId)
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
            paramDic.Add("@Id", songId);
            // create the command
            cmd = CreateCommandWithStoredProcedure("Proj_SP_GetCommentForSong", con, paramDic);
            var returnParameter = cmd.Parameters.Add("@returnValue", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;
            List<SongComment> CommentsList = new List<SongComment>();

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dataReader.Read())
                {
                    SongComment sc = new SongComment(
                       Convert.ToString(dataReader["UserId"]),
                       Convert.ToString(dataReader["Id"]),
                       Convert.ToString(dataReader["Comment"]),
                       Convert.ToString(dataReader["CreateDate"]),
                       Convert.ToString(dataReader["SongId"])
                   );
                    CommentsList.Add(sc);
                }
                return CommentsList;
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
        // This method Delete a songComment by id 
        //--------------------------------------------------------------------------------------------------
        public bool DeleteSongComment(string id)
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
            try
            {
                paramDic.Add("@Id", Convert.ToInt32(id));
            }
            catch
            {
                throw new Exception("Id Not in correct format");
            }
            // create the command
            cmd = CreateCommandWithStoredProcedure("Proj_SP_DeletSongComment", con, paramDic);
            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return Convert.ToBoolean(numEffected) ? Convert.ToBoolean(numEffected) : throw new Exception("Comment Not found");
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
        // This method insert artist comment        
        //--------------------------------------------------------------------------------------------------
        public bool InsertArtistCommentOrUpdate(ArtistComment artistComment)
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
            paramDic.Add("@UserId", artistComment.UserId);
            paramDic.Add("@Id", artistComment.Id);
            paramDic.Add("@Comment", artistComment.Text);
            paramDic.Add("@CreateDate", artistComment.CreateDate);
            paramDic.Add("@ArtistName", artistComment.ArtisName);
            // create the command
            cmd = CreateCommandWithStoredProcedure("Proj_SP_CreateOrUpdateCommentForArtist", con, paramDic);
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
        // This method get all the artistComments
        //--------------------------------------------------------------------------------------------------
        public List<ArtistComment> GetAllArtistComments(string artistName)
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
            paramDic.Add("@ArtistName", artistName);
            // create the command
            cmd = CreateCommandWithStoredProcedure("Proj_SP_GetCommentForArtist", con, paramDic);
            var returnParameter = cmd.Parameters.Add("@returnValue", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;
            List<ArtistComment> CommentsList = new List<ArtistComment>();

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dataReader.Read())
                {
                    ArtistComment ac = new ArtistComment(
                       Convert.ToString(dataReader["UserId"]),
                       Convert.ToString(dataReader["Id"]),
                       Convert.ToString(dataReader["Comment"]),
                       Convert.ToString(dataReader["CreateDate"]),
                       Convert.ToString(dataReader["ArtistName"])
                   );
                    CommentsList.Add(ac);
                }
                return CommentsList;
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
        // This method Delete a artistComment by id 
        //--------------------------------------------------------------------------------------------------
        public bool DeleteArtistComment(string id)
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
            try
            {
                paramDic.Add("@Id", Convert.ToInt32(id));
            }
            catch
            {
                throw new Exception("Id Not in correct format");
            }
            // create the command
            cmd = CreateCommandWithStoredProcedure("Proj_SP_DeletArtistComment", con, paramDic);
            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return Convert.ToBoolean(numEffected) ? Convert.ToBoolean(numEffected) : throw new Exception("Comment Not found");
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
