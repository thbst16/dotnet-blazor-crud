using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace BlazorCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        /// <summary>
        /// Creates a file.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create()
        {
            var tempFileName = Path.GetTempFileName();
            using (var writer = System.IO.File.OpenWrite(tempFileName))
            {
                await Request.Body.CopyToAsync(writer);
            }
            return Ok(Path.GetFileNameWithoutExtension(tempFileName));
        }
    }
}