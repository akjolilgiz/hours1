using Microsoft.AspNetCore.Mvc;
using ClockManagement.Models;
using System.Collections.Generic;

namespace ClockManagement.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet("/")]
    public ActionResult Index()
    {
      return View();
    }

    [HttpGet("/login")]
    public ActionResult Login()
    {
      return View();
    }

    [HttpPost("/login")]
    public ActionResult CreateLogIn(string userName, string password)
    {
      bool result = Employee.Login(userName, password);
      string resultString = "";
      if (result == true)
      {
        return View("Login");
      }
      else
      {
        resultString = "false";
      }
      return View(result);
    }

    [HttpGet("/signup")]
    public ActionResult Signup()
    {
      Dictionary<string, object> model = new Dictionary <string, object>();
      List<Department> allDepartments = Department.GetAll();
      List<Employee> allEmployees = Employee.GetAll();
      model.Add("allDepartments",allDepartments);
      model.Add("allEmployees",allEmployees);
      return View(model);
    }

    [HttpPost("/signup")]
    public ActionResult SignUp(int departmentId, string employeeName, string employeeUserName, string employeeUserPassword)
    {
      Employee newEmployee = new Employee(employeeName, employeeUserName, employeeUserPassword);
      newEmployee.Save();
      Department foundDepartment = Department.Find(departmentId);
      newEmployee.AddDepartment(foundDepartment);

      return RedirectToAction("Login");
    }
  }
}
