using Corporation.BLL.Models.Departments;
using Corporation.BLL.Services.Departments;
using Corporation.PL.ViewModels.Departments;
using Microsoft.AspNetCore.Mvc;

namespace Corporation.PL.Controllers
{
    public class DepartmentController(
        IDepartmentService departmentService,
        ILogger<DepartmentController> logger,
        IWebHostEnvironment environment
        ) : Controller
    {
        private readonly IDepartmentService _departmentService = departmentService;
        private readonly IWebHostEnvironment _environment = environment;
        private readonly ILogger<DepartmentController> _logger = logger;

        [HttpGet]
        public IActionResult Index()
        {
            var departments = _departmentService.GetAllDepartments();
            return View(departments);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreatedDepartmentDto department)
        {
            if (!ModelState.IsValid)
                return View(department);

            var message = string.Empty;
            try
            {
                var result = _departmentService.CreateDepartment(department);

                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    message = "Department is not created";
                    ModelState.AddModelError(string.Empty, message);
                    return View(department);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                message = _environment.IsDevelopment() ? ex.Message : "An error has occured during creating the department";
            }
            ModelState.AddModelError(string.Empty, message);
            return View(department);
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id is null)
                return BadRequest(); // 400

            var department = _departmentService.GetDepartmentById(id.Value);
            if (department is null)
                return NotFound();  // 404

            return View(department);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null)
                return BadRequest();

            var department = _departmentService.GetDepartmentById(id.Value);
            if (department is null)
                return NotFound();

            return View(new DepartmentEditViewModel()
            {
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                CreationDate = department.CreationDate,
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, DepartmentEditViewModel department)
        {
            if (!ModelState.IsValid)
                return View(department);

            var message = string.Empty;

            try
            {
                var updated = new UpdatedDepartmentDto()
                {
                    Id = id,
                    Code = department.Code,
                    Name = department.Name,
                    Description = department.Description,
                    CreationDate = department.CreationDate,
                };

                var result = _departmentService.UpdateDepartment(updated) > 0;

                if (result)
                    return RedirectToAction(nameof(Index));

                message = "An error has occured during update the department";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                message = _environment.IsDevelopment() ? ex.Message : "An error has occured during updating the department";
            }
            ModelState.AddModelError(string.Empty, message);
            return View(department);
        }

        [HttpGet] // Create view delete [HttpGet] when you decide to delete the department you will go to view and submit delete department.
        public IActionResult Delete(int? id)
        {
            if (id is null)
                return BadRequest();

            var department = _departmentService.GetDepartmentById(id.Value);

            if (department is null)
                return NotFound();

            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var message = string.Empty;

            try
            {
                var deleted = _departmentService.DeleteDepartment(id);
                if (deleted)
                    return RedirectToAction(nameof(Index));

                message = "An error has occured during deleting the department";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                message = _environment.IsDevelopment() ? ex.Message : "An error has occured during deleting the department";

            }
            return RedirectToAction(nameof(Index));
        }
    }
}
