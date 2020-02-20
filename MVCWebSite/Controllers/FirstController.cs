using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCWebSite.Controllers
{
    public class FirstController : Controller
    {
        // GET: First

        public ActionResult Index(int? id, int? x, int? y)
        {
            ViewBag.Id = id;
            return View();
            //Random r = new Random();

            //int x = r.Next(1,3);
            //if (x == 1)
            //    return View();
            //else
            //    return View("Aniya");
        }
        public ActionResult Operation(string task, int? x, int? y)
        {
            int result = 0;
            if (task == null || x == 0 || y == 0)
            {
                // ViewBag.MyResult = "Please Enter The Valid Information";
                return Content("Please Enter The Valid Information");
            }
            else
            {
                if (task == "add")
                {
                    result = (int)x + (int)y;
                }
                else if (task == "sub")
                {
                    result = (int)x + (int)y;
                }
                else if (task == "mul")
                {
                    result = (int)x * (int)y;
                }
                else if (task == "div")
                {
                    result = (int)x / (int)y;
                }
                else
                {
                    ViewBag.Myresult = "Please enter the right operation";
                }
                ViewBag.task = task;
                ViewBag.X = x;
                ViewBag.Y = y;
                ViewBag.MyResult = result;
            }
            return View();

        }

        public ActionResult Table(int number, int Multiplier)
        {
            ViewBag.Id = number;
            ViewBag.Mul = Multiplier;
            return View();

        }
        public ActionResult TableNew(int From, int To, int Multiplier)
        {
            ViewBag.From = From;
            ViewBag.To = To;
            ViewBag.Mul = Multiplier;
            return View();

        }
        public ActionResult ShowLinks()
        {
            for (int i = 1; i <= 10; i++)
            {
                ViewBag.Id = i;
            }

            return View();

        }
        public ActionResult ShowInfo(String Id)
        {
            ViewBag.Id = Id;
            return View();

        }
        public ActionResult ShowSamePage(int? Id)
        {

            ViewBag.Id = Id;
            return View();

        }
        public ActionResult ShowNextpage(int? Id)
        {
            if (Id == null)
            {
                return Content("Please Enter The Valid Information");
            }
            ViewBag.Id = Id;


            return View();
        }

        List<String> empNames = new List<string> { "Nazmul", "Sudipto", "Rajjo", "Srijan", "Aniya", "Rehamn" };
        List<int> ageOfEmployees = new List<int> { 37, 38, 7, 10, 2, 27 };
        List<String> deptOfEmployees = new List<string> { "IT", "Trainer", "Student", "Student", "Kid", "IT" };
        List<String> officeNameOfEmployees = new List<string> { "https://www.google.com", "http://www.supernovaservices.com", "https://www.google.com", "https://www.microsoft.com", "https://www.google.com", "https://www.microsoft.com" };

        public ActionResult ShowEmployees(string website)
        {
            bool find = false;
          
            //var empName = new List<string> { "se", "ut", "pi" };
            ViewBag.EmpNames = empNames;
            ViewBag.AgeOfEmployees = ageOfEmployees;
            ViewBag.DeptOfEmployees = deptOfEmployees;
            ViewBag.OfficeNameOfEmployees = officeNameOfEmployees;

            if (website != null)//some link was clicked!
            {
                find = true;

                List<String> empNamesSubList = new List<string>();
                List<int> ageOfEmployeesSubList = new List<int>();
                List<String> deptOfEmployeesSubList = new List<string>();
                List<String> officeNameOfEmployeesSubList = new List<string>();

                int totalEmployeeInOffice = 0;
                int totalAgeInOffice = 0;
                double averageAgeInOffice = 0;

                for (int i = 0; i <= officeNameOfEmployees.Count() - 1; i++)
                {
                    if (officeNameOfEmployees[i] == website)
                    {                        
                        empNamesSubList.Add(empNames[i]);
                       
                        ageOfEmployeesSubList.Add(ageOfEmployees[i]);
                        totalAgeInOffice = totalAgeInOffice + ageOfEmployees[i];
                        deptOfEmployeesSubList.Add(deptOfEmployees[i]);
                        officeNameOfEmployeesSubList.Add(officeNameOfEmployees[i]);

                        totalEmployeeInOffice++;
                    }
                
                }

                averageAgeInOffice = (double)totalAgeInOffice / totalEmployeeInOffice;
                ViewBag.AverageAge = averageAgeInOffice;

                ViewBag.EmpNames = empNamesSubList;
                ViewBag.AgeOfEmployees = ageOfEmployeesSubList;
                ViewBag.DeptOfEmployees = deptOfEmployeesSubList;
                ViewBag.OfficeNameOfEmployees = officeNameOfEmployeesSubList;
               

            }
            

            ViewBag.Find = find;
           

            return View();
        }

        public ActionResult ShowEmployeesAccordingOffice(String website)
        {
            //search for only those records matching my id (website)....
            List<String> empNamesSubList = new List<string>(); ;
            List<int> ageOfEmployeesSubList = new List<int>();
            List<String> deptOfEmployeesSubList = new List<string>();
            List<String> officeNameOfEmployeesSubList = new List<string>();



            for (int i = 0; i <= officeNameOfEmployees.Count() - 1; i++)
            {
                if (officeNameOfEmployees[i] == website)
                {
                    empNamesSubList.Add(empNames[i]);
                    ageOfEmployeesSubList.Add(ageOfEmployees[i]);
                    deptOfEmployeesSubList.Add(deptOfEmployees[i]);
                    officeNameOfEmployeesSubList.Add(officeNameOfEmployees[i]);
                }
            }

            ViewBag.EmpNames = empNamesSubList;
            ViewBag.AgeOfEmployees = ageOfEmployeesSubList;
            ViewBag.DeptOfEmployees = deptOfEmployeesSubList;
            ViewBag.OfficeNameOfEmployees = officeNameOfEmployeesSubList;



            return View();

        }

        public ActionResult Prime()
        {
            return View();
        }


        public ActionResult SomePage()
        {
            return Content("I am learning C# in MVC in 30 days! Now I am trying to make a webpage in MVC and C# is a great tool to create webpage and Now I am implementimg the same with Java in the year 2019");
        }


    }
}