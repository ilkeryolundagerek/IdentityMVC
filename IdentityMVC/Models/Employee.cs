namespace IdentityMVC.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Material { get; set; }
    }

    public class EmployeesViewModel
    {
        public IEnumerable<Employee> Employees { get; set; }
    }
}
