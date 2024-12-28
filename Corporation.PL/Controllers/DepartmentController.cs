using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Corporation.BLL.Models.Departments;
using Corporation.BLL.Services.Departments;
using Corporation.PL.ViewModels.Departments;

namespace Corporation.PL.Controllers
{
    public class DepartmentController(
        IDepartmentService departmentService,
        ILogger<DepartmentController> logger,
        IWebHostEnvironment environment,
        IMapper mapper
        ) : Controller
    {
        #region Service
        private readonly IDepartmentService _departmentService = departmentService;
        private readonly ILogger<DepartmentController> _logger = logger;
        private readonly IWebHostEnvironment _environment = environment;
        private readonly IMapper _mapper = mapper;

        #endregion
  
        #region Index

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var departments = await _departmentService.GetAllDepartmentsAsync();
            return View(departments);
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
        public async Task<IActionResult> Create(CreatedDepartmentDto department)
        {
            if (!ModelState.IsValid)
                return View(department);

            var message = string.Empty;
            try
            {
                var created = await _departmentService.CreateDepartmentAsync(department) > 0;

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

        #endregion
   
        #region Details

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
                return BadRequest(); // 400

            var department = await _departmentService.GetDepartmentByIdAsync(id.Value);
            if (department is null)
                return NotFound();  // 404

            return View(department);
        }

        #endregion

        #region Update
  
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return BadRequest();

            var department = await _departmentService.GetDepartmentByIdAsync(id.Value);
            if (department is null)
                return NotFound();

            var departmentVM = _mapper.Map<DepartmentDetailsDto, DepartmentEditViewModel>(department);

            return View(departmentVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, DepartmentEditViewModel department)
        {
            if (!ModelState.IsValid)
                return View(department);

            var message = string.Empty;

            try
            {
                var updated = _mapper.Map<UpdatedDepartmentDto>(department);

                var result = await _departmentService.UpdateDepartmentAsync(updated) > 0;

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

        #endregion

        #region Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var message = string.Empty;

            try
            {
                var deleted = await _departmentService.DeleteDepartmentAsync(id);
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
        
        #endregion
    }
}
