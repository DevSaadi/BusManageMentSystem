using CrystalDecisions.CrystalReports.Engine;
using FialBP.Models;
using FialBP.ViewModel;
using FinalProject.Filter;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace FialBP.Controllers
{

    public class EmployeeController : Controller
    {
        [HttpGet]
        public ActionResult Emp(int? id)
        {
            var db = new Model1();
            var owe = (from o in db.Staffs where o.StaffId == id select o).FirstOrDefault();
            owe = owe == null ? new Staff() : owe;
            ViewData["List"] = db.Staffs.ToList();
            ViewBag.TitleID = new SelectList(db.Titles, "TitleID", "TitleName");
            return View(owe);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Emp(Staff model, HttpPostedFileBase img)
        {
            string fileName = "";
            if (img != null)
            {
                fileName = img.FileName;
                string image = System.IO.Path.GetFileName(img.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/Images"), image);
                img.SaveAs(path);
            }

            var db = new Model1();
            var oStaff = db.Staffs.Find(model.StaffId);
            ViewBag.TitleID = new SelectList(db.Titles, "TitleID", "TitleName");
            if (oStaff == null)
            {
                model.Picture = "/Images/" + fileName;
                db.Staffs.Add(model);
                ViewBag.Message = "Saves successfully.";
            }
            else
            {
                oStaff.StaffName = model.StaffName;
                oStaff.Gender = model.Gender;
                oStaff.TitleID = model.TitleID;
                oStaff.JoinDate = model.JoinDate;
                oStaff.Address = model.Address;
                oStaff.Age = model.Age;
                oStaff.Email = model.Email;
                oStaff.Salary = model.Salary;
                oStaff.Picture = "/Images/" + fileName;

                ViewBag.Message = "Updated successfully.";
            }
            db.SaveChanges();
            ViewData["List"] = db.Staffs.ToList();
            return RedirectToAction("Emp");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var db = new Model1();
            var oStaff = (from o in db.Staffs where o.StaffId == id select o).FirstOrDefault();
            if (oStaff != null)
            {
                db.Staffs.Remove(oStaff);
                db.SaveChanges();
            }
            return RedirectToAction("Emp");
        }
        //Report
        public ActionResult PrintList()
        {
            var db = new Model1();
            var StaffList = (from x in db.Staffs
                             join y in db.Titles on x.StaffId equals y.TitleID
                             select new VmEp
                             {
                                 StaffId = x.StaffId,
                                 StaffName = x.StaffName,
                                 Gender = x.Gender,
                                 TitleName = y.TitleName,
                                 JoinDate = x.JoinDate,
                                 Address = x.Address,
                                 Age = x.Age,
                                 Email = x.Email,
                                 Salary = x.Salary,
                                 Picture = "http://localhost:44329" + x.Picture
                             }).ToList();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Report"), "CrystalReport1.rpt"));
            rd.SetDataSource(ListToDataTable(StaffList));
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "EmployeeList.pdf");
            }
            catch
            {
                throw;
            }
        }
        //Report
        public ActionResult PrintSingle(int id)
        {
            var db = new Model1();
            var StaffList = (from x in db.Staffs
                             join y in db.Titles on x.StaffId equals y.TitleID
                             where x.StaffId == id
                             select new VmEp
                             {
                                 StaffId = x.StaffId,
                                 StaffName = x.StaffName,
                                 Gender = x.Gender,
                                 TitleName = y.TitleName,
                                 JoinDate = x.JoinDate,
                                 Address = x.Address,
                                 Age = x.Age,
                                 Email = x.Email,
                                 Salary = x.Salary,
                                 Picture = "http://localhost:44329" + x.Picture


                             }).ToList();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Report"), "CrystalReport2.rpt"));
            rd.SetDataSource(ListToDataTable(StaffList));
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "Individual.pdf");
            }
            catch
            {
                throw;
            }
        }
        private DataTable ListToDataTable<T>(List<T> items)
        {
            DataTable datatable = new DataTable(typeof(T).Name);
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                datatable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    values[i] = Props[i].GetValue(item, null);
                }
                datatable.Rows.Add(values);
            }
            return datatable;
        }
    }
}
