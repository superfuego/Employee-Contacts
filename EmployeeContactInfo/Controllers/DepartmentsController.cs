using Microsoft.AspNetCore.Mvc;
using CompanyData.Models;
using CompanyData.Repositories;
using Utility;

namespace EmployeeContactInfo.Controllers
{
    public class DepartmentsController : Controller
    {
		private IDepartmentRepository _departmentRepository;

        public DepartmentsController(IDepartmentRepository departmentRepository)
        {
			_departmentRepository = departmentRepository;
        }

        // GET: Departments
        public IActionResult Index()
        {
			var (error, departments) = _departmentRepository.ListDepartments(0, 10);
			if (error != null)
			{
				//All of the error handling here and in the rest of these methods is terrible.  
				return StatusCode(500, error.ToString());
			}

            return View(departments);
        }

        // GET: Departments/Details/5
        public IActionResult Details(long? id)
        {
			if (!id.HasValue)
				return NotFound();

			var (error, department) = _departmentRepository.GetDepartment(id.Value);
			if (error != null)
			{
				return StatusCode(500, error.ToString());
			}

			return View(department);
		}

        // GET: Departments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("DepartmentId,Name,ModifiedDate,CreatedDate,Status")] Department department)
        {
            if (ModelState.IsValid)
            {
				var (error, _) = _departmentRepository.AddDepartment(department);
				if (error != null)
				{
					return StatusCode(500, error.ToString());
				}

				return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // GET: Departments/Edit/5
        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var (error, department) = _departmentRepository.GetDepartment(id.Value);

			if (error?.Exception != null)
			{
				return StatusCode(500, error.ToString());
			}

			if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, [Bind("DepartmentId,Name,ModifiedDate,CreatedDate,Status")] Department department)
        {
            if (id != department.DepartmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
				Error error;
				(error, department) = _departmentRepository.RenameDepartment(id, department.Name);

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
            return View(department);
        }

        // GET: Departments/Delete/5
        public IActionResult Delete(long? id)
        {
			if (id == null)
			{
				return NotFound();
			}

			var (error, department) = _departmentRepository.GetDepartment(id.Value);

			if (error?.Exception != null)
			{
				return StatusCode(500, error.ToString());
			}

			if (department == null)
			{
				return NotFound();
			}
			return View(department);
		}

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(long id)
        {
			var error = _departmentRepository.RemoveDepartment(id);
			if (error != null)
			{
				return StatusCode(500, error.ToString());
			}
            return RedirectToAction(nameof(Index));
        }
    }
}
