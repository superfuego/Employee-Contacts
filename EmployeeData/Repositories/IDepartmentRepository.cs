using CompanyData.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Utility;

namespace CompanyData.Repositories
{
	public interface IDepartmentRepository
	{
		(Error, Department) AddDepartment(Department department);
		(Error, Department) RenameDepartment(long departmentId, string name);
		(Error, Department) GetDepartment(long DepartmentId);
		(Error, IEnumerable<Department>) ListDepartments(int offset, int limit);
		Error RemoveDepartment(long departmentId);
	}
}
