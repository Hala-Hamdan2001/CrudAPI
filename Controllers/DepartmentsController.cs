using CrudApi.Data;
using CrudApi.DTOs;
using CrudApi.DTOs.Department;
using CrudApi.Migrations;
using CrudApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrudApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public DepartmentsController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var departments = context.Departments.Select(
                x=> new CetDepartmentDTO()
                {
                    Id= x.Id,
                    Name = x.Name,
                });
            return Ok(departments);
        }
        [HttpGet("Details")]
        public IActionResult GetById(int id)
        {
            var department = context.Departments.Find(id);
            if (department == null)
            {
                return NotFound("Department not found");
            }
            var response = new CetDepartmentDTO
            {
                Id = department.Id,
                Name = department.Name
            };
            return Ok(response);
        }
        [HttpPost("Create")]
        public IActionResult Create(CreateDepartmentDTO depDto)
        {
            Department department = new Department() {
                Name = depDto.Name,
            };
            context.Departments.Add(department);
            context.SaveChanges();
            return Ok(department);
        }
        [HttpPut("Update")]
        public IActionResult Update(int id, CreateDepartmentDTO depDto)
        {
            var current = context.Departments.Find(id);
            if (current == null)
            {
                return NotFound("Department not found");
            }
            current.Name = depDto.Name;
            context.SaveChanges();
            var response = new CetDepartmentDTO
            {
                Id = current.Id,
                Name = current.Name
            };

            return Ok(response);
        }
        [HttpDelete("Remove")]
        public IActionResult Remove(int id)
        {
            var department = context.Departments.Find(id);
            if (department == null)
            {
                return NotFound("Department not found");
            }
            context.Departments.Remove(department);
            context.SaveChanges();
            return Ok(department);
        }
    }
}
