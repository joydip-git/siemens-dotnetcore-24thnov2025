namespace DatabaseApp
{
    public class Employee
    {
        public int EmpId { get; set; }
        public string EmpName { get; set; } = string.Empty;
        public string? Department { get; set; }
        public decimal? Salary { get; set; }
    }
}
