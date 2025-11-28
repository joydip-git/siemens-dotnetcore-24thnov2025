namespace EFCoreDemo
{
    public class Employee
    {
        public int EmpId { get; set; }
        public string EmpName { get; set; } = string.Empty;
        public decimal? Salary { get; set; }
        public int? DeptId { get; set; }

        //navigation property
        public Department? Department { get; set; }
    }
}
