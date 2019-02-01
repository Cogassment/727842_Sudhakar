using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Helpers;
using rlgemp.Models;

namespace rlgemp.Repository
{
    public class EmployeeRepository
    {
        public string InsertEmployeeDetails(EmployeeDetails employeeDetail)
        {
            string viewMessage = string.Empty;
            string connectionstring = ConfigurationManager.ConnectionStrings["Constring"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionstring);
            string query = "insert INTO empdetails VALUES(@id,@role,@associatename,@projectname,@managermane)";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@id", SqlDbType.VarChar).Value = employeeDetail.Associateid;
                command.Parameters.Add("@role", SqlDbType.VarChar).Value = employeeDetail.Role;
                command.Parameters.Add("@associatename", SqlDbType.VarChar).Value = employeeDetail.Associatename;
                command.Parameters.Add("@projectname", SqlDbType.VarChar).Value = employeeDetail.Projectname;
                command.Parameters.Add("@managermane", SqlDbType.VarChar).Value = employeeDetail.Managername;

                connection.Open();
                int result = command.ExecuteNonQuery();
                connection.Close();

                if (result == 1)
                {
                    viewMessage = "Record Entered Succesfully";
                }
                else
                {
                    viewMessage = "error";
                }
            }
              return viewMessage;
        }

        public bool AssociateIdExists(string associateId)
        {
            bool isUserExists = false;
            string connectionString = ConfigurationManager.ConnectionStrings["Constring"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string userExistQuery = "Select Count(*) From dbo.empdetails Where associateid='" + associateId + "'";
                using (SqlCommand command = new SqlCommand(userExistQuery, connection))
                {
                    connection.Open();
                    isUserExists = Convert.ToBoolean(command.ExecuteScalar());
                    connection.Close();
                }
            }

            return isUserExists;
        }
        public List<EmployeeDetails> DisplayEmployeeDetails()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Constring"].ConnectionString;
            EmployeeDetails emplopyedetails=null;
            List<EmployeeDetails> employee = new List<EmployeeDetails>();
            using (SqlConnection connection = new SqlConnection(connectionString))
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
            return employee;
        }
 }
}
