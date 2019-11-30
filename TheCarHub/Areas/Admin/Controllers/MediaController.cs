using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TheCarHub.Models.ViewModels;
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
        private readonly IMapper _mapper;

        public MediaController(
            IMediaService mediaService,
            IWebHostEnvironment hostEnvironment,
            IConfiguration configuration,
            IMapper mapper)
        {
            _mediaService = mediaService;
            _webHostEnvironment = hostEnvironment;
            _configuration = configuration;
            _mapper = mapper;
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
                var fileName = await Utilities.FileUtility.UploadImage(
                    _webHostEnvironment, _configuration, item);

                if (string.IsNullOrEmpty(fileName))
                    return BadRequest();
                
                fileNames.Add(fileName);
            }
            
            return Ok(fileNames);
        }

//        // POST: Media/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public IActionResult DeleteConfirmed(int id)
//        {
//            _mediaService.DeleteMedia(id);
//            
//            return RedirectToAction(nameof(Index));
//        }
//
//        private async Task<bool> MediaExists(int id)
//        {
//            var media = await _mediaService.GetAllMedia();
//
//            return media
//                .ToList()
//                .Any(e => e.Id == id);
//        }
    }
}