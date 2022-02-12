using Blazorcrud.Server.Authorization;
using Blazorcrud.Server.Models;
using Blazorcrud.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blazorcrud.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UploadController : ControllerBase
    {
        private readonly IUploadRepository _uploadRepository;

        public UploadController(IUploadRepository uploadRepository)
        {
            _uploadRepository = uploadRepository;
        }

        /// <summary>
        /// Returns a list of paginated uploads with a default page size of 5.
        /// </summary>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult GetUploads(string? name, int page)
        {
            return Ok(_uploadRepository.GetUploads(name, page));
        }

        /// <summary>
        /// Gets a specific upload by Id.
        /// </summary>
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetUpload(int id)
        {
            return Ok(await _uploadRepository.GetUpload(id));
        }

        /// <summary>
        /// Creates an upload with base64 encoded file
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> AddUpload(Upload upload)
        {
            return Ok(await _uploadRepository.AddUpload(upload));
        }
        
        /// <summary>
        /// Updates an upload with a specific Id.
        /// </summary>
        [HttpPut]
        public async Task<ActionResult> UpdateUpload(Upload upload)
        {
            return Ok(await _uploadRepository.UpdateUpload(upload));
        }

        /// <summary>
        /// Deletes an upload with a specific Id.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUpload(int id)
        {
            return Ok(await _uploadRepository.DeleteUpload(id));
        }
    }
}
