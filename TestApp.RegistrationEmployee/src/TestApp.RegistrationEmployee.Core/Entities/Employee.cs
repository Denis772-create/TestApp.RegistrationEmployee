using System;
using System.ComponentModel.DataAnnotations;
using TestApp.RegistrationEmployee.Core.Interfaces;

namespace TestApp.RegistrationEmployee.Core.Entities
{
    public class Employee : BaseEntity<int>, IAggregateRoot
    {
        public string SecondName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime EmploymentDate { get; set; }
        [Required]
        public string Position { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
