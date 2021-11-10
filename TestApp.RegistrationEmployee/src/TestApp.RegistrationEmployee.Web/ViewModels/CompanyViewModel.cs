using System.ComponentModel.DataAnnotations;
using TestApp.RegistrationEmployee.Core.Entities;

namespace TestApp.RegistrationEmployee.Web.ViewModels
{
    public class CompanyViewModel
    {

        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public string OrganizationalAndLegalForm { get; set; }

        public static CompanyViewModel FromCompany(Company company)
        {
            return new CompanyViewModel
            {
                Name = company.Name,
                OrganizationalAndLegalForm = company.OrganizationalAndLegalForm
            };
        }

    }
}
