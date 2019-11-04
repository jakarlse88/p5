using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheCarHub.Data;
using TheCarHub.Models.Entities;
using TheCarHub.Models.ViewModels;
using TheCarHub.Services;

namespace TheCarHub.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService _carService;
        private readonly IMapper _mapper;

        public CarController(ICarService carService, IMapper mapper)
        {
            _carService = carService;
            _mapper = mapper;
        }

        // GET: Car
        public async Task<IActionResult> Index()
        {
            var cars = await _carService.GetAllCars();

            var viewModels = new List<CarViewModel>();

            foreach (var car in cars)
            {
                viewModels.Add(_mapper.Map<CarViewModel>(car));
            }

            return View(viewModels);
        }

        // GET: Car/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _carService.GetCarById(id.GetValueOrDefault());

            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: Car/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Car/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //  more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(
            [Bind("VIN,Year,Make,Model,Trim")] CarViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var car = _mapper.Map<Car>(viewModel);
                _carService.Add(car);

                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        // GET: Car/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car =
                await _carService.GetCarById(id.GetValueOrDefault());

            if (car == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<CarViewModel>(car);

            return View(viewModel);
        }


        // POST: Car/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id, [Bind("Id,VIN,Year,Make,Model,Trim")] CarViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var car = _mapper.Map<Car>(viewModel);

                try
                {
                    _carService.Edit(car);
                }
                catch (DbUpdateConcurrencyException)
                {
                    var carExists = await CarExists(viewModel.Id);

                    if (!carExists)
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


        // GET: Car/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _carService.GetCarById(id.GetValueOrDefault());

            if (car == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<CarViewModel>(car);

            return View(viewModel);
        }

        // POST: Car/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await _carService.GetCarById(id);

            _carService.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CarExists(int id)
        {
            var cars = await _carService.GetAllCars();

            return cars
                .ToList()
                .Any(e => e.Id == id);
        }
    }
}