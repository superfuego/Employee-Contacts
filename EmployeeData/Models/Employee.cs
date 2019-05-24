using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utility;

namespace CompanyData.Models
{
	[Table("Employee")]
	public class Employee
	{
		// snake_case columns because 
		// 1.  It's painful putting quotes in sql queries to enforce case sensitivity
		// 2.  It's what the linux kernel uses and is therefore virtuous 
		[Key]
		[Column("employee_id")]
		public long EmployeeId { get; set; }

		[Column("name")]
		public string Name { get; set; }

		[Column("home_phone")]
		public string HomePhone { get; set; }

		[Column("cell_phone")]
		public string CellPhone { get; set; }

		[Column("address")]
		public string Address { get; set; }

		[Column("email")]
		public string Email { get; set; }

		[Column("created_date")]
		public DateTime CreatedDate { get; set; }

		[Column("modified_date")]
		public DateTime ModifiedDate { get; set; }

		[Column("status")]
		public StatusEnum Status { get; set; }
		
		[Column("departmentId")]
		public long DepartmentId{ get; set; }

		public Department Department { get; set; }
	}
}
