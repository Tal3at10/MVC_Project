using Demo.BLL.Dtos;
using Demo.BLL.Dtos.Employees;
using Demo.BLL.Services.Employees;
using Demo.DAL.Entities.Common.Enums;
using Demo.DAL.Entities.Employees;
using Demo.PL.Controllers.Employees;
using Demo.PL.ViewModels.Employees;

using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers.Employees
{
    public class EmployeeController : Controller
    {
        #region Services
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger, IWebHostEnvironment env)
        {
            _employeeService = employeeService;
            _logger = logger;
            _env = env;
        }

        #endregion

        #region Index
        [HttpGet]
        public IActionResult Index()
        {
            var employees = _employeeService.GetAllEmployees();
            return View(employees);
        }
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            // SEnd Departments from action to view
            ViewData["Employees"] = _employeeService.GetAllEmployees();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeViewModel employeeViewModel)
        {
            if (!ModelState.IsValid)
                return View(employeeViewModel);
            var message = string.Empty;

            try
            {
                var result = _employeeService.CreateEmployee(new EmployeeToCreateDto()
                {
                    Name = employeeViewModel.Name,
                    PhoneNumber = employeeViewModel.PhoneNumber,
                    Address = employeeViewModel.Address,
                    Age = employeeViewModel.Age,
                    Email = employeeViewModel.Email,
                    Salary = employeeViewModel.Salary,
                    IsActive = employeeViewModel.IsActive,
                    HiringDate = employeeViewModel.HiringDate,
                    EmployeeType = employeeViewModel.EmployeeType,
                    Gander = employeeViewModel.Gander,
                    DepartmentId = employeeViewModel.DepartmentId,


                });

                if (result > 0)
                {
                    TempData["Message"] = "New Employee Created Successfully";
                }
                else
                {
                    message = "Employee Cannot be Created";
                    TempData["Message"] = message;
                    ModelState.AddModelError(string.Empty, message);
                    return View(employeeViewModel);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, message);

                if (_env.IsDevelopment())
                {
                    message = ex.Message;
                    return View(employeeViewModel);
                }
                else
                {
                    message = "Employee Cannot be Created";
                    return View("Error", message);
                }
            }

        }
        #endregion

        #region Details
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
                return BadRequest(); // erorr 400

            var employee = _employeeService.GetEmployeeById(id.Value);
            if (employee == null)
                return NotFound(); // erorr 404

            return View(employee);
        }
        #endregion

        #region Edit
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
                return BadRequest(); // 400 Bad Request

            var employee = _employeeService.GetEmployeeById(id.Value);
            if (employee == null)
                return NotFound(); // 404 Not Found


            var model = new EmployeeViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Address = employee.Address,
                Email = employee.Email,
                HiringDate = employee.HiringDate,
                Age = employee.Age,
                Gander = employee.Gander.ToString(),
                EmployeeType = employee.EmployeeType.ToString(),
                PhoneNumber = employee.PhoneNumber,
                IsActive = employee.IsActive,
                Salary = employee.Salary
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, EmployeeViewModel employeeVM)
        {
            if (!ModelState.IsValid)
                return View(employeeVM);
            var message = string.Empty;

            try
            {
                var result = _employeeService.UpdateEmployee(new EmployeeToUpdateDto() // maping from  EmployeeToUpdateDto to EmployeeViewModel
                {
                    Id = employeeVM.Id,
                    Name = employeeVM.Name,
                    Address = employeeVM.Address,
                    Email = employeeVM.Email,
                    Age = employeeVM.Age,
                    HiringDate = employeeVM.HiringDate,
                    Gander = employeeVM.Gander.ToString(),
                    EmployeeType = employeeVM.EmployeeType.ToString(),
                    PhoneNumber = employeeVM.PhoneNumber,
                    IsActive = employeeVM.IsActive,
                    Salary = employeeVM.Salary
                });

                if (result > 0)
                {
                    TempData["Message"] = "Employee Updated Successfully";
                }
                else
                {
                    message = "Employee Cannot Be Updated";
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                message = _env.IsDevelopment() ? ex.Message : "Employee Cannot Be Updated";
            }
            return View(employeeVM);
        }
        #endregion

        #region Delete
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null)
                return BadRequest();

            var employee = _employeeService.GetEmployeeById(id.Value);

            if (employee == null) return NotFound();
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var result = _employeeService.DeleteEmployee(id);
            var message = string.Empty;


            try
            {
                if (result)
                {
                    TempData["Message"] = "Employee Deleted Successfully";
                    return RedirectToAction(nameof(Index));

                }

                message = "An Error Happened while Deleting";


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                message = _env.IsDevelopment() ? ex.Message : "An Error Happened while Deleting";
            }
            ModelState.AddModelError(string.Empty, message);
            return View(nameof(Index));
        }
        #endregion
    }

}
