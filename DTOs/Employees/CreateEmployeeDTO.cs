namespace CrudApi.DTOs.Employees
{
    public class CreateEmployeeDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int DepartmentId { get; set; }

    }
}
