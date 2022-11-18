using Cars.Data;
using Cars.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Cars.Controllers
{
    public class CarController : Controller
    {
        private readonly CarContext _context;

        public CarController(CarContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Cars);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Car car)
        {
            if (ModelState.IsValid)
            {
                car.Id = _context.Cars.Max(x => x.Id) + 1;
                _context.Cars.Add(car);
                return RedirectToAction("Index");
            }

            return View(car);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var car = _context.Cars.FirstOrDefault(x => x.Id == id);

            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        [HttpPost]
        public IActionResult Edit(Car car)
        {
            if (ModelState.IsValid)
            {
                var index = _context.Cars.FindIndex(x => x.Id == car.Id);
                _context.Cars[index] = car;
                return RedirectToAction("Index");
            }

            return View(car);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var car = _context.Cars.FirstOrDefault(x => x.Id == id);

            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCar(int? id)
        {
            var car = _context.Cars.FirstOrDefault(x => x.Id == id);
            if (car == null)
            {
                return NotFound();
            }
            _context.Cars.Remove(car);
            return RedirectToAction("Index");
        }
    }
}
