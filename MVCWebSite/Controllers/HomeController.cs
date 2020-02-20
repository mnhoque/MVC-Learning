using MVCWebSite.DAL;
using MVCWebSite.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCWebSite.Controllers
{
    public class HomeController : Controller
    {
        public static int pageSize = 3;

        // GET: Home
        public ActionResult ShowDetails()
        {

            DataTable dt = DB.Select("Select * From Employees");

            ViewBag.Emps = dt;


            return View();
        }

        public ActionResult ShowFromDetails(int? id)
        {

            DataTable dt = DB.Select("Select * From Employees");
            int rowNumber = dt.Rows.Count;

            int pageCount = 1 + (rowNumber / pageSize);
            //System.Diagnostics.Debug.WriteLine($"The page count is {pageCount}");
            //ViewBag.Emps = dt;

            ViewBag.PageCount = pageCount;
            ViewBag.RowNumber = rowNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.Emps = dt;
            return View();
        }

        [HttpGet]
        public ActionResult EmployeeDetails(int? selectedEmpId, int? n = 1)
        {
            ViewBag.n = n;

            if (selectedEmpId != null)
            {
                TempData["SelectedRowEmpId"] = selectedEmpId;
            }

            if (TempData["SelectedRowEmpId"] != null)
            {
                ViewBag.SelectedRowEmpId = TempData["SelectedRowEmpId"];
                TempData.Keep("SelectedRowEmpId");
            }



            DataTable dt = DB.Select("Select * From Employees");
            //ViewBag.totalPageNumber = dt.Rows.Count / pageSize + 1;
            //DataRow dr = dt.Rows[0];
            // string selectedRow = (Request["select"]);

            if (dt.Rows.Count % pageSize == 0)
            {
                ViewBag.totalPageNumber = dt.Rows.Count / pageSize;
            }
            else
            {
                ViewBag.totalPageNumber = dt.Rows.Count / pageSize + 1;
            }

            List<Employee> lst = new List<Employee>();//this list will contain the content of the page n

            int startIndex = ((int)n - 1) * pageSize;
            int endIndex = ((int)n - 1) * pageSize + (pageSize - 1);


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i >= startIndex && i <= endIndex)
                {
                    Employee emp = new Employee();
                    emp.EmpId = (int)dt.Rows[i]["EmpId"];
                    emp.EmployeeName = (string)dt.Rows[i]["EmployeeName"];
                    emp.AgeOfEmployee = (int)dt.Rows[i]["AgeOfEmployee"];
                    emp.OfficeOfEmployee = (string)dt.Rows[i]["OfficeOfEmployee"];

                    lst.Add(emp);
                }

            }
            return View(lst);
        }

        [HttpPost]
        [ActionName("EmployeeDetails")]
        public ActionResult EmployeeDetailsPost(int? n = 1)
        {
            pageSize = int.Parse(Request["size"]);


            return RedirectToAction("EmployeeDetails");//return to the get method!
        }

        [HttpGet]
        public ActionResult Create()
        {
            Employee emp = new Employee();
            return View(emp);
        }

        [HttpPost]
        [ActionName("Create")]
        public ActionResult CreatePost(Employee emp)
        {
            DB.ExecuteCommand($"Insert into Employees(EmployeeName, AgeOfEmployee,OfficeOfEmployee) values('{emp.EmployeeName}',{emp.AgeOfEmployee},'{emp.OfficeOfEmployee}')");

            int x = 5;

            DataTable dt = DB.Select("Select * From Employees");

            if (dt.Rows.Count % pageSize == 0)
            {
                x = dt.Rows.Count / pageSize;
            }
            else
            {
                x = dt.Rows.Count / pageSize + 1;
            }
            return RedirectToAction("EmployeeDetails", new { n = x });
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            DataTable dt = DB.Select("Select * From Employees");

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["EmpId"].ToString() == id.ToString())
                {
                    Employee emp = new Employee();
                    emp.EmpId = (int)dr["EmpId"];
                    emp.EmployeeName = (string)dr["EmployeeName"];
                    emp.AgeOfEmployee = (int)dr["AgeOfEmployee"];
                    emp.OfficeOfEmployee = (string)dr["OfficeOfEmployee"];

                    return View(emp);
                }
            }

            return Content("Invalid id " + id);
        }

        [HttpPost]
        [ActionName("Edit")]
        public ActionResult EditPost(Employee model)
        {
            DB.ExecuteCommand($"UPDATE Employees SET EmployeeName = '{model.EmployeeName}', AgeOfEmployee = {model.AgeOfEmployee},OfficeOfEmployee = '{model.OfficeOfEmployee}' WHERE EmpId = {model.EmpId}");
            return RedirectToAction("EmployeeDetails");
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return Content("Please supply the emp id!");
            }

            int pageView;

            DataTable dt = DB.Select("Select * From Employees");
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                if (dt.Rows[i]["EmpId"].ToString() == id.ToString())
                {
                    if ((i + 1) % pageSize == 0)
                    {
                        pageView = (i + 1) / pageSize;
                    }
                    else
                    {
                        pageView = ((i + 1) / pageSize) + 1;
                    }
                    ViewBag.PageView = pageView;
                    Employee emp = new Employee();
                    emp.EmpId = (int)dt.Rows[i]["EmpId"];
                    emp.EmployeeName = (string)dt.Rows[i]["EmployeeName"];
                    emp.AgeOfEmployee = (int)dt.Rows[i]["AgeOfEmployee"];
                    emp.OfficeOfEmployee = (string)dt.Rows[i]["OfficeOfEmployee"];

                    return View(emp);
                }
            }
            return Content("Employee id has not matched with supplied " + id);
        }

        public ActionResult Delete(int id)
        {
            DataTable dt = DB.Select("Select * From Employees Where EmpId = " + id);
            if (dt.Rows.Count == 0)
                return RedirectToAction("EmployeeDetails");

            DataRow dr = dt.Rows[0];

            Employee emp = new Employee();
            emp.EmpId = (int)dr["EmpId"];
            emp.EmployeeName = (string)dr["EmployeeName"];
            emp.AgeOfEmployee = (int)dr["AgeOfEmployee"];
            emp.OfficeOfEmployee = (string)dr["OfficeOfEmployee"];

            return View(emp);

        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeletePost(int id)
        {
            //DB.ExecuteCommand($"UPDATE Employees SET EmployeeName = '{model.EmployeeName}', AgeOfEmployee = {model.AgeOfEmployee},OfficeOfEmployee = '{model.OfficeOfEmployee}' WHERE EmpId = {model.EmpId}");
            DB.ExecuteCommand($"DELETE FROM Employees WHERE EmpId = {id}");
            return RedirectToAction("EmployeeDetails");

        }

        [HttpGet]
        public ActionResult DisableButton()
        {
            return View();
        }

        [HttpPost]
        [ActionName("DisableButton")]
        public ActionResult PostDisableButton()
        {
            if (Request["Submit1"] != null)
            {
                if (TempData["Clicks"] == null)
                {
                    TempData["Clicks"] = 0;
                }

                TempData["Clicks"] = (int)TempData["Clicks"] + 1;

                //ViewBag.ClickCount = TempData["Clicks"];

                TempData.Keep("Clicks");
            }

            if ((int)TempData["Clicks"] == 5)
                ViewBag.DisableText = "disabled";

            TempData.Keep("Clicks");

            return View();
        }

        [HttpGet]
        public ActionResult TextGet()
        {

            return View();

        }
        [HttpPost]
        [ActionName("TextGet")]
        public ActionResult PostTextGet()
        {

            if (Request["btnSet"] != null)//btnSet is clicked
            {
                TempData["x"] = Request["Number"];
            }
            else if (Request["btnGet"] != null)
            {
                if (TempData["x"] != null)
                {
                    ViewBag.Msg = "You have selected: " + TempData["x"];
                }

                //TempData["Clicks"] = (int)TempData["Clicks"] + 1;

                //ViewBag.ClickCount = TempData["Clicks"];

                //TempData.Keep("Clicks");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Adder()
        {
            return View();

        }
        [HttpPost]
        [ActionName("Adder")]
        public ActionResult PostAdder()
        {

            if (Request["btnAdd"] != null)//btnSet is clicked
            {
                if (TempData["Sum"] == null)
                {
                    TempData["Sum"] = 0;
                }


                TempData["Sum"] = (int)TempData["Sum"] + int.Parse(Request["Number"]);

                TempData.Keep("Sum");
            }
            else if (Request["btnGet"] != null)
            {
                if (TempData["Sum"] != null)
                {
                    ViewBag.TotalSum = "Total summation: " + TempData["Sum"];
                }
                // return RedirectToAction("DisableButton");
            }
            return View();
        }

        const int totalDonationAmount = 2500;

        [HttpGet]
        public ActionResult DonateSum()
        {
            if (TempData["Donations"] == null)//first click (the list is absent)
            {
                TempData["Donations"] = new List<Donator>();//initialize an empty donation box
                ViewBag.RequiredAmount = totalDonationAmount;
            }
            TempData.Keep("Donations");
            ViewBag.TotalDonation = totalDonationAmount;

            List<Donator> lst = (List<Donator>)TempData["Donations"];
            int donationSum = totalDonation(lst);//see the new total
            int requiredAmount = totalDonationAmount - donationSum;//new required Amount

            ViewBag.DonationSum = donationSum;
            ViewBag.RequiredAmount = requiredAmount;

            return View();
        }


        [HttpPost]
        [ActionName("DonateSum")]
        public ActionResult PostDonateSum()
        {
            int requiredAmount = 0;

            int donationSum = 0;
            var lst = TempData["Donations"] as List<Donator>;
            ViewBag.TotalDonation = totalDonationAmount;
            if (Request["btnDonate"] != null)
            {
                bool match = false;

                for (int i = 0; i <= lst.Count() - 1; i++)
                {
                    if (lst[i].donatorName == Request["DonatorName"])
                    {
                        match = true;
                    }
                }

                if (match == false)
                {
                    Donator donator = new Donator();

                    donationSum = totalDonation(lst);//before donating, see the total sum
                    requiredAmount = totalDonationAmount - donationSum;


                    if (int.Parse(Request["DonatorAmount"]) <= requiredAmount)
                    {
                        donator.donatorName = Request["DonatorName"];
                        donator.donatorAmount = int.Parse(Request["DonatorAmount"]);
                        lst.Add(donator);
                    }
                }

                if (match == true)
                {
                    String donatedBefore = Request["DonatorName"] + " has already donated";
                    ViewBag.Message = donatedBefore;
                }

            }


            if (Request["btnDetails"] != null)
            {
                return RedirectToAction("DonationSummary");
            }

            donationSum = totalDonation(lst);//see the new total
            requiredAmount = totalDonationAmount - donationSum;//new required Amount

            ViewBag.DonationSum = donationSum;
            ViewBag.RequiredAmount = requiredAmount;

            TempData.Keep("Donations");
            return View();
        }


        public ActionResult DonationSummary()
        {
            if (TempData["Donations"] != null)
            {
                var donatorsList = TempData["Donations"] as List<Donator>;
                int totalAmount = 0;
                for (int i = 0; i <= donatorsList.Count - 1; i++)
                {
                    totalAmount = totalAmount + donatorsList[i].donatorAmount;

                }
                ViewBag.EmployeeList1 = donatorsList;
                TempData.Keep("Donations");
                ViewBag.TotalDonatedAmount = totalAmount;
                ViewBag.NumberOfDonators = donatorsList.Count;
                return View();
            }
            else
            {
                return Content("Donation is yet to begin!");
            }



        }
        public int totalDonation(List<Donator> donatorsList)
        {
            int totalDonation = 0;
            for (int i = 0; i <= donatorsList.Count - 1; i++)
            {
                totalDonation = totalDonation + donatorsList[i].donatorAmount;
            }
            return totalDonation;
        }

        [HttpGet]
        public ActionResult DisplayCircle()
        {

            return View();
        }

        [HttpPost]
        [ActionName("DisplayCircle")]
        public ActionResult PostDisPlayCircle()
        {
            if (Request["btnDraw"] != null)
            {
                string getCoordinate = Request["Coordinate"];
                string[] arr = getCoordinate.Split(',');
                int x = int.Parse(arr[0]);
                int y = int.Parse(arr[1]);
                int radius = int.Parse(Request["Radius"]);

                ViewBag.X = x;
                ViewBag.Y = y;
                ViewBag.Radius = radius;

            }
            return View();
        }
        public ActionResult TimeCount()
        {
            return View();
        }
        public ActionResult AnimateRect()
        {
            return View();
        }
        public ActionResult GroupOfButtons()
        {
            //check if you are already logged in
            //see if Session["UserId"] exists or not
            if (Session["UserId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("LogIn");
            }

        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("LogIn");
        }

        string conStr = "server=(local)\\sqlexpress;database=NazmulDB;Trusted_Connection=Yes";
        public ActionResult InifiniteRectangle()
        {
            return View();
        }

        public ActionResult LogIn()
        {
            return View();
        }


        public JsonResult ValidateLogin()
        {
            try
            {
                string userId = Request["Userid"];
                string password = Request["Password"];

                //validate data...

                SqlDataAdapter da = new SqlDataAdapter($"Select * from UserTable Where UserId = '{userId}' and [Password]='{password}'", conStr);
                DataTable dt = new DataTable();
                da.Fill(dt);

                //validate......

                System.Threading.Thread.Sleep(2000);


                if (dt.Rows.Count == 1)
                {
                    Session["UserId"] = userId;
                    Session["FullName"] = dt.Rows[0]["Name"].ToString();

                    return Json(new { Status = "Success", UserName = userId, FullName = Session["FullName"] });

                }
                else
                {
                    return Json(new { Status = "Failed", ErrorMessage = "Invalid Credentials" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Status = "Error", ErrorMessage = "Some Server Failure." + ex.Message });
            }

        }
        public JsonResult LogOutRemote()
        {
            Session.Abandon();
            return Json(new { Status = "Success" });
        }

            public ActionResult ShowPersonalDetails()
        {
            return View();
        }

        public JsonResult ValidPersonalDetails()
        {
            try
            {
                string userId = Request["Userid"];


                SqlDataAdapter da = new SqlDataAdapter($"Select * from UserTable Where UserId = '{userId}'", conStr);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count == 1)//aita bhujte hobe
                {
                    Session["UserId"] = userId;
                    Session["FullName"] = dt.Rows[0]["Name"].ToString();

                    return Json(new
                    {
                        Status = "Success",
                        UserName = userId,
                        FullName = Session["FullName"]
                    }
                    );
                }
                else
                {
                    return Json(new { Status = "Failed", ErrorMessage = "Record Not found" });
                }


            }
            catch (Exception)
            {
                return Json(new { Status = "Error", ErrorMessage = "Something went wrong in server!" });
            }



        }

        public ActionResult OpenDialogueBox()
        {
            return View();
        }

        //Model log in pop up
        public ActionResult PopUpLogInModel()
        {
            return View();
        }


        //[HttpGet]
        //public ActionResult LogInValidation()
        //{

        //    return View();
        //}

        [HttpPost]
        //[ActionName("LogInValidation")]
        public ActionResult LogInValidation()
        {
            if(Request["username"] == "Nazmul" && Request["password"] == "123456")
            {
                ViewBag.userName = Request["username"];
                ViewBag.password = Request["password"];
            }
            else
            {
                TempData["error"] = "This is an error message for "+ Request["username"];
                return RedirectToAction("PopUpLogInModel");
            }
            
            return View();
        }
    }
}