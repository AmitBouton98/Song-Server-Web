using Microsoft.AspNetCore.Mvc;
using Server.Moodle;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionMusicsController : ControllerBase
    {
        // GET: api/<QuestionMusicsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
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
