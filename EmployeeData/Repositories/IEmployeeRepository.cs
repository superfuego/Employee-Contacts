using CompanyData.Models;
using System.Collections.Generic;
using Utility;

namespace CompanyData.Repositories
{
	public interface IEmployeeRepository
	{
		(Error, Employee) AddEmployee(Employee employee);
		(Error, Employee) UpdateEmployee(Employee employee);
		(Error, Employee) GetEmployee(long employeeId);
		(Error, IEnumerable<Employee>) ListEmployees(int offset, int limit, string departmentQuery = null);
		Error RemoveEmployee(long EmployeeId);
	}
}
