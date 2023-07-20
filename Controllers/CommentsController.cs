using Microsoft.AspNetCore.Mvc;
using Server.Moodle;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        [HttpGet]
        [Route("GetSongComments/songId/{songId}")]
        public  IActionResult GetSongComments(string songId) 
        {
            try
            {
                return Ok(SongComment.GetCommentBySongID(songId));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { error = ex.Message });
            }

        }
        [HttpGet]
        [Route("GetArtistComments/artistName/{artistName}")]
        public IActionResult GetArtistComments(string artistName) 
        {
            try
            {
                return Ok(ArtistComment.GetCommentByArtistName(artistName));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { error = ex.Message });
            }

        }
        [HttpDelete]
        [Route("DeleteArtistComment/commentId/{commentId}")]
        public IActionResult DeleteArtistComment(string commentId)
        {
            try
            {
                return Ok(ArtistComment.Delete(commentId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // DELETE api/<WebUsersController>/5
        [HttpDelete]
        [Route("DeleteSongComment/commentId/{commentId}")]
        public IActionResult DeleteSongComment(string commentId)
        {
            try
            {
                return Ok(SongComment.Delete(commentId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("PostSongComment")]
        public IActionResult PostSongComment([FromBody] SongComment sc)
        {
            try
            {
                bool check = SongComment.InsertSongCommentOrUpdate(sc);
                return check ? Ok("Song comment added") : Ok("Song comment Updated");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { error = ex.Message });

            }
        }
        [HttpPost]
        [Route("PostArtistComment")]
        public IActionResult PostArtistComment([FromBody] ArtistComment ac)
        {
            try
            {
                bool check = ArtistComment.InsertArtistCommentOrUpdate(ac);
                return check ? Ok("Artist comment added") : Ok("Artist comment Updated");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { error = ex.Message });

            }
        }
    }
}
