using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TheCarHub.Data;
using TheCarHub.Models;
using TheCarHub.Models.Entities;
using TheCarHub.Models.ViewModels;
using TheCarHub.Services;

namespace TheCarHub.Controllers
{
    [Authorize]
    public class MediaController : Controller
    {
        private readonly IMediaService _mediaService;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public MediaController(
            IMediaService mediaService,
            IWebHostEnvironment hostEnvironment,
            IConfiguration config,
            IMapper mapper)
        {
            _mediaService = mediaService;
            _hostEnvironment = hostEnvironment;
            _config = config;
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

        // POST: Media/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,FileName,Caption")] MediaViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Image upload
                if (viewModel.FormFiles != null && viewModel.FormFiles.Count > 0)
                {
                    foreach (var file in viewModel.FormFiles)
                    {
                        if (file.Length > 0)
                        {
                            var fileName =
                                Path.GetRandomFileName() + Path.GetExtension(file.FileName);

                            var path =
                                Path.Combine(_hostEnvironment.WebRootPath,
                                    _config["Media:Directory"], $"{fileName}");

//                            if (listing.Media == null)
//                            {
//                                listing.Media = new List<Media>();
//                            }
//
//                            listing.Media.Add(new Media
//                            {
//                                FileName = fileName,
//                                ListingId = listing.Id,
//                                Listing = listing,
//                                Caption = "",
//                                MediaTags = new List<MediaTag>()
//                            });

                            _mediaService.AddMedia(new Media
                            {
                                FileName = fileName,
                                Caption = viewModel.Caption
                            });

                            using (var stream = System.IO.File.Create(path))
                            {
                                await file.CopyToAsync(stream);
                            }
                        }
                    }
                }
            }

            return View(viewModel);
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
        public async Task<IActionResult> DeleteConfirmed(int id)
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