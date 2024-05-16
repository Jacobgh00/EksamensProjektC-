using BusinessLogic.BLL;
using DTO.Models;
using FerryManagementData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace WebGUI.Controllers
{
    public class FerryController : Controller
    {
        private FerryBLL ferryBLL = new FerryBLL();
        private CarBLL carBLL = new CarBLL();
        private GuestBLL guestBLL = new GuestBLL();

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
                return HttpNotFound("Ferry not found");

            return View(ferry);
        }

        //Get: Ferry/Create
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
                ModelState.AddModelError("", "Failed to create ferry: " + ex.Message);
            }
            return View(ferry);
        }

        // GET: Ferry/Edit/5
        public ActionResult Edit(int id)
        {
            var ferry = ferryBLL.GetFerry(id);
            if (ferry == null)
            {
                return HttpNotFound("Ferry not found");
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
                ModelState.AddModelError("", "Failed to edit ferry: " + ex.Message);
            }
            return View(ferry);
        }

        // GET: Ferry/Delete/5
        public ActionResult Delete(int id)
        {
            var ferry = ferryBLL.GetFerry(id);
            if (ferry == null)
            {
                return HttpNotFound("Ferry not found");
            }
            return View(ferry);
        }

        // POST: Ferry/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                ferryBLL.DeleteFerry(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Failed to delete ferry: " + ex.Message);
            }
            return View();
        }

    }
}