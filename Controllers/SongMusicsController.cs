using Microsoft.AspNetCore.Mvc;
using Server.Moodle;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongMusicsController : ControllerBase
    {
        // GET: api/<SongMusicsController>
        [HttpGet]
        public List<SongMusic> Get()
        {
            return SongMusic.GetAllSongs();
        }
        [HttpGet]
        [Route("GetById/id/{id}")]
        public IActionResult GetById(string id)
        {
            try
            {
                return Ok(SongMusic.GetSongById(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { error = ex.Message });
            }
        }
        [HttpGet]
        [Route("GetNumberOfSongs")]
        public IActionResult GetNumberOfSongs()
        {
            try
            {
                return Ok(SongMusic.GetNumberOfSongs());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet]
        [Route("GetSongByName/name/{name}")]
        public IActionResult GetSongByName(string name)
        {
            try
            {
                return Ok(SongMusic.GetSongByName(name));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { error = ex.Message });
            }
        }
        [HttpGet]
        [Route("GetSongByText/text/{text}")]
        public IActionResult GetSongByText(string text)
        {
            try
            {
                return Ok(SongMusic.GetSongByText(text));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { error = ex.Message });
            }
        }
        [HttpGet]
        [Route("GetByArtistName/artistName/{artistName}")]
        public IActionResult GetByArtistName(string artistName)
        {
            try
            {
                return Ok(SongMusic.GetSongByArtistName(artistName));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { error = ex.Message });
            }

        }
        // GET api/<UserMusicsController>/5
        [HttpGet]
        [Route("GetFavoriteSongByUserId/UserId/{UserId}")]
        public IActionResult GetFavoriteSongByUserId(string UserId) 
        {
            try
            {
                var usr =  SongMusic.GetFavoriteSongByUserId(UserId);
                return Ok(usr);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { error = ex.Message });
            }

        }
        // GET api/<UserMusicsController>/5
        [HttpGet]
        [Route("GetSongsUserMightLike/UserId/{UserId}")]
        public IActionResult GetSongsUserMightLike(string UserId)
        {
            try
            {
                var usr = SongMusic.GetSongsUserMightLike(UserId);
                return Ok(usr);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { error = ex.Message });
            }

        }

        // GET api/<UserMusicsController>/5
        [HttpGet]
        [Route("GetTheNumberPlayedForGivenSong/SongId/{SongId}")]
        public IActionResult GetTheNumberPlayedForGivenSong(string SongId)
        {
            try
            {
                var usr = SongMusic.GetTheNumberPlayedForGivenSong(SongId);
                return Ok(usr);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { error = ex.Message });
            }

        }
        [HttpGet]
        [Route("GetTop5SongsForUser")]
        public IActionResult GetTop5SongsForUser(string UserId)
        {
            try
            {
                return Ok(SongMusic.GetTop5SongsForUser(UserId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet]
        [Route("GetTop5SongsForArtist")]
        public IActionResult GetTop5SongsForArtist(string ArtistName)
        {
            try
            {
                return Ok(SongMusic.GetTop5SongsForArtist(ArtistName));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("GetTop10GlobalSongs")]
        public IActionResult GetTop10GlobalSongs()
        {
            try
            {
                return Ok(SongMusic.GetTop10GlobalSongs());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // GET api/<UserMusicsController>/5
        [HttpGet]
        [Route("GetTheNumberOfAppearanceInUserByGivenSong/SongId/{SongId}")]
        public IActionResult GetTheNumberOfAppearanceInUserByGivenSong(string SongId)
        {
            try
            {
                var usr = SongMusic.GetTheNumberOfAppearanceInUserByGivenSong(SongId);
                return Ok(usr);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { error = ex.Message });
            }

        }

        [HttpPost]
        public IActionResult Post([FromBody] SongMusic song)
        {
            try
            {
                bool check = SongMusic.InsertOrUpdateSong(song);
                return check ? Ok("Song added") : Ok("Song Updated");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { error = ex.Message });

            }
        }

        // PUT api/<UserMusicsController>/5
        [HttpPut]
        [Route("Put")]
        public IActionResult Put(string UserId, string SongId)
        {
            try
            {
                return Ok(SongMusic.AddFavoriteSong(UserId, SongId));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { error = ex.Message });
            }
        }
        [HttpPut]
        [Route("ChangeSongUrl")]
        public IActionResult ChangeSongUrl(string SongId, string Url)
        {
            try
            {
                return Ok(SongMusic.ChangeSongUrl(SongId, Url));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { error = ex.Message });
            }
        }
        [HttpPut]
        [Route("ChangeYoutubeIdSong")]
        public IActionResult ChangeYoutubeIdSong(string SongId, string YoutubeId)
        {
            try
            {
                return Ok(SongMusic.ChangeYoutubeIdSong(SongId, YoutubeId));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { error = ex.Message });
            }
        }
        // PUT api/<UserMusicsController>/5
        [HttpPut]
        [Route("CreateOrUpdateNumberOfPlayed")]
        public IActionResult CreateOrUpdateNumberOfPlayed(string SongId, string UserId)
        {
            try
            {
                return Ok(SongMusic.CreateOrUpdateNumberOfPlayed(SongId, UserId));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { error = ex.Message });
            }
        }
        // DELETE api/<WebUsersController>/5
        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete(string UserId, string SongId)
        {
            try
            {
                return Ok(SongMusic.DeleteFavoriteSong(UserId, SongId));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { error = ex.Message });
            }
        }
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(string SongId)
        {
            try
            {
                return Ok(SongMusic.Delete(SongId));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { error = ex.Message });
            }
        }
    }
}
