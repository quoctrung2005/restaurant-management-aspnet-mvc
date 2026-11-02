using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DA_LTW.Models;
using System.Data.Entity;

namespace DA_LTW.Controllers
{
    public class NhanVienController : Controller
    {
        QL_NHAHANGEntities data = new QL_NHAHANGEntities();

        // GET: NhanVien
        public ActionResult Index()
        {
            List<NHANVIEN> ds = data.NHANVIENs.ToList();
            ViewBag.ActiveMenu = "NhanVien";
            return View(ds);
        }

        // GET: Details
        public ActionResult Details(string id)
        {
            var nv = data.NHANVIENs.Find(id);
            if (nv == null)
            {
                return HttpNotFound();
            }
            return View(nv);
        }

        // GET: Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NHANVIEN nv)
        {
            if (ModelState.IsValid)
            {
                data.NHANVIENs.Add(nv);
                data.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nv);
        }

        // GET: Edit
        public ActionResult Edit(string id)
        {
            var nv = data.NHANVIENs.Find(id);
            if (nv == null)
            {
                return HttpNotFound();
            }
            return View(nv);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(NHANVIEN nv)
        {
            if (ModelState.IsValid)
            {
                data.Entry(nv).State = EntityState.Modified;
                data.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nv);
        }

        // GET: Delete
        public ActionResult Delete(string id)
        {
            var nv = data.NHANVIENs.Find(id);
            if (nv == null)
            {
                return HttpNotFound();
            }
            return View(nv);
        }

        // POST: Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var nv = data.NHANVIENs.Find(id);
            if (nv == null)
            {
                return HttpNotFound();
            }

            data.NHANVIENs.Remove(nv);
            data.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                data.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
