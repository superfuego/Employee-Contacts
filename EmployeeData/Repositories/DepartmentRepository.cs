using CompanyData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;

namespace CompanyData.Repositories
{
	public class DepartmentRepository: RepositoryBase, IDepartmentRepository
	{
		public (Error, Department) AddDepartment(Department department)
		{
			try
			{
				Context.Departments.Add(department);
				Context.SaveChanges();
				return (null, department);
			}
			catch(Exception ex) //This is obviously bad, but makes sense for a coding demo
			{
				return (new Error("Failed to save Department", ex), null);
			}
		}

		public Error RemoveDepartment(long departmentId)
		{
			try
			{
				var department = Context.Departments.FirstOrDefault(d => d.DepartmentId == departmentId);

				if (department == null)
					return new Error("The specified department does not exist");

				department.Status = StatusEnum.Deleted;
				Context.SaveChanges();

				return null;
			}
			catch (Exception ex)
			{
				return new Error(ex);
			}
		}

		public (Error, Department) RenameDepartment(long departmentId, string name)
		{
			try
			{
				var department = Context.Departments.FirstOrDefault(d => d.DepartmentId == departmentId);

				if (department == null)
					return (new Error("The specified department does not exist"), null);

				department.Name = name;
				Context.SaveChanges();

				return (null, department);
			}
			catch (Exception ex)
			{
				return (new Error(ex), null);
			}
		}

		public (Error, Department) GetDepartment(long departmentId)
		{
			try
			{
				return (null, Context.Departments.First(d => d.DepartmentId == departmentId));
			}
			catch (Exception ex)
			{
				return (new Error(ex), null);
			}
		}

		public (Error, IEnumerable<Department>) ListDepartments(int skip, int take)
		{
			if (skip < 0)
				return (new Error(new ArgumentOutOfRangeException("Skip can't be negative")), null);

			if (take < 0)
				return (new Error(new ArgumentOutOfRangeException("Take can't be negative")), null);

			try
			{
				return (null, Context.Departments.Where(d => d.Status == StatusEnum.Active).Skip(skip).Take(take));
			}
			catch (Exception ex)
			{
				return (new Error(ex), null);
			}
		}
	}
}
