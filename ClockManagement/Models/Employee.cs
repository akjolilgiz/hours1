using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace ClockManagement.Models
{
  public class Employee
  {
    public int id {get; set;}
    public string name {get; set;}
    public string username {get; set;}
    public string password {get; set;}

    public Employee (string Name, string loginUsername, string loginPassword, int employeeId=0)
    {
      id = employeeId;
      name = Name;
      username = loginUsername;
      password = loginPassword;
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
        int employeeId = rdr.GetInt32(0);
        string employeeName = rdr.GetString(1);
        string employeeUsername = rdr.GetString(2);
        string employeeUserPassword = rdr.GetString(3);


        Employee newEmployee = new Employee(employeeName, employeeUsername, employeeUserPassword, employeeId);
        allEmployees.Add(newEmployee);
      }

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allEmployees;
    }

    public static bool Login(string userName, string password)
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();

        MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;

        cmd.CommandText = @"SELECT * FROM employees WHERE username = @searchName AND password = @searchPassword;";

        cmd.Parameters.AddWithValue("@searchName", userName);
        cmd.Parameters.AddWithValue("@searchPassword", password);

        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

        if(rdr.Read())
        {
        // Login Pass
        return true;
        }
        else
        {
        //Login fail
        return false;
        }
      }

      public void Save()
        {
          MySqlConnection conn = DB.Connection();
          conn.Open();

          var cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"INSERT INTO employees (name, username, password) VALUES (@name, @username, @password);";

          cmd.Parameters.AddWithValue("@name", this.name);
          cmd.Parameters.AddWithValue("@username", this.username);
          cmd.Parameters.AddWithValue("@password", this.password);


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
      string username = "";
      string password = "";

      while(rdr.Read())
      {
        employeeId = rdr.GetInt32(0);
        employeeName = rdr.GetString(1);
        username = rdr.GetString(2);
        password = rdr.GetString(3);
      }
      Employee foundEmployee = new Employee(employeeName, username, password, employeeId);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundEmployee;
    }

    public void Edit(string employeeName, string employeeUserName, string employeeUserPassword)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE employees SET name = @newEmployeeName, username = @newEmployeeUserName, password = @newEmployeeUserPassword WHERE id = @searchId;";
      cmd.Parameters.AddWithValue("@searchId", this.id);
      cmd.Parameters.AddWithValue("@newEmployeeName", employeeName);
      cmd.Parameters.AddWithValue("@newEmployeeUserName", employeeUserName);
      cmd.Parameters.AddWithValue("@newEmployeeUserPassword", employeeUserPassword);


      cmd.ExecuteNonQuery();

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
        string employeesUserName = rdr.GetString(2);
        string employeesUserPassword = rdr.GetString(3);

        Employee newEmployee = new Employee (employeesName, employeesUserName, employeesUserPassword, employeeId);
        allEmployees.Add(newEmployee);

      }
      conn.Close();
      if (conn !=null)
      {
        conn.Dispose();
      }
      return allEmployees;
    }

    // public String GetHours()
    //  {
    //    {
    //      MySqlConnection conn = DB.Connection();
    //      conn.Open();
    //      var cmd = conn.CreateCommand() as MySqlCommand;
    //      cmd.CommandText = @"SELECT SUM(hours) as sumHours FROM employees_hours WHERE employee_id = @searchId;";
    //      cmd.Parameters.AddWithValue("@searchId", this.id);
    //
    //      String allHours = ((String)cmd.ExecuteScalar());
    //      conn.Close();
    //      if (conn != null)
    //      {
    //          conn.Dispose();
    //      }
    //      return allHours;
    //    }
    //  }
    public List<Hour> GetHours()
    {
      List<Hour> allHours = new List<Hour> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM employees_hours WHERE employee_id = @searchId;";
      cmd.Parameters.AddWithValue("@searchId", this.id);


      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {

        TimeSpan employeeHours = rdr.GetTimeSpan(3);
        int employeeId = rdr.GetInt32(4);
        DateTime employeeClockIn = rdr.GetDateTime(1);
        DateTime employeeClockOut = rdr.GetDateTime(2);
        int hourId = rdr.GetInt32(0);

        Hour newHour = new Hour(employeeId, employeeClockIn, employeeClockOut, employeeHours, hourId);
        allHours.Add(newHour);
      }

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allHours;
    }
  }
}
