using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        [Authorize]
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

            var viewModel = _mapper.Map<CarViewModel>(car);
            
            if (car == null || viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        // GET: Car/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Car/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //  more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(
            [Bind("VIN,Year,Make,Model,Trim")] CarViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var car = new Car
                {
                    VIN = viewModel.VIN,
                    Year = viewModel.Year,
                    Make = viewModel.Make,
                    Model = viewModel.Model,
                    Trim = viewModel.Trim
                };

                _carService.Add(car);

                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        // GET: Car/Edit/5
        [Authorize]
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
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id, [Bind("VIN,Year,Make,Model,Trim")] CarViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
//                var car = _mapper.Map<Car>(viewModel);
                var car = await _carService.GetCarById(id);

                if (car == null)
                {
                    return NotFound();
                }

                car.VIN = viewModel.VIN;
                car.Year = viewModel.Year;
                car.Make = viewModel.Make;
                car.Model = viewModel.Model;
                car.Trim = viewModel.Trim;
                
                try
                {
                    _carService.Edit(car);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await CarExists(viewModel.Id)))                    
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
        [Authorize]
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
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
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