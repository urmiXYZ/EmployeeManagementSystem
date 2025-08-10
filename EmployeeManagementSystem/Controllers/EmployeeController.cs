using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeContext _context;

        public EmployeeController(EmployeeContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var employees = _context.Employees.ToList();
            return View(employees);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (employee.Picture == null)
            {
                ModelState.AddModelError("Picture", "Picture is required.");
            }

            if (ModelState.IsValid)
            {
                string fileName = Path.GetFileName(employee.Picture.FileName);
                string extension = Path.GetExtension(fileName);

                if (extension != ".jpg" && extension != ".png" && extension != ".jpeg")
                {
                    ModelState.AddModelError("Picture", "Only .jpg, .png, and .jpeg files are allowed.");
                    return View(employee);
                }

                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Pictures", employee.Name + extension);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    employee.Picture.CopyTo(stream);
                }

                employee.PicturePath = "/Pictures/" + employee.Name + extension;

                _context.Employees.Add(employee);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(employee);
        }


        public IActionResult Edit(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee == null) return NotFound();

            return View(employee);
        }

        [HttpPost]
        public IActionResult Edit(int id, Employee updatedEmployee)
        {
            if (ModelState.IsValid)
            {
                var employee = _context.Employees.FirstOrDefault(e => e.Id == id);
                if (employee == null) return NotFound();

                employee.Name = updatedEmployee.Name;
                employee.Email = updatedEmployee.Email;

                if (updatedEmployee.Picture != null)
                {
                    string fileName = Path.GetFileName(updatedEmployee.Picture.FileName);
                    string extension = Path.GetExtension(fileName);

                    if (extension != ".jpg" && extension != ".png" && extension != ".jpeg")
                    {
                        ModelState.AddModelError("Picture", "Only .jpg, .png, and .jpeg files are allowed.");
                        return View(updatedEmployee);
                    }

                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Pictures", updatedEmployee.Name + extension);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        updatedEmployee.Picture.CopyTo(stream);
                    }

                    employee.PicturePath = "/Pictures/" + updatedEmployee.Name + extension;
                }

                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(updatedEmployee);
        }



        public IActionResult Delete(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee == null) return NotFound();

            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee == null) return NotFound();

            if (!string.IsNullOrEmpty(employee.PicturePath))
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", employee.PicturePath.TrimStart('/'));
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            _context.Employees.Remove(employee);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
