using rlgemp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using rlgemp.Repository;

namespace rlgemp.Services
{
    public class EmployeeService
    {
        EmployeeRepository employeeRepository;
        public EmployeeService()
        {
            employeeRepository = new EmployeeRepository();
        }
        public string InsertEmployeeDetails(EmployeeDetails employeeDetail)
        {
            var result = string.Empty;
            if (!employeeRepository.AssociateIdExists(employeeDetail.Associateid))
            {
                result = employeeRepository.InsertEmployeeDetails(employeeDetail);
            }
            else
            {
                result = "associateId Already Exist";
            }

            return result;
        }

        public List<EmployeeDetails> Home()
        {
            return employeeRepository.Home();
        }
    }

}