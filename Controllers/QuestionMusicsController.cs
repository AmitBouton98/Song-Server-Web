using Microsoft.AspNetCore.Mvc;
using Server.Moodle;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionMusicsController : ControllerBase
    {
        [HttpGet]
        [Route("CreateQustion1WhoCreatedTheSong")]
        public IActionResult CreateQustion1WhoCreatedTheSong()
        {
            try
            {
                return Ok(QuestionMusic.CreateQustion1WhoCreatedTheSong());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { error = ex.Message });
            }
        }
        [HttpGet]
        [Route("CreateQustion2WhatSongBelongToArtist")]
        public IActionResult CreateQustion2WhatSongBelongToArtist()
        {
            try
            {
                return Ok(QuestionMusic.CreateQustion2WhatSongBelongToArtist());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { error = ex.Message });
            }
        }
        [HttpGet]
        [Route("CreateQustion3WhatPicBelongToArtist")]
        public IActionResult CreateQustion3WhatPicBelongToArtist()
        {
            try
            {
                return Ok(QuestionMusic.CreateQustion3WhatPicBelongToArtist());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { error = ex.Message });
            }
        }
        [HttpGet]
        [Route("CreateQustion4WhatPicBelongToSong")]
        public IActionResult CreateQustion4WhatPicBelongToSong()
        {
            try
            {
                return Ok(QuestionMusic.CreateQustion4WhatPicBelongToSong());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { error = ex.Message });
            }
        }
        [HttpGet]
        [Route("CreateQustion5WhatIsTheDurationForSong")]
        public IActionResult CreateQustion5WhatIsTheDurationForSong()
        {
            try
            {
                return Ok(QuestionMusic.CreateQustion5WhatIsTheDurationForSong());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { error = ex.Message });
            }
        }
        [HttpGet]
        [Route("CreateQustion6whichSongBeginWith")]
        public IActionResult CreateQustion6whichSongBeginWith()
        {
            try
            {
                return Ok(QuestionMusic.CreateQustion6whichSongBeginWith());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { error = ex.Message });
            }
        }
        // GET api/<QuestionMusicsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<QuestionMusicsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<QuestionMusicsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<QuestionMusicsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
