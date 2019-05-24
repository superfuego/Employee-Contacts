using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CompanyData;
using CompanyData.Models;
using CompanyData.Repositories;
using Utility;

namespace EmployeeContactInfo.Controllers
{
    public class EmployeesController : Controller
    {
		private readonly IEmployeeRepository _employeeRepository;
		private readonly IDepartmentRepository _departmentRepository;

		public EmployeesController(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository)
		{
			_employeeRepository = employeeRepository;
			_departmentRepository = departmentRepository;
		}

		// GET: Employees
		public IActionResult Index(string searchString)
		{
			var (error, employees) = _employeeRepository.ListEmployees(0, int.MaxValue, searchString?.ToLower());
			if (error != null)
			{
				//All of the error handling here and in the rest of these methods is terrible.
				return StatusCode(500, error.ToString());
			}

			return View(employees);
		}

		// GET: Employees/Details/5
		public IActionResult Details(long? id)
		{
			if (!id.HasValue)
				return NotFound();

			var (error, employee) = _employeeRepository.GetEmployee(id.Value);
			if (error != null)
			{
				return StatusCode(500, error.ToString());
			}

			return View(employee);
		}

		// GET: Employees/Create
		public IActionResult Create()
        {
			ViewData["DepartmentId"] = new SelectList(ListDepartments(), "DepartmentId", "Name");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("EmployeeId,Name,HomePhone,CellPhone,Address,Email,CreatedDate,ModifiedDate,Status,DepartmentId")] Employee employee)
        {
			if (ModelState.IsValid)
			{
				var (error, _) = _employeeRepository.AddEmployee(employee);
				if (error?.Exception != null)
				{
					//Possibly a duplicate?  The error class should be more specific, maybe have an enum indicating different errors, like "not found" and "already exists".
					return StatusCode(500, error.ToString());
				}

				//We're silently ignoring the duplicate here.
				return RedirectToAction(nameof(Index));
			}
			ViewData["DepartmentId"] = new SelectList(ListDepartments(), "DepartmentId", "Name", employee.DepartmentId);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public IActionResult Edit(long? id)
        {
			if (id == null)
			{
				return NotFound();
			}

			var (error, employee) = _employeeRepository.GetEmployee(id.Value);

			if (error?.Exception != null)
			{
				return StatusCode(500, error.ToString());
			}

			if (employee == null)
			{
				return NotFound();
			}
			ViewData["DepartmentId"] = new SelectList(ListDepartments(), "DepartmentId", "Name", employee.DepartmentId);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, [Bind("EmployeeId,Name,HomePhone,CellPhone,Address,Email,CreatedDate,ModifiedDate,Status,DepartmentId")] Employee employee)
        {
			if (id != employee.EmployeeId)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				Error error;
				(error, employee) = _employeeRepository.UpdateEmployee(employee);

				if (error != null)
				{
					if (error.Exception != null)
					{
						return StatusCode(500, error.ToString());
					}
					return NotFound();
				}
				return RedirectToAction(nameof(Index));
			}
			ViewData["DepartmentId"] = new SelectList(ListDepartments(), "DepartmentId", "Name", employee.DepartmentId);
            return View(employee);
        }

		// GET: Employees/Delete/5
		public IActionResult Delete(long? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var (error, employee) = _employeeRepository.GetEmployee(id.Value);

			if (error?.Exception != null)
			{
				return StatusCode(500, error.ToString());
			}

			if (employee == null)
			{
				return NotFound();
			}
			return View(employee);
		}

		// POST: Employees/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(long id)
		{
			var error = _employeeRepository.RemoveEmployee(id);
			if (error != null)
			{
				return StatusCode(500, error.ToString());
			}
			return RedirectToAction(nameof(Index));
		}

		private IEnumerable<Department> ListDepartments()
		{
			var (error, departments) = _departmentRepository.ListDepartments(0, int.MaxValue);
			if (error?.Exception != null)
				throw error.Exception; //While writing this demo project, feedback is better than silent failure
			return departments;
		}
    }
}
