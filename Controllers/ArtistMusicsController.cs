using Microsoft.AspNetCore.Mvc;
using Server.Moodle;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistMusicsController : ControllerBase
    {
        [HttpGet]
        public List<ArtistMusic> Get()
        {
            return ArtistMusic.GetAllArtists();
        }

        [HttpGet]
        [Route("GetArtistByName/name/{name}")]
        public  IActionResult GetArtistByName(string name)
        {
            try
            {
                return Ok(ArtistMusic.GetArtistByName(name));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { error = ex.Message });
            }

        }
        [HttpGet]
        [Route("GetNumberOfArtists")]
        public IActionResult GetNumberOfArtists()
        {
            try
            {
                return Ok(ArtistMusic.GetNumberOfArtists());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet]
        [Route("GetTop1ArtistForUser")]
        public IActionResult GetTop1ArtistForUser(string UserId)
        {
            try
            {
                return Ok(ArtistMusic.GetTop1ArtistForUser(UserId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

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
        [HttpGet]
        [Route("GetTop10Artists")]
        public IActionResult GetTop10Artists()
        {
            try
            {
                var usr = ArtistMusic.GetTop10Artists();
                return Ok(usr);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { error = ex.Message });
            }

        }
        [HttpPost]
        public IActionResult Post([FromBody] ArtistMusic artist)
        {
            try
            {
                bool check = ArtistMusic.InsertOrUpdateArtist(artist);
                return check ? Ok("Artist added") : Ok("Artist Updated");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { error = ex.Message });

            }
        }

        // need to change to add favorit to user  FLAGKHALED
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
        // need to change to add favorit to user  FLAGKHALED
        // PUT api/<UserMusicsController>/5
        [HttpPut]
        [Route("UpdateArtistUrl")]
        public IActionResult UpdateArtistUrl(string ArtistName, string ArtistUrl)
        {
            try
            {
                return Ok(ArtistMusic.UpdateArtistUrl(ArtistName, ArtistUrl));
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
        [HttpDelete("{name}")]
        public IActionResult Delete(string name)
        {
            try
            {
                var u = ArtistMusic.Delete(name);
                return Ok(u);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
