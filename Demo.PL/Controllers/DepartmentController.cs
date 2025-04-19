using Demo.BLL.Dtos;
using Demo.BLL.Services;
using Demo.PL.ViewModels.Departments;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IWebHostEnvironment _env;

        public DepartmentController(IDepartmentService departmentService, ILogger<DepartmentController> logger, IWebHostEnvironment env)
        {
            _departmentService = departmentService;
            _logger = logger;
            _env = env;
        }

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
        public IActionResult Create(DepartmentToCreateDto departmentToCreateDto)
        {
            if (!ModelState.IsValid)
                return View(departmentToCreateDto);
            var message = string.Empty;

            try
            {
                var result = _departmentService.CreateDepartment(departmentToCreateDto);

                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    message = "Department Cannot be Created";
                    ModelState.AddModelError(string.Empty, message);
                    return View(departmentToCreateDto);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,message);

                if (_env.IsDevelopment())
                {
                    message = ex.Message;
                    return View(departmentToCreateDto);
                }
                else
                {
                    message = "Department Cannot be Created";
                    return View("Error", message);
                }
            }

        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if(id == null)
                return BadRequest(); // erorr 400

            var department = _departmentService.GetDepartmentById(id.Value);
            if(department == null)
                return NotFound(); // erorr 404

            return View(department);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return BadRequest(); // error 400

            var department = _departmentService.GetDepartmentById(id.Value);
            if (department == null)
                return NotFound(); // error 404

            return View(new DepartmentEditViewModel
            {
                Code = department.Code,
                Name = department.Name,
                CreationDate = department.CreationDate,
                Description = department.Description
            });
        }

    }
}
