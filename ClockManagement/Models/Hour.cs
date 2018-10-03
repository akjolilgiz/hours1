using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace ClockManagement.Models
{
  public class Hour
  {
    public int id {get; set;}
    public DateTime? clock_in {get; set; }
    public DateTime? clock_out {get; set; }
    public TimeSpan? hours {get; set; }
    public int employee_id {get; set; }

    public Hour (int newEmployee_id, DateTime? clockIn = null, DateTime? clockOut = null, TimeSpan? newHours = null, int Id=0)
    {
      id = Id;
      hours = newHours;
      employee_id = newEmployee_id;
      clock_in = clockIn;
      clock_out = clockOut;
    }

    public override bool Equals(System.Object otherHour)
    {
      if (!(otherHour is Hour))
      {
        return false;
      }
      else
      {
        Hour newHour = (Hour) otherHour;
        bool areIdsEqual = (this.id == newHour.id);
        bool areClockInsEqual = (this.clock_in == newHour.clock_in);
        bool areClockOutsEqual = (this.clock_out == newHour.clock_out);
        bool areHoursEqual = (this.hours == newHour.hours);
        bool areEmployeeIdsEqual = (this.employee_id == newHour.employee_id);

        return (areIdsEqual && areEmployeeIdsEqual && areHoursEqual && areClockOutsEqual && areClockInsEqual);
      }
    }

    public override int GetHashCode()
    {
      return this.hours.GetHashCode();
    }

    public static List<Hour> GetAll()
      {
        List<Hour> allHours  = new List<Hour> ();
        MySqlConnection conn = DB.Connection();
        conn.Open();

        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = "SELECT * FROM employees_hours;";
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

      public void ClockIn(int id)
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();

        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"UPDATE employees_hours SET clock_in = @clockIn WHERE employee_id = @searchId;";
        cmd.Parameters.AddWithValue("@clockIn", DateTime.Now);
        cmd.Parameters.AddWithValue("@searchId", id);


        cmd.ExecuteNonQuery();
        id = (int) cmd.LastInsertedId;

        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
      }

      public void ClockOut(int id)

      {
        MySqlConnection conn = DB.Connection();
        conn.Open();

        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"UPDATE employees_hours SET clock_out = @clockOut WHERE employee_id = @newId;";
        cmd.Parameters.AddWithValue("@clockOut", DateTime.Now);
        cmd.Parameters.AddWithValue("@newId", id);


        cmd.ExecuteNonQuery();
        id = (int) cmd.LastInsertedId;

        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
      }

      public void Hours(int id)
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();

        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"UPDATE employees_hours SET hours = TIMEDIFF( clock_out, clock_in) WHERE employee_id = @thisId;";

        cmd.Parameters.AddWithValue("@thisId", id);


        cmd.ExecuteNonQuery();
        id = (int) cmd.LastInsertedId;

        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
      }


      public static Hour Find(int id)
       {
           MySqlConnection conn = DB.Connection();
           conn.Open();
           var cmd = conn.CreateCommand() as MySqlCommand;
           cmd.CommandText = @"SELECT * FROM employees_hours WHERE employee_id = (@searchId);";
           cmd.Parameters.AddWithValue("@searchId", id);

           var rdr = cmd.ExecuteReader() as MySqlDataReader;
           int Id = 0;
           DateTime? employeeClockIn = null;
           DateTime? employeeClockOut = null;
           TimeSpan? employeeHours = null;
           int employeeId = 0;

           while(rdr.Read())
           {
             Id = rdr.GetInt32(0);
             employeeClockIn = rdr.GetDateTime(1);
             employeeClockOut = rdr.GetDateTime(2);
             employeeHours = rdr.GetTimeSpan(3);
             employeeId = rdr.GetInt32(4);
           }
           Hour newHour = new Hour(employeeId, employeeClockIn, employeeClockOut, employeeHours, Id);
           conn.Close();
           if (conn != null)
           {
               conn.Dispose();
           }
           return newHour;
       }

      //  public void Save()
      //  {
      //    MySqlConnection conn = DB.Connection();
      //    conn.Open();
       //
      //    var cmd = conn.CreateCommand() as MySqlCommand;
      //    cmd.CommandText = @"INSERT INTO employees_hours (employee_id) VALUES (@searchId);";
       //
      //    MySqlParameter employeeName = new MySqlParameter();
      //    employeeName.ParameterName = "@searchId";
      //    employeeName.Value = this.id;
      //    cmd.Parameters.Add(employeeName);
       //
      //    cmd.ExecuteNonQuery();
      //    id = (int) cmd.LastInsertedId;
       //
      //    conn.Close();
      //    if (conn != null)
      //    {
      //      conn.Dispose();
      //    }
      //  }

       public void AddEmployee(Employee newEmployee)
       {
         MySqlConnection conn = DB.Connection();
         conn.Open();
         var cmd = conn.CreateCommand() as MySqlCommand;
         cmd.CommandText = @"INSERT INTO employees_hours (employee_id) VALUES (@EmployeeId)";
         cmd.Parameters.AddWithValue("@EmployeeId", newEmployee.id);
         cmd.ExecuteNonQuery();

         conn.Close();
         if (conn != null)
         {
           conn.Dispose();
         }
       }
      //  public List<Employee> GetEmployees()
      //    {
      //      List<Employee> allEmployees = new List<Employee>{};
      //      MySqlConnection conn = DB.Connection();
      //      conn.Open();
      //      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      //      cmd.CommandText = @"SELECT * FROM employees WHERE id = @employeeId ;";
       //
      //      cmd.Parameters.AddWithValue("@employeeId", this.employee_id);
       //
      //      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
       //
      //      while(rdr.Read())
      //      {
      //        int Id = rdr.GetInt32(0);
      //        string employeeName = rdr.GetString(1);
      //        string employeeUserName = rdr.GetString(2);
      //        string employeeUserPassword = rdr.GetString(3);
       //
      //        Employee newEmployee = new Employee(employeeName, employeeUserName, employeeUserPassword, Id);
      //        allEmployees.Add(newEmployee);
      //      }
      //      conn.Close();
      //      if (conn != null)
      //      {
      //        conn.Dispose();
      //      }
      //      return allEmployees;
      //    }
      public void Save(int id)
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();

        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"INSERT INTO employees_hours (employee_id) VALUES (@Id);";

        MySqlParameter employeeName = new MySqlParameter();
        employeeName.ParameterName = "@Id";
        employeeName.Value = id;
        cmd.Parameters.Add(employeeName);

        cmd.ExecuteNonQuery();
        id = (int) cmd.LastInsertedId;

        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
      }
  }
}
