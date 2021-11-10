#nullable enable
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using TestApp.RegistrationEmployee.Core.Entities;
using TestApp.RegistrationEmployee.Core.Interfaces;
using TestApp.RegistrationEmployee.Web.Filters;
using TestApp.RegistrationEmployee.Web.ViewModels;

namespace TestApp.RegistrationEmployee.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IRepository<Employee> _repository;
        private readonly IRepository<Company> _repositoryComp;
        public EmployeeController(
            IRepository<Employee> repository,
            IRepository<Company> repositoryComp)
        {
            _repository = repository;
            _repositoryComp = repositoryComp;
        }

        [HttpGet]
        public async Task<IActionResult> List(string company, string name)
        {
            ViewBag.Companies = new SelectList(
               await _repositoryComp.ListAsync(),
                "Name",
                "Name");

            var employees = await _repository.ListAsync();

            var employeesViewModel = employees
                .Select(s => new EmployeeViewModel
                {
                    FirstName = s.Name,
                    SecondName = s.SecondName,
                    LastName = s.LastName,
                    EmploymentDate = s.EmploymentDate,
                    Position = s.Position,
                    Company = _repositoryComp.GetByIdAsync(s.CompanyId).Result.Name
                });

            if (company != null && company != "All")
                employeesViewModel = employeesViewModel.Where(p => p.Company == company);

            if (!String.IsNullOrEmpty(name))
                employeesViewModel = employeesViewModel.Where(p => p.FirstName.Contains(name));

            return View(employeesViewModel);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create(EmployeeViewModel model)
        {
            Employee employee = new Employee
            {
                Name = model.FirstName,
                SecondName = model.SecondName,
                LastName = model.LastName,
                EmploymentDate = model.EmploymentDate,
                Position = model.Position,
                Company = await _repositoryComp.GetByNameAsync(model.Company)
            };

            try
            {
                await _repository.AddAsync(employee);
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
            var employee = await _repository.GetByNameAsync(id);
            if (employee == null)
                return NotFound();

            return View(EmployeeViewModel.FromEmployee(employee));
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Edit(EmployeeViewModel model)
        {
            var employee = await _repository.GetByNameAsync(model.FirstName);
            if (employee == null)
                return NoContent();

            employee.Name = model.FirstName;
            employee.SecondName = model.SecondName;
            employee.LastName = model.LastName;
            employee.EmploymentDate = model.EmploymentDate;
            employee.Position = model.Position;
            employee.Company = await _repositoryComp.GetByNameAsync(model.Company);

            await _repository.UpdateAsync(employee);
            await _repository.SaveChangesAsync();

            return RedirectToAction("List");
        }

        public async Task<ActionResult> Delete(string id)
        {
            var employee = await _repository.GetByNameAsync(id);

            if (employee != null)
                await _repository.DeleteAsync(employee);

            return RedirectToAction("List");
        }

    }
}
