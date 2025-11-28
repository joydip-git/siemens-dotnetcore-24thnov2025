using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace DatabaseApp
{
    public class DbContextOptions
    {
        public string ConnectionString { get; set; } = string.Empty;
    }
    public class EmployeeDbContext
    {
        private readonly string _connectionString;
        public EmployeeDbContext(IOptions<DbContextOptions> options)
        {
            _connectionString = options.Value.ConnectionString;
        }
        public List<Employee>? Employees()
        {
            SqlConnection? connection = null;
            //string conStr = @"Data Source=JOYDIP-PC\SQLEXPRESS;Initial Catalog=siemensdb;Integrated Security=True;Trust Server Certificate=True;";
            string query = "SELECT * FROM Employees";
            List<Employee>? employees = null;
            try
            {
                //using (connection = new SqlConnection(conStr))
                using (connection = new SqlConnection(_connectionString))
                {
                    SqlCommand command = connection == null ? new SqlCommand(query, connection) : connection.CreateCommand();
                    command.CommandText = query;

                    connection?.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        employees = new List<Employee>();
                        while (reader.Read())
                        {
                            Employee emp = new Employee
                            {
                                EmpId = Convert.ToInt32(reader["empid"]),
                                EmpName = reader["empname"].ToString() ?? string.Empty,
                                Department = reader["department"] as string,
                                Salary = reader["salary"] as decimal?
                            };
                            employees.Add(emp);
                        }
                    }
                }
                return employees;
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection?.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        public int AddEmployee(Employee emp)
        {
            SqlConnection? connection = null;
            string conStr = @"Data Source=JOYDIP-PC\SQLEXPRESS;Initial Catalog=siemensdb;Integrated Security=True;Trust Server Certificate=True;";
            string query = "INSERT INTO Employees (empid, empname, department, salary) VALUES (@EmpId, @EmpName, @Department, @Salary)";
            try
            {
                connection = new SqlConnection(conStr);
                SqlCommand command = connection.CreateCommand();
                command.CommandText = query;
                command.Parameters.AddWithValue("@EmpId", emp.EmpId);
                command.Parameters.AddWithValue("@EmpName", emp.EmpName);
                command.Parameters.AddWithValue("@Department", (object?)emp.Department ?? DBNull.Value);
                command.Parameters.AddWithValue("@Salary", (object?)emp.Salary ?? 0);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected;
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection?.State == ConnectionState.Open)
                    connection.Close();
            }
        }
    }
}
