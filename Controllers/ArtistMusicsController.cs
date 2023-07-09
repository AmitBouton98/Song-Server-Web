using Microsoft.AspNetCore.Mvc;
using Server.Moodle;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistMusicsController : ControllerBase
    {
        // GET: api/<ArtistMusicsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UserMusicsController>/5
        [HttpGet]
        [Route("GetFavoriteArtistByUserId/UserId/{UserId}")]
        public IActionResult GetFavoriteArtistByUserId(string UserId)
        {
            try
            {
                var usr = ArtistMusic.GetFavoriteArtistByUserId(UserId);
                return Ok(usr);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { error = ex.Message });
            }

        }
        // GET api/<UserMusicsController>/5
        [HttpGet]
        [Route("GetNumberOfPlayedForGivenArtist/ArtistName/{ArtistName}")]
        public IActionResult GetNumberOfPlayedForGivenArtist(string ArtistName)
        {
            try
            {
                var usr = ArtistMusic.GetNumberOfPlayedForGivenArtist(ArtistName);
                return Ok(usr);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { error = ex.Message });
            }

        }
        // GET api/<UserMusicsController>/5
        [HttpGet]
        [Route("GetTheNumberOfAppearanceInUserByGivenArtist/ArtistName/{ArtistName}")]
        public IActionResult GetTheNumberOfAppearanceInUserByGivenArtist(string ArtistName)
        {
            try
            {
                var usr = ArtistMusic.GetTheNumberOfAppearanceInUserByGivenArtist(ArtistName);
                return Ok(usr);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { error = ex.Message });
            }

        }
        // POST api/<ArtistMusicsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserMusicsController>/5
        [HttpPut]
        [Route("Put")]
        public IActionResult Put(string UserId, string ArtistName)
        {
            try
            {
                return Ok(ArtistMusic.AddFavoriteArtist(UserId, ArtistName));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { error = ex.Message });
            }
        }

        // DELETE api/<WebUsersController>/5
        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete(string UserId, string ArtistName)
        {
            try
            {
                return Ok(ArtistMusic.DeleteFavoriteArtist(UserId, ArtistName));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { error = ex.Message });
            }
        }
    }
}
