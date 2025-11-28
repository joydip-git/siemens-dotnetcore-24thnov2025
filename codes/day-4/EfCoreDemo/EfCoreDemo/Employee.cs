using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfCoreDemo
{
    [Table("employees")]
    public class Employee
    {       
        [Key]
        [Column("empid")]
        public int EmpId { get; set; }

        [Column("empname")]
        public string EmpName { get; set; } = string.Empty;

        [Column("department")]
        public string? Department { get; set; }

        [Column("salary")]
        public decimal? Salary { get; set; }
    }
}
