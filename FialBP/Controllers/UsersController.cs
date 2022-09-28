using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FialBP.Models;

namespace FialBP.Controllers
{
    public class UsersController : Controller
    {
        private Model1 db = new Model1();

        // GET: Users
        public async Task<ActionResult> Index()
        {
            return View(await db.Users.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "UserID,Username,UserPass,UserType")] User tblUser)
        {
            if (ModelState.IsValid)
            {
                var userId = db.Users.Max(o => o.UserID) + 1;
                var oUser = new User();
                oUser.UserID = userId;
                oUser.Username = tblUser.Username;
                oUser.UserPass = tblUser.UserPass;
                oUser.UserType = tblUser.UserType;
                db.Users.Add(oUser);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tblUser);
        }

        // GET: Users/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "UserID,Username,UserPass,UserType")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            User user = await db.Users.FindAsync(id);
            db.Users.Remove(user);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public async Task<ActionResult> UserRole(int userId)
        {
            var listTblUserRole = await db.UserRoles.Where(o => o.UserID == userId).ToListAsync();
            var oTblUser = await db.Users.Where(o => o.UserID == userId).FirstOrDefaultAsync();
            TempData["Username"] = oTblUser.Username;

            #region create menu at view page
            var listUserRole = new List<UserRole>();

            #region Buses
            var oUserRole = listTblUserRole.Where(o => o.UserID == userId && o.PageName == "Buses").FirstOrDefault();
            if (oUserRole == null)
            {
                oUserRole = new UserRole();
                oUserRole.UserID = userId;
                oUserRole.PageName = "Buses"; // controller name
                oUserRole.IsCreate = false;
                oUserRole.IsRead = false;
                oUserRole.IsUpdate = false;
                oUserRole.IsDelete = false;

                listUserRole.Add(oUserRole);
            }
            else
            {
                listUserRole.Add(oUserRole);
            }
            #endregion

            #region emp
            oUserRole = listTblUserRole.Where(o => o.UserID == userId && o.PageName == "Employee").FirstOrDefault();
            if (oUserRole == null)
            {
                oUserRole = new UserRole();
                oUserRole.UserID = userId;
                oUserRole.PageName = "Employee";
                oUserRole.IsCreate = false;
                oUserRole.IsRead = false;
                oUserRole.IsUpdate = false;
                oUserRole.IsDelete = false;

                listUserRole.Add(oUserRole);
            }
            else
            {
                listUserRole.Add(oUserRole);
            }
            #endregion

            #endregion

            return View(listUserRole);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UserRole(UserRole[] UserRoles)
        {
            var userRoleIdMax = db.UserRoles.Max(o => (int?)o.UserRoleID) ?? 0;
            for (var i = 0; i < UserRoles.Length; i++)
            {
                int userRoleId = UserRoles[i].UserRoleID;
                var oTblUserRole = await db.UserRoles.Where(o => o.UserRoleID == userRoleId).FirstOrDefaultAsync();
                if (oTblUserRole == null) // insert
                {
                    oTblUserRole = new UserRole();
                    oTblUserRole.UserRoleID = ++userRoleIdMax;
                    oTblUserRole.UserID = UserRoles[i].UserID;
                    oTblUserRole.PageName = UserRoles[i].PageName;
                    oTblUserRole.IsCreate = UserRoles[i].IsCreate;
                    oTblUserRole.IsRead = UserRoles[i].IsRead;
                    oTblUserRole.IsUpdate = UserRoles[i].IsUpdate;
                    oTblUserRole.IsDelete = UserRoles[i].IsDelete;
                    db.UserRoles.Add(oTblUserRole);
                }
                else // update
                {
                    oTblUserRole.IsCreate = UserRoles[i].IsCreate;
                    oTblUserRole.IsRead = UserRoles[i].IsRead;
                    oTblUserRole.IsUpdate = UserRoles[i].IsUpdate;
                    oTblUserRole.IsDelete = UserRoles[i].IsDelete;
                }
            }
            await db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}

