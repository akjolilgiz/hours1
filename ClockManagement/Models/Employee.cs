using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace ClockManagement.Models
{
  public class Employee
  {
    public int id {get; set;}
    public string name {get; set;}

    public Employee (string Name, int employeeId=0)
    {
      id = employeeId;
      name = Name;
    }

    public override bool Equals(System.Object otherEmployee)
    {
      if (!(otherEmployee is Employee))
      {
        return false;
      }
      else
      {
        Employee newEmployee = (Employee) otherEmployee;
        bool areIdsEqual = (this.id == newEmployee.id);
        bool areDescriptionsEqual = (this.name == newEmployee.name);
        return (areIdsEqual && areDescriptionsEqual);
      }
    }

    public override int GetHashCode()
    {
      return this.name.GetHashCode();
    }

    public static List<Employee> GetAll()
    {
      List<Employee> allEmployees  = new List<Employee> ();
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = "SELECT * FROM employees;";

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        string employeeName = rdr.GetString(1);
        int employeeId = rdr.GetInt32(0);

        Employee newEmployee = new Employee(employeeName, employeeId);
        allEmployees.Add(newEmployee);
      }

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allEmployees;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO employees (name) VALUES (@name);";

      MySqlParameter employeeName = new MySqlParameter();
      employeeName.ParameterName = "@name";
      employeeName.Value = this.name;
      cmd.Parameters.Add(employeeName);

      cmd.ExecuteNonQuery();
      id = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static void Delete(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM employees WHERE id = @thisId;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM employees;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static Employee Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM employees WHERE id = @searchId;";

      MySqlParameter SearchId = new MySqlParameter();
      SearchId.ParameterName = "@SearchId";
      SearchId.Value = id;
      cmd.Parameters.Add(SearchId);

      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

      int employeeId = 0;
      string employeeName = "";

      while(rdr.Read())
      {
        employeeId = rdr.GetInt32(0);
        employeeName = rdr.GetString(1);
      }
      Employee foundEmployee = new Employee(employeeName, employeeId);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundEmployee;
    }

    public void Edit(string employeeName)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE employees SET name = @newEmployeeName WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = this.id;
      cmd.Parameters.Add(searchId);

      MySqlParameter newEmployeeName = new MySqlParameter();
      newEmployeeName.ParameterName = "@newEmployeeName";
      newEmployeeName.Value = employeeName;
      cmd.Parameters.Add(newEmployeeName);

      cmd.ExecuteNonQuery();
      this.name = employeeName;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void AddDepartment(Department newDepartment)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO departments_employees (department_id, employee_id) VALUES (@DepartmentId, @EmployeeId);";

      MySqlParameter departmentId = new MySqlParameter();
      departmentId.ParameterName = "@DepartmentId";
      departmentId.Value = newDepartment.id;
      cmd.Parameters.Add(departmentId);

      MySqlParameter employeeId = new MySqlParameter();
      employeeId.ParameterName = "@EmployeeId";
      employeeId.Value = id;
      cmd.Parameters.Add(employeeId);

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }


    public List<Department> GetDepartments()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT department_id FROM departments_employees WHERE employee_id = @employeeId;";

      MySqlParameter employeeIdParameter = new MySqlParameter();
      employeeIdParameter.ParameterName = "@employeeId";
      employeeIdParameter.Value = id;
      cmd.Parameters.Add(employeeIdParameter);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      List<int> departmentIds = new List<int> {};
      while(rdr.Read())
      {
        int departmentId = rdr.GetInt32(0);
        departmentIds.Add(departmentId);
      }
      rdr.Dispose();

      List<Department> departments = new List<Department> {};
      foreach (int departmentId in departmentIds)
      {
        var departmentQuery = conn.CreateCommand() as MySqlCommand;
        departmentQuery.CommandText = @"SELECT * FROM departments WHERE id = @DepartmentId;";

        MySqlParameter departmentIdParameter = new MySqlParameter();
        departmentIdParameter.ParameterName = "@DepartmentId";
        departmentIdParameter.Value = departmentId;
        departmentQuery.Parameters.Add(departmentIdParameter);

        var departmentQueryRdr = departmentQuery.ExecuteReader() as MySqlDataReader;
        while(departmentQueryRdr.Read())
        {
          int thisDepartmentId = departmentQueryRdr.GetInt32(0);
          string departmentName = departmentQueryRdr.GetString(1);
          Department foundDepartment = new Department(departmentName, thisDepartmentId);
          departments.Add(foundDepartment);
        }
        departmentQueryRdr.Dispose();
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return departments;
    }

    public static List<Employee> SearchEmployee(string employeeName)
    {
      List<Employee> allEmployees = new List<Employee>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;

      cmd.CommandText = @"SELECT * FROM employees WHERE name LIKE @searchName;";

      cmd.Parameters.AddWithValue("@searchName", "%" + employeeName + "%");

      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

      while (rdr.Read())
      {
        int employeeId = rdr.GetInt32(0);
        string employeesName = rdr.GetString(1);

        Employee newEmployee = new Employee (employeesName, employeeId);
        allEmployees.Add(newEmployee);

      }
      conn.Close();
      if (conn !=null)
      {
        conn.Dispose();
      }
      return allEmployees;
    }
  }
}
