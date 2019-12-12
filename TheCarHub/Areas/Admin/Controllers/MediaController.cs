using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheCarHub.Services;

namespace TheCarHub.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class MediaController : Controller
    {
        private readonly IMediaService _mediaService;

        public MediaController(
            IMediaService mediaService)
        {
            _mediaService = mediaService;
        }

        // POST: Media/Upload
        [HttpPost]
        public async Task<IActionResult> Upload(IList<IFormFile> files)
        {
            if (files == null || !files.Any())
            {
                return BadRequest();
            }
            
            var fileNames = await _mediaService.UploadFiles(files);

            if (fileNames == null || !fileNames.Any())
            {
                return BadRequest();
            }
            else
            {
                return Json(fileNames);                
            }
        }

        // POST: Media/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromBody] string fileName)
        {
            if (fileName == null)
            {
                return BadRequest();
            }
            
            var media = 
                await _mediaService.GetMediaByFileNameAsync(fileName);

            if (media == null)
            {
                return NotFound();
            }
            
            var removalSuccess = _mediaService.RemoveMediaObject(media);

            if (removalSuccess)
            {
                return Ok();
            }
            else
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        
    }
}