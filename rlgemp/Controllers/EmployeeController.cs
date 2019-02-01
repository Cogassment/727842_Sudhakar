using rlgemp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Configuration;
using rlgemp.Services;
using rlgemp.Repository;

namespace rlgemp.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeeService employeeservice;
        public EmployeeController()
        {
            employeeservice = new EmployeeService();
        }
         [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DisplayEmployeeDetails()
        {

            var result = employeeservice.DisplayEmployeeDetails();

            return Json(result);
        }

        [HttpPost]
        public ActionResult Index(EmployeeDetails employeeDetail)
        {
            var EmployeeService = new EmployeeService();
            try
            {
                if (ModelState.IsValid == true)
                {
                    TempData["usermsg"] = EmployeeService.InsertEmployeeDetails(employeeDetail);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["usermsg"] = ex.Message;
                return RedirectToAction("Errorpage");
            }
        }
    }
}
