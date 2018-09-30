using Microsoft.AspNetCore.Mvc;
using ClockManagement.Models;
using System.Collections.Generic;

namespace ClockManagement.Controllers
{
  public class DepartmentController : Controller
  {
    [HttpGet("/departments")]
    public ActionResult Index()
    {
      Dictionary<string, object> model = new Dictionary <string, object>();
      List<Department> allDepartments = Department.GetAll();
      List<Employee> allEmployees = Employee.GetAll();
      model.Add("allDepartments", allDepartments);
      model.Add("allEmployees", allEmployees);
      return View(model);
    }

    [HttpGet("departments/new")]
    public ActionResult CreateForm()
    {
      Dictionary<string, object> model = new Dictionary <string, object>();
      List<Employee> allEmployees = Employee.GetAll();
      List<Department> allDepartments = Department.GetAll();
      model.Add("allEmployees",allEmployees);
      model.Add("allDepartments",allDepartments);
      return View(model);
    }

    [HttpPost("/departments")]
    public ActionResult Create(int departmentEmployeeId, string departmentName)
    {
      Department newDepartment = new Department(departmentName);
      newDepartment.Save();
      Employee foundEmployee = Employee.Find(departmentEmployeeId);
      newDepartment.AddEmployee(foundEmployee);

      return RedirectToAction("Index");
    }

    [HttpGet("/departments/delete")]
    public ActionResult DeleteAll()
    {
      Employee.DeleteAll();
      return RedirectToAction("Index");
    }

    [HttpGet("departments/{departmentId}")]
    public ActionResult Details(int departmentId)
    {
      Dictionary<string, object> model = new Dictionary <string, object>();
      Department selectedDepartment = Department.Find(departmentId);
      List<Employee> departmentEmployees = selectedDepartment.GetEmployees();
      List<Employee> allEmployees = Employee.GetAll();
      model.Add("selectedDepartment", selectedDepartment);
      model.Add("departmentEmployees", departmentEmployees);
      model.Add("allEmployees", allEmployees);
      return View(model);
    }

    [HttpPost("/departments/{departmentId}")]
    public ActionResult AddDepartment(int EmployeeId, int departmentId)
    {
      Employee foundEmployee = Employee.Find(EmployeeId);
      Department foundDepartment = Department.Find(departmentId);
      foundDepartment.AddEmployee(foundEmployee);
      return RedirectToAction("Details", new {departmentId = foundDepartment.id});
    }


    [HttpGet("/departments/{departmentId}/update")]
    public ActionResult UpdateForm(int departmentId)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      List<Employee> allEmployees = Employee.GetAll();
      Department selectedDepartment = Department.Find(departmentId);
      model.Add("allEmployees", allEmployees);
      model.Add("department", selectedDepartment);
      return View(model);
    }

    [HttpPost("/departments/{departmentId}/update")]
    public ActionResult Update(int departmentId, string newName)
    {
      Department thisDepartment = Department.Find(departmentId);
      thisDepartment.Edit(newName);
      return RedirectToAction("Details", new {departmentId = thisDepartment.id});
    }

    [HttpGet("/departments/{departmentId}/delete")]
    public ActionResult Delete(int departmentId)
    {
      Department thisDepartment = Department.Find(departmentId);
      Department.Delete(departmentId);
      return RedirectToAction("Index");
    }

    [HttpPost("/departments/search")]
    public ActionResult Search()
    {
      string userInput = Request.Form["searched"];
      string searchedDepartment =
      char.ToUpper(userInput[0]) + userInput.Substring(1);
      List<Department> foundDepartments = Department.SearchDepartment(searchedDepartment);
      return View(foundDepartments);
    }
  }
}
