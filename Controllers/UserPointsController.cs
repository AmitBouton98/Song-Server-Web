using Microsoft.AspNetCore.Mvc;
using Server.Moodle;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPointsController : ControllerBase
    {
        
        // GET: api/<UserPointsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UserPointsController>/5
        [HttpGet]
        [Route("GetTop10Scores")]
        public IActionResult GetTop10Scores() // sending email
        {
            try
            {
                var usr = UserPoint.GetTop10Scores();
                return Ok(usr);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { error = ex.Message });
            }

        }

        // POST api/<UserPointsController>
        [HttpPost]
        public IActionResult Post([FromBody] UserPoint up)
        {
            try
            {
                bool check = UserPoint.CreateOrUpdateScore(up);
                return check ? Ok("Score added") : Ok("Score updated");
                //return UserMusic.InsertOrUpdateUser(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { error = ex.Message });

            }

        }

        // PUT api/<UserPointsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE api/<UserPointsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
