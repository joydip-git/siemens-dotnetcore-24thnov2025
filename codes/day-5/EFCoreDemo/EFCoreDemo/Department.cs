namespace EFCoreDemo
{
    public class Department
    {
        public int DeptId { get; set; }
        public string DeptName { get; set; } = string.Empty;

        //navigation property
        public ICollection<Employee>? Employees { get; set; }
    }
}
