using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using TestApp.RegistrationEmployee.Core.Entities;
using TestApp.RegistrationEmployee.Core.Interfaces;
using TestApp.RegistrationEmployee.Web.Filters;
using TestApp.RegistrationEmployee.Web.ViewModels;

namespace TestApp.RegistrationEmployee.Web.Controllers
{
    public class CompanyController : Controller
    {
        private readonly IRepository<Company> _repository;
        public CompanyController(IRepository<Company> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var company = await _repository.ListAsync();
            var companyViewModel = company
                .Select(s => new CompanyViewModel
                {
                    Name = s.Name,
                    OrganizationalAndLegalForm = s.OrganizationalAndLegalForm
                });

            return View(companyViewModel);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create(CompanyViewModel model)
        {
            Company company = new Company
            {
                Name = model.Name,
                OrganizationalAndLegalForm = model.OrganizationalAndLegalForm
            };

            try
            {
                await _repository.AddAsync(company);
                await _repository.SaveChangesAsync();
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
            }

            return RedirectToAction("List");
        }


        public async Task<IActionResult> Edit(string id)
        {
            var company = await _repository.GetByNameAsync(id);
            if (company == null)
                return NotFound();

            return View(CompanyViewModel.FromCompany(company));
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Edit(CompanyViewModel model)
        {
            var company = await _repository.GetByNameAsync(model.Name);
            if (company == null)
                return NoContent();

            company.Name = model.Name;
            company.OrganizationalAndLegalForm = model.OrganizationalAndLegalForm;

            await _repository.UpdateAsync(company);
            await _repository.SaveChangesAsync();

            return RedirectToAction("List");
        }

        public async Task<ActionResult> Delete(string id)
        {
            var company = await _repository.GetByNameAsync(id);

            if (company != null)
                await _repository.DeleteAsync(company);

            return RedirectToAction("List");
        }

    }
}
