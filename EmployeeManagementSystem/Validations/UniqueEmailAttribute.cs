using EmployeeManagementSystem.Models;
using System.ComponentModel.DataAnnotations;

public class UniqueEmailAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var context = (EmployeeContext)validationContext.GetService(typeof(EmployeeContext));
        string email = value as string ?? "";

        var instance = validationContext.ObjectInstance;
        var employee = instance as Employee;

        var existingEmployee = context.Employees
                    .FirstOrDefault(e => e.Email == email && e.Id != employee.Id);

        if (existingEmployee != null) 
        {
            return new ValidationResult("This email already exists.");
        }

        return ValidationResult.Success;
    }
}
