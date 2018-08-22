using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Future_is_mine.SQLOperations;
using Future_is_mine.Models;
using System.Data;

namespace Future_is_mine.Controllers
{
    public class EmployeeController : Controller
    {
        Employee_CURD _Employee_CURD;
        private DataSet _DataSet;
        string EmployeeEmail, Query, message;
        Employee _Employee;


        public JsonResult CheckCredentials(Employee _Employee)
        {
            _DataSet = new DataSet();
            string message, Query;
            Query = "select email,password from candidates where email='" + _Employee.Email + "' and password='" + _Employee.password + "'";
            _DataSet = _Employee_CURD.GetDetails(Query);

            if (_DataSet.Tables["Table"].Rows.Count <= 0)
            {
                message = "Invalid Credentials";
                return new JsonResult { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else {
                Session["UserEmail"] = _DataSet.Tables["Table"].Rows[0]["email"].ToString();
                message = "success";
                return new JsonResult { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            }
        }

        [HttpPost]
        public JsonResult InsertEmployee(Employee _Employee)
        {

            string strQuery = "insert into [Candidates] values('" + _Employee.EmployeeId + "','" + _Employee.FirstName+ "','" + _Employee.LastName + "','" + _Employee.City + "','" + _Employee.Email + "'," + _Employee.password + ",'" + _Employee.classification + "','"+_Employee.country+ "','"+_Employee.Experience + "','" + _Employee.Active + "','"+_Employee.verified+"','" + _Employee.EmailVerified + "','" + DateTime.Now.Date + "','" + _Employee.profilesummary + "','" + _Employee.CurrentRole + "')";
            string Result = _Employee_CURD.AddEmployee(strQuery);
            return new JsonResult { Data = Result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        // GET: Employee
        [HttpGet]
        public ActionResult Login() {
            return View();
        }

        public ActionResult EmployeeProfile() {
            //if (Session["UserEmail"] != null)
            //{
            //    return View();
            //}
            //else
            //{
            //    return RedirectToAction("Index", "Login");

            //}
            return View();
        }

        [HttpGet]
        public JsonResult GetEmployeeDetails()
        {
            EmployeeEmail = Session["UserEmail"].ToString();

             _Employee = new Employee();
            // string CurrentRole, Firstname, Lastname, Email,City;

            Query = "select * from candidates where email='" + EmployeeEmail + "'";
            _Employee_CURD = new Employee_CURD();

            _DataSet = _Employee_CURD.GetDetails(Query);
            var data = new
            {
                About = _DataSet.Tables["Table"].Rows[0]["profilesummary"].ToString(),
                CurrentRole = _DataSet.Tables["Table"].Rows[0]["CurrentRole"].ToString(),
                Firstname = _DataSet.Tables["Table"].Rows[0]["FirstName"].ToString(),
                Lastname = _DataSet.Tables["Table"].Rows[0]["LastName"].ToString(),
                Email = _DataSet.Tables["Table"].Rows[0]["Email"].ToString(),
                City = _DataSet.Tables["Table"].Rows[0]["City"].ToString(),
            };
            if (_DataSet.Tables["Table"].Rows.Count <= 0)
            {
                message = "Invalid Credentials";
                return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else {

                message = "success";
                return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }


        }

        public JsonResult UpdateEmployeeDetails(string About, string CurrentRole, string Firstname, string Lastname, string email, string city)
        {
            bool result;
            _Employee = new Employee();
            Query = "update candidates set profilesummary='" + About + "',CurrentRole='" + CurrentRole + "',firstname='" + Firstname + "',lastname='" + Lastname + "',email='" + email + "',city='" + city + "'";
            _Employee_CURD = new Employee_CURD();
            result = _Employee_CURD.UpdateData(Query);

            if (result == true)
            {
                message = "Data Updated seccesfully";
                return new JsonResult { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            }
            else {
                message = "Something Went Wrong";
                return new JsonResult { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }


        [AllowAnonymous]
        public JsonResult CheckEmail(string EmployeeEmail)
        {
            Query = "select email from candidates where email='" + EmployeeEmail + "'";
            _Employee_CURD = new Employee_CURD();
            _DataSet = _Employee_CURD.GetDetails(Query);

            if (_DataSet.Tables["Table"].Rows.Count <= 0)
            {
                message = "Fail";
                return new JsonResult { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else {

                message = "success";
                return new JsonResult { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }


    }

}