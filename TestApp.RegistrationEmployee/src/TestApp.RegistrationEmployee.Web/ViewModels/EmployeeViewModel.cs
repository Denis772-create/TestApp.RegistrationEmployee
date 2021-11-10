using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using TestApp.RegistrationEmployee.Core.Entities;

namespace TestApp.RegistrationEmployee.Web.ViewModels
{
    public class EmployeeViewModel
    {
        [Required]
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime EmploymentDate { get; set; }
        [Required]
        public string Position { get; set; }
        public string Company { get; set; }
        public IEnumerable<Employee> Employees { get; set; } = null;
        public SelectList Companies { get; set; } = null;

        public static EmployeeViewModel FromEmployee(Employee employee)
        {
            return new EmployeeViewModel
            {
                FirstName = employee.Name,
                SecondName = employee.SecondName,
                LastName = employee.LastName,
                EmploymentDate = employee.EmploymentDate,
                Position = employee.Position
            };
        }

    }
}
