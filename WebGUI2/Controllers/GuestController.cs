using BusinessLogic.BLL;
using DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebGUI2.Controllers
{
    public class GuestController : Controller
    {

        private GuestBLL guestBLL = new GuestBLL();
        private FerryBLL ferryBLL = new FerryBLL();
        private CarBLL carBLL = new CarBLL();

        // GET: Guest/Add
        public ActionResult Add(int ferryId)
        {
            // Preparing the dropdown for Cars based on the selected Ferry
            var cars = carBLL.GetAllCarsForFerry(ferryId);
            ViewBag.Cars = new SelectList(cars, "CarID", "Numberplate");

            // Setting default values
            var guest = new GuestDTO { FerryID = ferryId };
            return View(guest);
        }

        // POST: Guest/Add
        [HttpPost]
        public ActionResult Add(GuestDTO guest)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    guestBLL.AddGuestToFerry(guest.FerryID, guest);
                    return RedirectToAction("Details", "Ferry", new { id = guest.FerryID });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error creating guest: " + ex.Message);
                }
            }

            // Preserve the dropdown list in case of an error
            var cars = carBLL.GetAllCarsForFerry(guest.FerryID);
            ViewBag.Cars = new SelectList(cars, "CarID", "Numberplate", guest.CarID);

            return View(guest);
        }

        // GET: Guest/Edit/5
        public ActionResult Edit(int id)
        {
            var guest = guestBLL.GetGuest(id);
            if (guest == null)
            {
                return HttpNotFound();
            }

            var cars = carBLL.GetAllCarsForFerry(guest.FerryID);
            ViewBag.Cars = new SelectList(cars, "CarID", "Numberplate", guest.CarID);

            return View(guest);
        }


        // POST: Guest/Edit/5
        [HttpPost]
        public ActionResult Edit(GuestDTO guest)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    guestBLL.UpdateGuest(guest);
                    return RedirectToAction("Details", "Ferry", new { id = guest.FerryID });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error updating guest: " + ex.Message);
                }
            }

            // Preserve the dropdown list in case of an error
            var cars = carBLL.GetAllCarsForFerry(guest.FerryID);
            ViewBag.Cars = new SelectList(cars, "CarID", "Numberplate", guest.CarID);

            return View(guest);
        }

        // GET: Guest/Delete/5
        public ActionResult Delete(int id)
        {
            var guest = guestBLL.GetGuest(id);
            if (guest == null)
            {
                return HttpNotFound();
            }
            return View(guest);
        }

        // POST: Guest/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var guest = guestBLL.GetGuest(id);
            if (guest == null)
            {
                return HttpNotFound();
            }
            try
            {
                guestBLL.DeleteGuest(id);
                return RedirectToAction("Details", "Ferry", new { id = guest.FerryID });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error deleting guest: " + ex.Message);
                return View("Delete", guest);
            }
        }

    }
}