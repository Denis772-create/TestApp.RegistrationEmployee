using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.RegistrationEmployee.Core.Entities
{
    public abstract class BaseEntity<TId>
    {
        [Required, Key]
        public TId Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
