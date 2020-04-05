using AutoMapper;
using BlazorCrud.Shared.Data;
using BlazorCrud.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace BlazorCrud.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UploadController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly UploadContext _context;
        private readonly IMapper _mapper;

        public UploadController(IConfiguration config, UploadContext context, IMapper mapper)
        {
            _config = config;
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns a list of paginated uploads with a default page size of 10.
        /// </summary>
        [HttpGet]
        public PagedResult<Upload> GetAll([FromQuery]int page)
        {
            int pageSize = 10;
            // Do not send file content for all files during search
            foreach (Upload u in _context.Uploads)
            {
                u.FileContent = string.Empty;
            }
            return _context.Uploads
                .OrderBy(p => p.Id)
                .AsNoTracking()
                .GetPaged(page, pageSize);
        }

        /// <summary>
        /// Gets a specific upload by Id.
        /// </summary>
        [HttpGet("{id}", Name = "GetUpload")]
        public ActionResult<Upload> GetById(int id)
        {
            var item = _context.Uploads.Find(id);
            // Do not send password over webAPI GET
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        /// <summary>
        /// Creates a file.
        /// </summary>
        [HttpPost]
        [Authorize]
        public IActionResult Create(Upload upload)
        {
            upload.UploadTimestamp = DateTime.Now;
            upload.ProcessedTimestamp = null;
            if (ModelState.IsValid)
            {
                _context.Uploads.Add(upload);
                _context.SaveChanges();
                return CreatedAtRoute("GetUpload", new { id = upload.Id }, upload);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}