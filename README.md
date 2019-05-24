# Employee-Contacts

Instructions for running:
1.  It needs a PostgreSQL instance.  Can run locally, easy to install in docker if you have it.  
2.  Open the project in Visual Studio
3.  Update the connection string in CompanyContext.OnConfiguring to point to your PostgreSQL instance
4.  Set the startup project to CompanyData
5.  Open Tools => Nuget Package Manager => Package Manager Console
5.  Set the Default project in Package Manager Console to CompanyData
6.  run "Update-Database" in Package Manager Console
5.  Set the EmployeeContactInfo project as the startup project.
6.  Run (F5)
6.  There are two pages, Employees and Departments, that can be reached from the top navigation bar on the home page.

If you're not using visual studio, the "Update-Database" command can be run in powershell.
If you run into any issues, happy to look into them with you.

Thanks,
Nelson
