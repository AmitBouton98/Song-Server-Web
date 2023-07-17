using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Server.Moodle;
using Server.Moodle.DAL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserMusicsController : ControllerBase
    {
        // GET: api/<UserMusicsController>
        [HttpGet]
        public List<UserMusic> Get()
        {
            return UserMusic.GetAllUsers();
        }

        // GET api/<UserMusicsController>/5
        [HttpGet]
        [Route("GetByemail/email/{email}")]
        public async Task<IActionResult> GetByemail(string email) // sending email
        {
            try
            {
                var usr = await UserMusic.GetUserByEmail(email);
                return Ok(usr);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { error = ex.Message });
            }
            
        }
        [HttpGet]
        [Route("CheckIfKeyCorrect")]
        public IActionResult CheckIfKeyCorrect(string key, string email)
        {
            try
            {
                return Ok(UserMusic.checkIfKeyCorrect(key, email));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet]
        [Route("GetNumberOfUsers")]
        public IActionResult GetNumberOfUsers()
        {
            try
            {
                return Ok(UserMusic.GetNumberOfUsers());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        // GET api/<UserMusicsController>/5
        [HttpGet]
        [Route("CheckIfExists")]
        public IActionResult CheckIfExists(string email, string password)
        {
            try
            {
                return Ok(UserMusic.CheckUserExists(email,password));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { error = ex.Message });
            }
        }
        // POST api/<UserMusicsController>
        [HttpPost]
        public IActionResult Post([FromBody]UserMusic user)
        {
            try
            {
                bool check = UserMusic.InsertOrUpdateUser(user);
                return check? Ok("User added") : Ok("User Updated");
                //return UserMusic.InsertOrUpdateUser(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { error = ex.Message });

            }
        }

        // PUT api/<UserMusicsController>/5
        [HttpPut]
        [Route("Put")]
        public IActionResult Put(string id,string password, string passwordToChange)
        {
            try
            {
                return Ok(UserMusic.ChangePassword(id,password, passwordToChange));
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { error = ex.Message });
            }
        }

        // DELETE api/<WebUsersController>/5
        [HttpDelete("{email}")]
        public IActionResult Delete(string email)
        {
            try
            {
                var u = UserMusic.Delete(email);
                return Ok(u);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
