using AutoMapper;
using Corporation.BLL.Models.Departments;
using Corporation.BLL.Services.Departments;
using Corporation.PL.ViewModels.Departments;
using Microsoft.AspNetCore.Mvc;

namespace Corporation.PL.Controllers
{
    public class DepartmentController(
        IDepartmentService departmentService,
        ILogger<DepartmentController> logger,
        IWebHostEnvironment environment,
        IMapper mapper
        ) : Controller
    {
        private readonly IDepartmentService _departmentService = departmentService;
        private readonly ILogger<DepartmentController> _logger = logger;
        private readonly IWebHostEnvironment _environment = environment;
        private readonly IMapper _mapper = mapper;

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
                var created = _departmentService.CreateDepartment(department) > 0;

                var createdDept = _mapper.Map<CreatedDepartmentDto>(department);

                if (created)
                    TempData["Message"] = "Department is created";
                else
                    TempData["Message"] = "Department is not created";
                return RedirectToAction(nameof(Index));
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

            var departmentVM = _mapper.Map<DepartmentDetailsDto, DepartmentEditViewModel>(department);

            return View(departmentVM);
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
                var updated = _mapper.Map<UpdatedDepartmentDto>(department);

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
