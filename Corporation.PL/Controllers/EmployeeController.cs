using Microsoft.AspNetCore.Mvc;
using Corporation.BLL.Models.Employees;
using Corporation.BLL.Services.Departments;
using Corporation.BLL.Services.Employees;
using Corporation.PL.ViewModels.Employees;
using Microsoft.AspNetCore.Authorization;

namespace Corporation.PL.Controllers
{
    [Authorize]
    public class EmployeeController(
        IEmployeeService employeeService,
        ILogger<EmployeeController> logger,
        IWebHostEnvironment environment) : Controller
    {
        #region Service
        
        private readonly IEmployeeService _employeeService = employeeService;
        private readonly ILogger<EmployeeController> _logger = logger;
        private readonly IWebHostEnvironment _environment = environment;

        #endregion
  
        #region Index

        [HttpGet]
        public async Task<IActionResult> Index(string search)
        {
            var employees = await _employeeService.GetAllEmployeesAsync(search);
            return View(employees);
        }

        #endregion

        #region Create

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatedEmployeeDto employee)
        {
            if (!ModelState.IsValid)
                return View();

            var message = string.Empty;
            try
            {
                var created = await _employeeService.CreateEmployeeAsync(employee) > 0;
                if (created)
                    TempData["Message"] = "Employee is created";
                else
                    TempData["Message"] = "Employee is not created";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                message = _environment.IsDevelopment() ? ex.Message : "An error has occured during creating the department";

                ModelState.AddModelError(string.Empty, message);
                return View();
            }

        }

        #endregion

        #region Details

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
                return BadRequest();

            var employee = await _employeeService.GetEmployeeByIdAsync(id.Value);
            if (employee is null)
                return NotFound();

            return View(employee);
        }

        #endregion

        #region Update

        [HttpGet]
        public async Task<IActionResult> Edit(int? id, [FromServices] IDepartmentService departmentService)
        {
            if (id is null)
                return BadRequest();

            var employee = await _employeeService.GetEmployeeByIdAsync(id.Value);

            if (employee is null)
                return NotFound();

            //ViewBag.departments = departmentService.GetAllDepartments();
            ViewData["departments"] = departmentService.GetAllDepartmentsAsync();


            return View(new EmployeeEditViewModel()
            {
                Name = employee.Name,
                Address = employee.Address,
                Email = employee.Email,
                Age = employee.Age,
                HiringDate = employee.HiringDate,
                IsActive = employee.IsActive,
                PhoneNumber = employee.PhoneNumber,
                Salary = employee.Salary,
                EmployeeType = employee.EmployeeType,
                Gender = employee.Gender,
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, UpdatedEmployeeDto employee)
        {
            if (!ModelState.IsValid)
                return View();

            var message = string.Empty;

            try
            {
                var updated = await _employeeService.UpdateEmployeeAsync(employee) > 0;
                if (updated)
                    return RedirectToAction(nameof(Index));
                message = "An error has occured during updating employee";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                message = _environment.IsDevelopment() ? ex.Message : "An error has occured during updating employee";
            }

            ModelState.AddModelError(string.Empty, message);
            return View(employee);
        }

        #endregion

        #region Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var message = string.Empty;

            try
            {
                var deleted = await _employeeService.DeleteEmployeeAsync(id);
                if (deleted)
                    return RedirectToAction(nameof(Index));
                message = "An error has occured during deleting employee";

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                message = _environment.IsDevelopment() ? ex.Message : "An error has occured during deleting employee";
            }
            return RedirectToAction(nameof(Index));
        }

        #endregion
    }
}
