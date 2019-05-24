using CompanyData;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utility;

namespace EmployeeContactInfo.Models
{
	public class Department
	{
		public long DepartmentId { get; set; }

		public string Name { get; set; }

		[DataType(DataType.Date)]
		public DateTime ModifiedDate { get; set; }

		[DataType(DataType.Date)]
		public DateTime CreatedDate { get; set; }

		public StatusEnum Status { get; set; }
	}
}