using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CompanyData.Models;

namespace CompanyData
{
	public class CompanyContext : DbContext
	{

		public DbSet<Employee> Employees { get; set; }
		public DbSet<Department> Departments { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseNpgsql("Host=localhost;Database=klove;Username=postgres;Password=po5tgre5");
		}

		//Let's overwrite created/modified dates on save.  Mostly taken from stack overflow
		//https://stackoverflow.com/questions/37285948/how-to-set-created-date-and-modified-date-to-enitites-in-db-first-approach
		public override int SaveChanges()
		{
			var AddedEntities = ChangeTracker.Entries().Where(E => E.State == EntityState.Added).ToList();
			var time = DateTime.Now;

			AddedEntities.ForEach(E =>
			{
				E.Property("CreatedDate").CurrentValue = time;
				E.Property("ModifiedDate").CurrentValue = time;
			});

			var ModifiedEntities = ChangeTracker.Entries().Where(E => E.State == EntityState.Modified).ToList();

			ModifiedEntities.ForEach(E =>
			{
				E.Property("ModifiedDate").CurrentValue = time;
			});

			return base.SaveChanges();
		}
	}
}
