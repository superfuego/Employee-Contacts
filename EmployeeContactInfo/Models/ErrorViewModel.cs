using System;

namespace EmployeeContactInfo.Models
{
	public class ErrorViewModel
	{
		public string RequestId { get; set; }

		public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

		public bool String { get; set; }
	}
}