using CompanyData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Utility;

namespace CompanyData.Repositories
{
	public class EmployeeRepository : RepositoryBase, IEmployeeRepository
	{
		public (Error, Employee) AddEmployee(Employee employee)
		{
			try
			{
				if (Context.Employees.Any(e => e.CellPhone == employee.CellPhone && e.Name == employee.Name && e.Status == StatusEnum.Active))
					return (new Error("This employee already exists.  Please update their information instead"), null);

				Context.Employees.Add(employee);
				Context.SaveChanges();
				return (null, employee);
			}
			catch (Exception ex) //This is obviously bad, but makes sense for a coding demo
			{
				return (new Error("Failed to save Employee", ex), null);
			}
		}

		public Error RemoveEmployee(long employeeId)
		{
			try
			{
				var employee = GetEmployeeImpl(employeeId);
				if (employee == null)
					return new Error("The specified employee does not exist");

				employee.Status = StatusEnum.Deleted;

				Context.SaveChanges();

				return null;
			}
			catch (Exception ex)
			{
				return new Error(ex);
			}
		}
		
		public (Error, Employee) UpdateEmployee(Employee employee)
		{
			try
			{
				if (employee.EmployeeId == 0) //All entities will start at 1, autoincrementing.  
					return (new Error("This employee does not exist.  Please create them instead"), null);
				
				var existing = GetEmployeeImpl(employee.EmployeeId);
				if (existing == null)
				{
					return (new Error("Not Found"), null);
				}

				existing.HomePhone = employee.HomePhone;
				existing.Name = employee.Name;
				existing.CellPhone = employee.CellPhone;
				existing.Email = employee.Email;
				existing.Address = employee.Address;
				existing.DepartmentId = employee.DepartmentId;

				Context.SaveChanges();

				return (null, employee);
			}
			catch (Exception ex)
			{
				return (new Error(ex), null);
			}
		}

		public (Error, Employee) GetEmployee(long employeeId)
		{
			try
			{
				var employee = GetEmployeeImpl(employeeId);
				if (employee == null)
					return (new Error("The specified employee does not exist"), null);

				return (null, employee);

			}
			catch (Exception ex)
			{
				return (new Error(ex), null);
			}
		}

		public (Error, IEnumerable<Employee>) ListEmployees(int skip, int take, string departmentQueryString = null)
		{
			if (skip < 0)
				return (new Error(new ArgumentOutOfRangeException("Skip can't be negative")), null);

			if (take < 0)
				return (new Error(new ArgumentOutOfRangeException("Take can't be negative")), null);

			try
			{
				return (null, Context.Employees.Include(e => e.Department).Where(e => e.Status == StatusEnum.Active 
											&& (string.IsNullOrEmpty(departmentQueryString) || e.Department.Name.ToLower().Contains(departmentQueryString))).Skip(skip).Take(take));
			}
			catch (Exception ex)
			{
				return (new Error(ex), null);
			}
		}

		#region private helpers

		private Employee GetEmployeeImpl(long employeeId)
		{
			return Context.Employees.Include(e => e.Department).FirstOrDefault(e => e.EmployeeId == employeeId && e.Status == StatusEnum.Active);
		}

		#endregion
	}
}
