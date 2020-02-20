using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCWebSite.Models
{
    public class Employee
    {
        public int EmpId { get; set; }
        public string EmployeeName { get; set; }
        public int AgeOfEmployee { get; set; }
        public string OfficeOfEmployee { get; set; }
    }
}