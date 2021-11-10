using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TestApp.RegistrationEmployee.Core.Interfaces;

namespace TestApp.RegistrationEmployee.Core.Entities
{
    public class Company : BaseEntity<int>, IAggregateRoot
    {
        public Company()
        {
            Employees = new List<Employee>();
        }

        [Required]
        public string OrganizationalAndLegalForm { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
