using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace EfCoreDemo
{
    [Table("departments")]
    public class Department
    {
        [Key]
        [Column("deptid", TypeName = "INT")]
        [NotNull]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DeptId { get; set; }

        [Column("deptname", TypeName = "varchar(50)")]
        [NotNull]
        [MaxLength(50)]
        public string DeptName { get; set; } = string.Empty;
    }
}
