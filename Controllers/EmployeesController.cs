using CrudApi.Data;
using CrudApi.DTOs.Employees;
using CrudApi.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public EmployeesController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var employees = context.Employees.ToList();
            var response = employees.Adapt<IEnumerable<GetEmployeeDTO>>();
            return Ok(response);
        }
        [HttpGet("Details")]
        public IActionResult GetById(int id)
        {
            var employee = context.Employees.Find(id);
            if (employee == null)
            {
                return NotFound("Employee not found");
            }
            var response = employee.Adapt<GetEmployeeDTO>();
            return Ok(response);
        }
        [HttpPost("Create")]
        public IActionResult Create(CreateEmployeeDTO empDto)
        {
            var employee = empDto.Adapt<Employee>();
            context.Employees.Add(employee);
            context.SaveChanges();
            return Ok();
        }
        [HttpPut("Update")]
        public IActionResult Update(int id, CreateEmployeeDTO empDto)
        {
            var current = context.Employees.Find(id);
            if (current == null)
            {
                return NotFound("Employee not found");
            }
            empDto.Adapt(current); 
            context.SaveChanges();
            return Ok(current.Adapt<GetEmployeeDTO>());
        }
        [HttpDelete("Remove")]
        public IActionResult Remove(int id)
        {
            var employee = context.Employees.Find(id);
            if (employee == null)
            {
                return NotFound("Employee not found");
            }
            context.Employees.Remove(employee);
            context.SaveChanges();
            return Ok(employee);
        }
    }
}
