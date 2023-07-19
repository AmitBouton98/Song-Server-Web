using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using System.Net;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {

        // GET api/<UploadController>
        [HttpGet]
        public IActionResult Get(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return BadRequest("Please provide a valid file name.");
            }

            string path = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "usersImages");
            var filePath = Path.Combine(path, fileName);

            if (System.IO.File.Exists(filePath))
            {
                // Determine the content type based on the file extension
                string contentType = GetContentType(filePath);

                // Return the image file to the client's browser
                return File(System.IO.File.OpenRead(filePath), contentType);
            }
            else
            {
                // If the file does not exist, return a not found status
                return NotFound();
            }
        }

        private string GetContentType(string filePath)
        {
            // Get the file extension
            string extension = Path.GetExtension(filePath);

            // Set the default content type to "application/octet-stream" (generic binary)
            string contentType = "application/octet-stream";

            // Map commonly used file extensions to their corresponding content types
            if (!string.IsNullOrEmpty(extension))
            {
                switch (extension.ToLower())
                {
                    case ".jpg":
                    case ".jpeg":
                        contentType = "image/jpeg";
                        break;
                    case ".png":
                        contentType = "image/png";
                        break;
                    case ".gif":
                        contentType = "image/gif";
                        break;
                        // Add more cases as needed for other file types
                }
            }

            return contentType;
        }




        // GET api/<UploadController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UploadController>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] IFormFile file)
        {
            List<string> imageLinks = new List<string>();
            string path = System.IO.Directory.GetCurrentDirectory();
            if (file.Length > 0)
            {
                var filePath = Path.Combine(path, "usersImages/" + file.FileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    await file.CopyToAsync(stream);
                }
                imageLinks.Add(file.FileName);
            }
            // Return status code  
            return Ok(imageLinks);

        }

        // PUT api/<UploadController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UploadController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
