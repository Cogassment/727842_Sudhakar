using rlgemp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Configuration;

namespace rlgemp.Controllers
{
    public class EmployeeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(EmployeeDetails employeedetail)
        {
            if (ModelState.IsValid == true)
            {
                if (AssociateIdExists(employeedetail.Associateid) != true)
                {
                    try
                    {
                        string connectionstring = ConfigurationManager.ConnectionStrings["Constring"].ConnectionString;
                        SqlConnection connection = new SqlConnection(connectionstring);
                        string query = "insert INTO empdetails VALUES(@id,@role,@associatename,@projectname,@managermane)";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.Add("@id", SqlDbType.VarChar).Value = employeedetail.Associateid;
                            command.Parameters.Add("@role", SqlDbType.VarChar).Value = employeedetail.Role;
                            command.Parameters.Add("@associatename", SqlDbType.VarChar).Value = employeedetail.Associatename;
                            command.Parameters.Add("@projectname", SqlDbType.VarChar).Value = employeedetail.Projectname;
                            command.Parameters.Add("@managermane", SqlDbType.VarChar).Value = employeedetail.Managername;
                            connection.Open();
                            int result = command.ExecuteNonQuery();
                            connection.Close();
                            if (result == 1)
                            {
                                TempData["usermsg"] = "Record Entered Succesfully";
                            }
                            else
                            {
                                TempData["usermsg"] = "error";
                            }
                }
                }
                    catch (Exception ex)
                    {
                        TempData["usermsg"] = "error";
                    }
                }
                else
                {
                    TempData["usermsg"] = "associateId Already Exist";
                }
           }
            return RedirectToAction("Index");
        }

        public ActionResult Home()
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["Constring"].ConnectionString;
            EmployeeDetails emplopyedetails;
            List<EmployeeDetails> employee = new List<EmployeeDetails>();
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                SqlCommand cmd = new SqlCommand("select * from empdetails", connection);
                connection.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    emplopyedetails = new EmployeeDetails();
                    emplopyedetails.Associateid = rdr["associateId"].ToString();
                    emplopyedetails.Role = rdr["role"].ToString();
                    emplopyedetails.Associatename = rdr["associatename"].ToString();
                    emplopyedetails.Projectname = rdr["projectname"].ToString();
                    emplopyedetails.Managername = rdr["managername"].ToString();
                    employee.Add(emplopyedetails);
                }
            }
            return Json(employee);
        }
        public bool AssociateIdExists(string associateid)
        {
            bool isUserExists = false;
           
            string connectionstring = ConfigurationManager.ConnectionStrings["Constring"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                string userExistQuery = "Select Count(*) From dbo.empdetails Where associateid='" + associateid + "'";
                using (SqlCommand command = new SqlCommand(userExistQuery, connection))
                {
                    connection.Open();
                    isUserExists = Convert.ToBoolean(command.ExecuteScalar());
                    connection.Close();
                }
            }
            return isUserExists;
        }
    }
}     