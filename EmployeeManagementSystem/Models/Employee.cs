using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagementSystem.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(20, ErrorMessage = "Name cannot be longer than 20 characters.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email.")]
        [StringLength(100, ErrorMessage = "Email cannot be longer than 100 characters.")]
        [UniqueEmail]
        public string Email { get; set; }
        public string? PicturePath { get; set; }
        [NotMapped]
        [PictureValidation(5 * 1024 * 1024)]
        public IFormFile? Picture { get; set; }
    }
}
