namespace OptionsPattern
{
    public class Employee { }
    public class Department { }
    public interface IRepo<T>
    {
        T GetData();
    }
    public class EmployeeRepo : IRepo<Employee>
    {
        public Employee GetData()
        {
            return new();
        }
    }
    public class DepartmentRepo : IRepo<Department>
    {
        public Department GetData()
        {
            return new();
        }
    }
}
