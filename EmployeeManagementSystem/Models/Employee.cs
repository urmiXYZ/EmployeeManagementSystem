using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagementSystem.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string? PicturePath { get; set; }
        [NotMapped]
        public IFormFile? Picture { get; set; }
    }
}
