using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreDemo
{
    public class SiemensDbContext : DbContext
    {
        public SiemensDbContext(DbContextOptions<SiemensDbContext> options)
            : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //Action<SqlServerDbContextOptionsBuilder> del = (builder)=>builder
        //    optionsBuilder.UseSqlServer("Data Source=JOYDIP-PC\\SQLEXPRESS;Initial Catalog=siemensdb;Integrated Security=True;Trust Server Certificate=True;");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            EntityTypeBuilder<Department> deptBuilder = modelBuilder.Entity<Department>();

            deptBuilder
                .ToTable("departments")
                .HasKey(d => d.DeptId)
                .HasName("pk_departments_deptid");

            deptBuilder
                .Property<int>(d => d.DeptId)
                .HasColumnName("deptid")
                .HasColumnType("int")
                .IsRequired();

            deptBuilder
                .Property<string>(d => d.DeptName)
                .HasColumnName("deptname")
                .HasColumnType("varchar(50)")
                .IsRequired();

            EntityTypeBuilder<Employee> empBuilder = modelBuilder.Entity<Employee>();

            empBuilder
                .ToTable("employees")
                .HasKey(e => e.EmpId)
                .HasName("pk_employees_empid");

            empBuilder
                .Property<int>(e => e.EmpId)
                .HasColumnName("empid")
                .HasColumnType("int")
                .IsRequired();

            empBuilder
                .Property<string>(e => e.EmpName)
                .HasColumnName("empname")
                .HasColumnType("varchar(50)")
                .IsRequired();

            empBuilder
                .Property<decimal?>(e => e.Salary)
                .HasColumnName("salary")
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0.0m)
                .IsRequired(false);

            empBuilder
                .Property<int?>(e => e.DeptId)
                .HasColumnName("deptid")
                .HasColumnType("int")
                .IsRequired(false);

            empBuilder
                .HasOne<Department>(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DeptId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Employee_Department");

            //deptBuilder
            //    .HasMany(d => d.Employees)
            //    .WithOne(e => e.Department)
            //    .HasForeignKey(e => e.DeptId);

            deptBuilder
                .HasData(
                new Department { DeptId = 1, DeptName = "Human Resources" },
                new Department { DeptId = 2, DeptName = "Information Technology" }
            );
            empBuilder
                .HasData(
                new Employee { EmpId = 1, EmpName = "John Doe", Salary = 60000.00m, DeptId = 1 },
                new Employee { EmpId = 2, EmpName = "Jane Smith", Salary = 75000.00m, DeptId = 2 }
            );
        }
    }
}
