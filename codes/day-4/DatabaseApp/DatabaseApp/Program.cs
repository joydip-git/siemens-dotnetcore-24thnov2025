using DatabaseApp;

EmployeeDbContext employeeRepository = new EmployeeDbContext();
Employee employee = new Employee { EmpId = 102, EmpName = "Alice Johnson", Department = "HR", Salary = 60000m };
int result = employeeRepository.AddEmployee(employee);
if(result > 0)
{
    Console.WriteLine("Employee added successfully.");
}
else
{
    Console.WriteLine("Failed to add employee.");
}
var list = employeeRepository.Employees();
if (list != null)
{
    foreach (var emp in list)
    {
        Console.WriteLine($"ID: {emp.EmpId}, Name: {emp.EmpName}, Department: {emp.Department}, Salary: {emp.Salary}");
    }
}