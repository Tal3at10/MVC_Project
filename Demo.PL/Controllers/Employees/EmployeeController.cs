using Demo.BLL.Dtos;
using Demo.BLL.Dtos.Employees;
using Demo.BLL.Services.Employees;
using Demo.PL.Controllers.Employees;
//using Demo.PL.ViewModels.Employees;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers.Employees
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger, IWebHostEnvironment env)
        {
            _employeeService = employeeService;
            _logger = logger;
            _env = env;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var employees = _employeeService.GetAllEmployees();
            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeToCreateDto employeeToCreateDto)
        {
            if (!ModelState.IsValid)
                return View(employeeToCreateDto);
            var message = string.Empty;

            try
            {
                var result = _employeeService.CreateEmployee(employeeToCreateDto);

                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    message = "Employee Cannot be Created";
                    ModelState.AddModelError(string.Empty, message);
                    return View(employeeToCreateDto);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, message);

                if (_env.IsDevelopment())
                {
                    message = ex.Message;
                    return View(employeeToCreateDto);
                }
                else
                {
                    message = "Employee Cannot be Created";
                    return View("Error", message);
                }
            }

        }

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

        //[HttpGet]
        //public IActionResult Edit(int? id)
        //{
        //    if (id == null)
        //        return BadRequest(); // error 400

        //    var employee = _employeeService.GetEmployeeById(id.Value);
        //    if (employee == null)
        //        return NotFound(); // error 404

        //    return View(new EmployeeEditViewModel
        //    {
        //        Code = employee.Code,
        //        Name = employee.Name,
        //        CreationDate = employee.CreationDate,
        //        Description = employee.Description
        //    });
        //}

        //[HttpPost]
        //public IActionResult Edit(int id, EmployeeEditViewModel employeeVM)
        //{
        //    if (!ModelState.IsValid)
        //        return View(employeeVM);
        //    var message = string.Empty;

        //    try
        //    {
        //        var result = _employeeService.UpdateEmployee(new EmployeeToUpdateDto()
        //        {
        //            Code = employeeVM.Code,
        //            Name = employeeVM.Name,
        //            Description = employeeVM.Description,
        //            CreationDate = employeeVM.CreationDate,
        //        });

        //        if (result > 0)
        //            return RedirectToAction(nameof(Index));
        //        else
        //        {
        //            message = "Employee Cannot Be Updated";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        message = _env.IsDevelopment() ? ex.Message : "Employee Cannot Be Updated";
        //    }
        //    return View(employeeVM);
        //}

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
        public IActionResult Delete(int id)
        {
            var result = _employeeService.DeleteEmployee(id);
            var message = string.Empty;


            try
            {
                if (result)
                    return RedirectToAction(nameof(Index));

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
    }
}
