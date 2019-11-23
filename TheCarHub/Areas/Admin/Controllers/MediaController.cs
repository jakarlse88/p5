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

        // GET: Media
        public async Task<IActionResult> Index()
        {
            var media = await _mediaService.GetAllMedia();

            var viewModels = new List<MediaViewModel>();

            foreach (var item in media)
            {
                viewModels.Add(_mapper.Map<MediaViewModel>(item));
            }

            return View(viewModels);
        }

        // GET: Media/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var media =
                await _mediaService.GetMediaById(id.GetValueOrDefault());

            if (media == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<MediaViewModel>(media);

            return View(viewModel);
        }

        // GET: Media/Create
        public IActionResult Create()
        {
            return View();
        }
        
        // POST: Media/Upload
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest();

            var fileName = await Utilities.FileUtility.UploadImage(
                _webHostEnvironment, _configuration, file);

            if (string.IsNullOrEmpty(fileName))
                return BadRequest();
            
            return Json(fileName);
        }

        // GET: Media/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var media =
                await _mediaService.GetMediaById(id.GetValueOrDefault());

            if (media == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<MediaViewModel>(media);

            return View(viewModel);
        }

        // POST: Media/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id, [Bind("Id,Caption,ListingForeignKey")]
            MediaViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var media = await _mediaService.GetMediaById(id);

                if (media == null)
                {
                    return NotFound();
                }

                media.Caption = viewModel.Caption;
                
                try
                {
                    _mediaService.EditMedia(media);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await MediaExists(media.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        // GET: Media/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var media = 
                await _mediaService.GetMediaById(id.GetValueOrDefault());
            
            if (media == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<MediaViewModel>(media);

            return View(viewModel);
        }

        // POST: Media/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _mediaService.DeleteMedia(id);
            
            return RedirectToAction(nameof(Index));
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