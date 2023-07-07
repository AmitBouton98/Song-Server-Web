﻿using Microsoft.AspNetCore.Mvc;
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
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
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
        // POST api/<SongMusicsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
    }
}
