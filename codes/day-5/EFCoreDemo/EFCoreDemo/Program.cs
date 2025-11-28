// See https://aka.ms/new-console-template for more information
using EFCoreDemo;
using Microsoft.EntityFrameworkCore;

DbContextOptionsBuilder<SiemensDbContext> optionsBuilder = new DbContextOptionsBuilder<SiemensDbContext>();

optionsBuilder
    .UseSqlServer("Data Source=JOYDIP-PC\\SQLEXPRESS;Initial Catalog=siemensdb;Integrated Security=True;Trust Server Certificate=True;");


DbContextOptions<SiemensDbContext> dbContextOptions = optionsBuilder.Options;

using (SiemensDbContext dbContext = new SiemensDbContext(dbContextOptions))
{

    var employees = dbContext.Employees;
    var department = dbContext.Employees.First(e => e.EmpId == 1).Department;
    Console.WriteLine(department?.DeptName);
    employees
        .ToList()
        .ForEach(e =>
    {
        Console.WriteLine($"ID: {e.EmpId}, Name: {e.EmpName}, DeptID: {e.DeptId}");
        var dept = e.Department;
        Console.WriteLine(dept?.DeptName);
    });
    Console.WriteLine("\n\n\n");
    var departments = dbContext.Departments;
    departments
        .ToList()
        .ForEach(d =>
        {
            Console.WriteLine($"DeptID: {d.DeptId}, DeptName: {d.DeptName}");
            d.Employees?
                    .ToList()
                    .ForEach(emp =>
            {
                Console.WriteLine($"\tEmpID: {emp.EmpId}, EmpName: {emp.EmpName}");
            });
        });
}
