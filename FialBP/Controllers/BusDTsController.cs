using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FialBP.Models;
using FialBP.ViewModel;

namespace FialBP.Controllers
{
    public class BusDTsController : Controller
    {
        //private Model1 db = new Model1();

        // GET: BusDTs
        public ActionResult Index(int? id)
        {
            var ctx = new Model1();
            var categoryWiseBusQuantity = from p in ctx.BusDTs
                                          group p by p.CategoryId into g
                                          select new
                                          {
                                              g.FirstOrDefault().CategoryId,
                                              Qty = g.Sum(s => s.Quantity)
                                          };
            var listCategory = (from c in ctx.Categories
                                join cwpq in categoryWiseBusQuantity on c.CategoryId equals cwpq.CategoryId
                                select new VmCategory
                                {
                                    CategoryName = c.CategoryName,
                                    CategoryId = (int)cwpq.CategoryId,
                                    Quantity = cwpq.Qty
                                }).ToList();
            var listBus = (from p in ctx.BusDTs
                           join c in ctx.Categories on p.CategoryId equals c.CategoryId
                           where p.CategoryId == id
                           select new VmBus
                           {
                               CategoryId = (int)p.CategoryId,
                               CategoryName = c.CategoryName,
                               BookingDate = p.BookingDate,
                               ImagePath = p.ImagePath,
                               Price = p.Price,
                               BusDTId = p.BusDTId,
                               BusDTName = p.BusDTName,
                               Quantity = p.Quantity
                           }).ToList();
            var oCategoryWiseBus = new VmCategoryWiseBus();
            oCategoryWiseBus.CategoryList = listCategory;
            oCategoryWiseBus.BusList = listBus;//change korun
            oCategoryWiseBus.CategoryId = listBus.Count > 0 ? listBus[0].CategoryId : 0;
            oCategoryWiseBus.CategoryName = listBus.Count > 0 ? listBus[0].CategoryName : "";

            return View(oCategoryWiseBus);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            //category dropdown
            var oVmBustCategory = new VmBusCategory();
            var ctx = new Model1();
            oVmBustCategory.CategoryList = ctx.Categories.ToList();
            return View(oVmBustCategory);
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(Category cat, string[] BusDTName, decimal[] Price, int[] Quantity, DateTime[] BookingDate, HttpPostedFileBase[] imgFile)
        {
            var ctx = new Model1();
            var oCategory = (from c in ctx.Categories where c.CategoryName == cat.CategoryName.Trim() select c).FirstOrDefault();
            if (oCategory == null)
            {
                ctx.Categories.Add(cat);
                ctx.SaveChanges();
            }
            else
            {
                cat.CategoryId = oCategory.CategoryId;
            }
            var listBus = new List<BusDT>();
            for (int i = 0; i < BusDTName.Length; i++)
            {
                string imgPath = "";
                if (imgFile[i] != null && imgFile[i].ContentLength > 0)
                {
                    var fileName = Path.GetFileName(imgFile[i].FileName);
                    string fileLocation = Path.Combine(
                        Server.MapPath("~/uploads"), fileName);
                    imgFile[i].SaveAs(fileLocation);

                    imgPath = "/uploads/" + imgFile[i].FileName;
                }
                var newProduct = new BusDT();
                newProduct.BusDTName = BusDTName[i];
                newProduct.Quantity = Quantity[i];
                newProduct.Price = Price[i];
                newProduct.BookingDate = BookingDate[i];
                newProduct.ImagePath = imgPath;
                newProduct.Quantity = Quantity[i];
                newProduct.CategoryId = cat.CategoryId;
                listBus.Add(newProduct);
            }
            ctx.BusDTs.AddRange(listBus);
            ctx.SaveChanges();
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            var ctx = new Model1();
            var oBus = (from p in ctx.BusDTs
                        join c in ctx.Categories on p.CategoryId equals c.CategoryId
                        where p.BusDTId == id
                        select new VmBus
                        {
                            CategoryId = (int)p.CategoryId,
                            CategoryName = c.CategoryName,
                            BookingDate = p.BookingDate,
                            ImagePath = p.ImagePath,
                            Price = p.Price,
                            BusDTId = p.BusDTId,
                            BusDTName = p.BusDTName,
                            Quantity = p.Quantity
                        }).FirstOrDefault();
            oBus.CategoryList = ctx.Categories.ToList();
            return View(oBus);
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(VmBus model)
        {
            var ctx = new Model1();

            string imgPath = "";
            if (model.ImgFile != null && model.ImgFile.ContentLength > 0)
            {
                var fileName = Path.GetFileName(model.ImgFile.FileName);
                string fileLocation = Path.Combine(
                    Server.MapPath("~/uploads"), fileName);
                model.ImgFile.SaveAs(fileLocation);

                imgPath = "/uploads/" + model.ImgFile.FileName;
            }

            var oBus = ctx.BusDTs.Where(w => w.BusDTId == model.BusDTId).FirstOrDefault();
            if (oBus != null)
            {
                oBus.BusDTName = model.BusDTName;
                oBus.Quantity = model.Quantity;
                oBus.Price = model.Price;
                oBus.BookingDate = model.BookingDate;
                oBus.CategoryId = model.CategoryId;
                if (!string.IsNullOrEmpty(imgPath))
                {
                    var fileName = Path.GetFileName(oBus.ImagePath);
                    string fileLocation = Path.Combine(Server.MapPath("~/uploads"), fileName);
                    if (System.IO.File.Exists(fileLocation))
                    {
                        System.IO.File.Delete(fileLocation);
                    }
                }
                oBus.ImagePath = imgPath == "" ? oBus.ImagePath : imgPath;

                ctx.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        public ActionResult EditMultiple(int? id)
        {
            var ctx = new Model1();
            var oCategoryWiseBus = new VmCategoryWiseBus();
            var listBus = (from p in ctx.BusDTs
                           join c in ctx.Categories on p.CategoryId equals c.CategoryId
                           where p.CategoryId == id
                           select new VmBus
                           {
                               CategoryId = (int)p.CategoryId,
                               CategoryName = c.CategoryName,
                               BookingDate = p.BookingDate,
                               ImagePath = p.ImagePath,
                               Price = p.Price,
                               BusDTId = p.BusDTId,
                               BusDTName = p.BusDTName,
                               Quantity = p.Quantity
                           }).ToList();
            oCategoryWiseBus.BusList = listBus;

            oCategoryWiseBus.CategoryList = (from c in ctx.Categories
                                             select new VmCategory
                                             {
                                                 CategoryId = c.CategoryId,
                                                 CategoryName = c.CategoryName
                                             }).ToList();
            oCategoryWiseBus.CategoryId = listBus.Count > 0 ? listBus[0].CategoryId : 0;
            oCategoryWiseBus.CategoryName = listBus.Count > 0 ? listBus[0].CategoryName : "";
            return View(oCategoryWiseBus);
        }

        [HttpPost]
        public ActionResult EditMultiple(Category model, int[] BusDTId, string[] BusDTName, decimal[] Price, int[] Quantity, DateTime[] BookingDate, HttpPostedFileBase[] imgFile)
        {
            var ctx = new Model1();
            var listBus = new List<BusDT>();
            for (int i = 0; i < BusDTName.Length; i++)
            {
                if (BusDTId[i] > 0)
                {
                    string imgPath = "";
                    if (imgFile[i] != null && imgFile[i].ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(imgFile[i].FileName);
                        string fileLocation = Path.Combine(
                            Server.MapPath("~/uploads"), fileName);
                        imgFile[i].SaveAs(fileLocation);

                        imgPath = "/uploads/" + imgFile[i].FileName;
                    }
                    int pid = BusDTId[i];
                    var oBus = ctx.BusDTs.Where(w => w.BusDTId == pid).FirstOrDefault();
                    if (oBus != null)
                    {
                        oBus.BusDTName = BusDTName[i];
                        oBus.Quantity = Quantity[i];
                        oBus.Price = Price[i];
                        oBus.BookingDate = BookingDate[i];
                        oBus.CategoryId = model.CategoryId;
                        if (!string.IsNullOrEmpty(imgPath))
                        {
                            var fileName = Path.GetFileName(oBus.ImagePath);
                            string fileLocation = Path.Combine(Server.MapPath("~/uploads"), fileName);
                            if (System.IO.File.Exists(fileLocation))
                            {
                                System.IO.File.Delete(fileLocation);
                            }
                        }
                        oBus.ImagePath = imgPath == "" ? oBus.ImagePath : imgPath;
                        ctx.SaveChanges();
                    }
                }
                else if (!string.IsNullOrEmpty(BusDTName[i]))
                {
                    string imgPath = "";
                    if (imgFile[i] != null && imgFile[i].ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(imgFile[i].FileName);
                        string fileLocation = Path.Combine(
                            Server.MapPath("~/uploads"), fileName);
                        imgFile[i].SaveAs(fileLocation);

                        imgPath = "/uploads/" + imgFile[i].FileName;
                    }

                    var newProduct = new BusDT();
                    newProduct.BusDTName = BusDTName[i];
                    newProduct.Quantity = Quantity[i];
                    newProduct.Price = Price[i];
                    newProduct.BookingDate = BookingDate[i];
                    newProduct.ImagePath = imgPath;
                    newProduct.Quantity = Quantity[i];
                    newProduct.CategoryId = model.CategoryId;
                    ctx.BusDTs.Add(newProduct);
                    ctx.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            var ctx = new Model1();
            var oBus = ctx.BusDTs.Where(p => p.BusDTId == id).FirstOrDefault();
            if (oBus != null)
            {
                ctx.BusDTs.Remove(oBus);
                ctx.SaveChanges();

                var fileName = Path.GetFileName(oBus.ImagePath);
                string fileLocation = Path.Combine(
                    Server.MapPath("~/uploads"), fileName);

                if (System.IO.File.Exists(fileLocation))
                {

                    System.IO.File.Delete(fileLocation);
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult DeleteMultiple(int id)
        {
            var ctx = new Model1();
            var listBus = ctx.BusDTs.Where(p => p.CategoryId == id).ToList();
            foreach (var oBus in listBus)
            {
                if (oBus != null)
                {
                    ctx.BusDTs.Remove(oBus);
                    ctx.SaveChanges();

                    var fileName = Path.GetFileName(oBus.ImagePath);
                    string fileLocation = Path.Combine(
                        Server.MapPath("~/uploads"), fileName);
                    if (System.IO.File.Exists(fileLocation))
                    {

                        System.IO.File.Delete(fileLocation);
                    }
                }
            }

            var oCategory = ctx.Categories.Where(c => c.CategoryId == id).FirstOrDefault();
            ctx.Categories.Remove(oCategory);
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
