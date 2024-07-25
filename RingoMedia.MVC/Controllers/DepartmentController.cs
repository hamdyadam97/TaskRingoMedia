using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RingoMedia.Application.IServices;
using RingoMedia.Dtos;
using RingoMedia.Models;

namespace RingoMedia.MVC.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private async Task PopulateDepartmentsDropDownList()
        {
            var departments = await _departmentService.GetAllDepartmentsAsync();
            ViewBag.Departments = new SelectList(departments, "Id", "Name");
        }
        public DepartmentController(IDepartmentService departmentService, IWebHostEnvironment webHostEnvironment)
        {
            _departmentService = departmentService;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Department/Create
        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.GetDepartmentsAsync();
            ViewBag.Departments = departments.ToList();
            return View();
        }

        // POST: Department/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DepartmentDTO departmentDto)
        {
            if (ModelState.IsValid)
            {
                if (departmentDto.LogoFile != null)
                {
                    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(departmentDto.LogoFile.FileName);
                    var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await departmentDto.LogoFile.CopyToAsync(stream);
                    }

                    departmentDto.Logo = "/uploads/" + uniqueFileName;
                }
                await _departmentService.AddDepartmentAsync(departmentDto);
                return RedirectToAction(nameof(Index));
            }

            // Debug validation errors
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }

            var departments = await _departmentService.GetDepartmentsAsync();
            ViewBag.Departments = departments.ToList();

            return View(departmentDto);
        }

        // GET: Department/Index
        public async Task<IActionResult> Index()
        {
            var departments = await _departmentService.GetDepartmentsAsync();
            return View(departments);
        }

        public async Task<IActionResult> Details(int id)
        {
            var department = await _departmentService.GetDepartmentWithSubDepartmentsAsync(id);
            var parentDepartments = await _departmentService.GetParentDepartmentsAsync(id);

            var model = new DepartmentDetailsViewModel
            {
                Department = department,
                ParentDepartments = parentDepartments
            };

            return View(model);
        }

        // GET: Department/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var departmentDto = await _departmentService.GetDepartmentByIdAsync(id);
            if (departmentDto == null)
            {
                return NotFound();
            }

            // Prepare ViewBag or ViewData if necessary (e.g., for dropdown lists)
            await PopulateDepartmentsDropDownList();

            return View(departmentDto);
        }


        // POST: Department/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DepartmentDTO departmentDto)
        {
            if (id != departmentDto.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _departmentService.UpdateDepartmentAsync(departmentDto);
                return RedirectToAction(nameof(Index));
            }
            return View(departmentDto);
        }
        // GET: Department/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var department = await _departmentService.GetDepartmentByIdAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        // POST: Department/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _departmentService.DeleteDepartmentAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }


}
