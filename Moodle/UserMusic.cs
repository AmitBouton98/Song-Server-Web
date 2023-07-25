using Microsoft.Extensions.Options;
using Server.Email;
using Server.Moodle.DAL;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Runtime.CompilerServices;

namespace Server.Moodle
{

    public class UserMusic
    {
        public string Id { get; private set; } // key
        public string First { get; private set; }
        public string Last { get; private set; }
        public string Email { get; private set; } // unique
        public string Password { get; private set; }
        public string ImgUrl { get; private set; }
        public DateTime RegistrationDate { get; private set; }
        
        public UserMusic(string id, string first, string last, string email, string password, string imgUrl, DateTime registrationDate)
        {
            Id = id;
            First = first;
            Last = last;
            Email = email;
            Password = password;
            ImgUrl = imgUrl;
            RegistrationDate = registrationDate;
        }
        public static List<UserMusic> GetAllUsers()
        {
            DBservices dBservices = new DBservices();
            return dBservices.GetAllUsers();
        }
        public static UserMusic GetUserById(string id)
        {
            DBservices dBservices = new DBservices();
            return dBservices.GetUserById(id);
        }
        public async static Task<UserMusic> GetUserByEmail(string email)
        {
            DBservices dBservices = new DBservices();
            var user =  dBservices.GetUserByEmail(email);
            await SetKeyAndEmail(user);
            return user;
        }
        public static UserMusic CheckUserExists(string email, string password)
        {
            DBservices dBservices = new DBservices();
            return dBservices.CheckUserExists(email, HashPassword(password)); // hash value with sha256 
        }
        public static UserMusic? checkIfKeyCorrect(string key, string email, string password)
        {
            DBservices dBservices = new DBservices();
            return dBservices.checkIfKeyCorrect(key, email, HashPassword(password));
        }
        public static bool ChangePassword(string id,string password, string passwordToChange)
        {
            DBservices dBservices = new DBservices();
            return dBservices.ChangePassword(id,HashPassword(password), HashPassword(passwordToChange));
        }
        public static bool InsertOrUpdateUser(UserMusic user)
        {
            DBservices dBservices = new DBservices();
            user.Password = HashPassword(user.Password); // Encrypt with sha256 algo
            return dBservices.InsertOrUpdateUser(user);
        }
        public async static Task<bool> SetKeyAndEmail(UserMusic user)
        {
            DBservices dBservices = new DBservices();
            string key = generateOneTimeResetUrl();
            DateTime date = DateTime.Now;
            await user.sendEmail(user.Email, user, key);
            return dBservices.SetKeyAndDate(key, date, user.Email);
        }
        private static string generateOneTimeResetUrl()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 6);
        }
        private async Task sendEmail(string email, UserMusic user, string key)
        {
            // your email format regex pattern
            string pattern = @"^([a-z\d\.-]+)@([a-z\d-]+)\.([a-z]{2,8})(\.[a-z]{2,8})?$";

            if (!Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase))
            {
                return;
            }

            var smtpSettings = new SmtpSettings("smtp.elasticemail.com", 587, true, "amit.khaled.airbnb@gmail.com", "6BA88EB97CC6AE035885DC0CD3A95BB30CC8");
            var emailSender = new EmailSender(Options.Create(smtpSettings));
            string subject = "Your unique key";
            string copyCodeScript = @"
            <script>
            function copyCode() {
                const input = document.querySelector('#myInput');
                input.select();
                document.execCommand('copy');
            }
            </script>";
            string message = $@"
            <!DOCTYPE html>
            <html lang=""en"">
            <body style=""background-color: rgba(0, 0, 0, 0.761);"">
            <div style=""text-align: center;"">
                <img src=""https://media.discordapp.net/attachments/1092515523717234700/1130442483952795699/touch-icon-ipad-retina.png"" alt="""" style=""object-fit: contain; max-height: 250px;"">
                <h1 style=""color:white; text-align: center;"">Hello {user.First} {user.Last}, This is the 6 digits code that you can use for
                    reseting your password</h1>
                <p style=""color: white;"">select the numbers to copy</p>
                <div style=""display:flex; justify-content: center; align-items: center;"">
                    <div style="" margin:0 auto; display: inline-flex; justify-content: center; align-items: center; background-color: #fe7d7dA2;  font-size: 26px; color: white; padding: 10px; box-shadow: inset 0 0 10px white; border-radius: 5px;"">
                        &#128203;
                        <input type=""text"" id=""myInput"" value=""{key}"" readonly
                            style="" cursor: copy; border-style: none;  background-color: transparent; font-size: 26px; letter-spacing: 8px; color: whitepadding: 10px;""
                            size=""5"" onclick=""copyCode()"" >
                    </div>
                </div>
                <p style=""color: white;"">This code will be useable for 30 minutes if you didnt use it in the comming 30 minutes
                    it will expeared.
                </p>
            </div>
            {copyCodeScript}
            </body>
            </html>";
            await emailSender.SendEmailAsync(email, subject, message);
        }
        public static bool Delete(string email)
        {
            DBservices dBservices = new DBservices();
            return dBservices.DeleteUser(email);
        }
        public static int GetNumberOfUsers()
        {
            DBservices dBservices = new DBservices();
            return dBservices.GetNumberOfUsers();
        }
        public static int GetNumberOfPlayedForUser(string UserId)
        {
            DBservices dBservices = new DBservices();
            return dBservices.GetNumberOfPlayedForUser(UserId);
        }
        //this sha256 algo encrypt the code
        private static string key = "123";

        public static string HashPassword(string password)
        {
            string passwordWithKey = password + key;

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(passwordWithKey);
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }

    }
}
