using System;

namespace Utility
{
	//Simple class to return to support a "no throw" guarantee between the data stores and the UI.
	public class Error
	{
		public Error(string message) : this(message, null)	{}

		public Error(Exception ex) : this(null, ex)	{}

		public Error(string message, Exception ex)
		{
			Message = message;
			Exception = ex;
		}

		public string Message { get; }
		public Exception Exception { get; }

		public override string ToString()
		{
			return $"{Message}\n{Exception?.ToString()}";
		}
	}
}
