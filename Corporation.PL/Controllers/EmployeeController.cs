﻿using Corporation.BLL.Models.Employees;
using Corporation.BLL.Services.Departments;
using Corporation.BLL.Services.Employees;
using Corporation.PL.ViewModels.Employees;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace Corporation.PL.Controllers
{
    public class EmployeeController(
        IEmployeeService employeeService,
        ILogger<EmployeeController> logger,
        IWebHostEnvironment environment) : Controller
    {
        private readonly IEmployeeService _employeeService = employeeService;
        private readonly ILogger<EmployeeController> _logger = logger;
        private readonly IWebHostEnvironment _environment = environment;

        [HttpGet]
        public IActionResult Index(string search)
        {
            var employees = _employeeService.GetAllEmployees(search);
            return View(employees);
        }

        [HttpGet]
        public IActionResult Create([FromServices] IDepartmentService departmentService)
        {
            ViewData["departments"] = departmentService.GetAllDepartments();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreatedEmployeeDto employee)
        {
            if (!ModelState.IsValid)
                return View();

            var message = string.Empty;
            try
            {
                var created = _employeeService.CreateEmployee(employee) > 0;
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

        public IActionResult Details(int? id)
        {
            if (id is null)
                return BadRequest();

            var employee = _employeeService.GetEmployeeById(id.Value);
            if (employee is null)
                return NotFound();

            return View(employee);
        }

        [HttpGet]
        public IActionResult Edit(int? id, [FromServices] IDepartmentService departmentService)
        {
            if (id is null)
                return BadRequest();

            var employee = _employeeService.GetEmployeeById(id.Value);

            if (employee is null)
                return NotFound();

            //ViewBag.departments = departmentService.GetAllDepartments();
            ViewData["departments"] = departmentService.GetAllDepartments();


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
        public IActionResult Edit([FromRoute] int id, UpdatedEmployeeDto employee)
        {
            if (!ModelState.IsValid)
                return View();

            var message = string.Empty;

            try
            {
                var updated = _employeeService.UpdateEmployee(employee) > 0;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var message = string.Empty;

            try
            {
                var deleted = _employeeService.DeleteEmployee(id);
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
    }
}
