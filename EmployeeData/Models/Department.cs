using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utility;

namespace CompanyData.Models
{
	[Table("Department")]
	public class Department
	{
		[Key]
		[Column("department_id")]
		public long DepartmentId { get; set; }
	
		[Column("name")]
		public string Name { get; set; }

		[Column("modified_date")]
		public DateTime ModifiedDate { get; set; }

		[Column("created_date")]
		public DateTime CreatedDate { get; set; }

		[Column("status")]
		public StatusEnum Status { get; set; }
	}
}
