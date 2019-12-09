using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TheCarHub.Services;

namespace TheCarHub.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class MediaController : Controller
    {
        private readonly IMediaService _mediaService;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MediaController(
            IMediaService mediaService,
            IWebHostEnvironment hostEnvironment,
            IConfiguration configuration)
        {
            _mediaService = mediaService;
            _webHostEnvironment = hostEnvironment;
            _configuration = configuration;
        }

        // POST: Media/Upload
        [HttpPost]
        public async Task<IActionResult> Upload(IList<IFormFile> files)
        {
            if (files == null || files.Count == 0)
                return BadRequest();

            var fileNames = new List<string>();

            foreach (var item in files)
            {
                var fileName =
                    await Utilities.FileUtility.UploadImageToDiskAsync(
                        _webHostEnvironment,
                        _configuration, item);

                if (string.IsNullOrEmpty(fileName))
                    return BadRequest();

                fileNames.Add(fileName);
            }

            return Json(fileNames);
        }

        // POST: Media/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([FromBody] string fileName)
        {
            var media = 
                await _mediaService.GetMediaByFileNameAsync(fileName);

            if (media == null) return NotFound();
            
            try
            {
                Utilities.FileUtility.DeleteImageFromDisk(
                    _webHostEnvironment,
                    _configuration,
                    media.FileName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            _mediaService.DeleteMedia(media);

            return Ok();
        }

        private async Task<bool> MediaExists(int id)
        {
            var media = await _mediaService.GetAllMedia();

            return media
                .ToList()
                .Any(e => e.Id == id);
        }
    }
}