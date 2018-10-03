using Microsoft.AspNetCore.Mvc;
using ClockManagement.Models;
using System.Collections.Generic;

namespace ClockManagement.Controllers
{
  public class HourController : Controller
  {
    [HttpGet("/hours")]
    public ActionResult Index()
    {
      List<Hour> allHours = Hour.GetAll();
      return View(allHours);
    }

    [HttpPost("/employees/{id}/clockIn")]
    public ActionResult clockIn(int id)
    {
      Dictionary<string, object> model = new Dictionary <string, object>();
      Employee foundEmployee = Employee.Find(id);
      Hour clockInHour = new Hour(foundEmployee.id);
      clockInHour.Save(foundEmployee.id);
      clockInHour.ClockIn(foundEmployee.id);
      model.Add("foundEmployee", foundEmployee);
      model.Add("clockInHour", clockInHour);
      return RedirectToAction("Details","Employee", new {employeeId = foundEmployee.id});
    }

    [HttpPost("/employees/{id}/clockOut")]
    public ActionResult clockOut(int id)
    {
      Dictionary<string, object> model = new Dictionary <string, object>();
      Employee selectedEmployee = Employee.Find(id);
      Hour clockOutHour = Hour.Find(selectedEmployee.id);
      clockOutHour.ClockOut(selectedEmployee.id);
      clockOutHour.Hours(selectedEmployee.id);
      model.Add("selectedEmployee",selectedEmployee);
      // model.Add("clockOutHour", clockOutHour);
      return RedirectToAction("Details","Employee", new {employeeId = selectedEmployee.id});
    }

    [HttpGet("/reports")]
    public ActionResult AllReports()
    {
      return View("~/Views/Report/Index.cshtml");
    }

    [HttpGet("/totalshiftsperaccount")]
    public ActionResult TotalShiftsPerAccount()
    {
      List<Employee> allEmployees = Employee.GetAll();
      return View("~/Views/Report/TotalShiftsPerAccount.cshtml", allEmployees);
    }

    [HttpPost("/totalshiftsperaccount")]
    public ActionResult EmployeeShift(int employeeId)
    {
      Dictionary<string, object> model = new Dictionary<string,object>();
      Employee selectedEmployee = Employee.Find(employeeId);
      List<Hour> totalHours = Hour.TotalHours(employeeId);
      model.Add("selectedEmployee", selectedEmployee);
      model.Add("totalHours", totalHours);
      return View("~/Views/Report/TotalResults.cshtml", model);
    }
  }
}
