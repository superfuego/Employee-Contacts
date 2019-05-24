using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyData.Repositories
{
	public abstract class RepositoryBase : IDisposable
	{
		protected CompanyContext Context { get; }

		public RepositoryBase()
		{
			Context = new CompanyContext();
		}

		public void Dispose()
		{
			Context.Dispose();
		}
	}
}
