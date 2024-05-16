using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogic.BLL;
using DTO.Models;

namespace WebGUI2.Controllers
{
    public class FerryController : Controller
    {

        private FerryBLL ferryBLL = new FerryBLL();

        // GET: Ferry
        public ActionResult Index()
        {

            var ferries = ferryBLL.GetAllFerries();
            return View(ferries);
        }

        // GET: Ferry/Details/5
        public ActionResult Details(int id)
        {
            var ferry = ferryBLL.GetFerry(id);
            if (ferry == null)
            {
                return HttpNotFound();
            }
            return View(ferry);
        }

        //POST: Ferry/Add



        // GET: Ferry/Edit/5
        public ActionResult Edit(int id)
        {
            var ferry = ferryBLL.GetFerry(id);
            if (ferry == null)
            {
                return HttpNotFound();
            }
            return View(ferry);
        }

        // POST: Ferry/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FerryDTO ferry)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ferryBLL.UpdateFerry(ferry);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error updating ferry: " + ex.Message);
            }
            return View(ferry);
        }

       
        // POST: Ferry/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            try
            {
                ferryBLL.DeleteFerry(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                
                TempData["Error"] = "Error deleting ferry: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        // GET: Ferry/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ferry/Create
        [HttpPost]
        public ActionResult Create(FerryDTO ferry)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ferryBLL.AddFerry(ferry);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error creating ferry: " + ex.Message);
            }
            return View(ferry);
        }
    }
}