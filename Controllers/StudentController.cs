using Microsoft.AspNetCore.Mvc;
using StudentRegistration.Models;
using StudentRegistration.Services;

namespace StudentRegistration.Controllers
{
    public class StudentController : Controller
    {
        private readonly MongoDBService _service;

        public StudentController(MongoDBService service)
        {
            _service = service;
        }

        // Display all students
        public async Task<IActionResult> Index()
        {
            var students = await _service.GetAllAsync();

            return View(students);
        }

        // Registration page
        public IActionResult Register()
        {
            return View();
        }

        // Save student
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Student student)
        {
            if (!ModelState.IsValid)
                return View(student);

            await _service.CreateAsync(student);

            return RedirectToAction("Index");
        }

        // Edit page
        public async Task<IActionResult> Edit(string id)
        {
            var student = await _service.GetByIdAsync(id);

            if (student == null)
                return NotFound();

            return View(student);
        }

        // Update student
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Student student)
        {
            if (id != student.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(student);

            await _service.UpdateAsync(id, student);

            return RedirectToAction("Index");
        }

        // Delete student
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            await _service.DeleteAsync(id);

            return RedirectToAction("Index");
        }
    }
}