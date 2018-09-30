using System;
using System.Collections.Generic;
using MySql.Data.MySqlEmployee;

namespace ClockManagement.Models
{
  public class Department
  {
    public int id {get; set;}
    public string name {get; set;}

    public Employee (string Name, int employeeId=0)
    {
      id = employeeId;
      name = employeeName;
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

    public void AddStylist(Stylist newStylist)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO stylists_employees (stylist_id, employee_id) VALUES (@StylistId, @EmployeeId);";

      MySqlParameter stylistId = new MySqlParameter();
      stylistId.ParameterName = "@StylistId";
      stylistId.Value = newStylist.id;
      cmd.Parameters.Add(stylistId);

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


    public List<Stylist> GetStylists()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT stylist_id FROM stylists_employees WHERE employee_id = @employeeId;";

      MySqlParameter employeeIdParameter = new MySqlParameter();
      employeeIdParameter.ParameterName = "@employeeId";
      employeeIdParameter.Value = id;
      cmd.Parameters.Add(employeeIdParameter);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      List<int> stylistIds = new List<int> {};
      while(rdr.Read())
      {
        int stylistId = rdr.GetInt32(0);
        stylistIds.Add(stylistId);
      }
      rdr.Dispose();

      List<Stylist> stylists = new List<Stylist> {};
      foreach (int stylistId in stylistIds)
      {
        var stylistQuery = conn.CreateCommand() as MySqlCommand;
        stylistQuery.CommandText = @"SELECT * FROM stylists WHERE id = @StylistId;";

        MySqlParameter stylistIdParameter = new MySqlParameter();
        stylistIdParameter.ParameterName = "@StylistId";
        stylistIdParameter.Value = stylistId;
        stylistQuery.Parameters.Add(stylistIdParameter);

        var stylistQueryRdr = stylistQuery.ExecuteReader() as MySqlDataReader;
        while(stylistQueryRdr.Read())
        {
          int thisStylistId = stylistQueryRdr.GetInt32(0);
          string stylistName = stylistQueryRdr.GetString(1);
          Stylist foundStylist = new Stylist(stylistName, thisStylistId);
          stylists.Add(foundStylist);
        }
        stylistQueryRdr.Dispose();
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return stylists;
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
