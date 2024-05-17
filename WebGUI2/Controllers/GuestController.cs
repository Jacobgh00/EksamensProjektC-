using BusinessLogic.BLL;
using DTO.Models;
using FerryManagementData.Models;
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
            var cars = carBLL.GetAllCarsForFerry(ferryId);
            var availableCars = cars.Where(c => c.Guests.Count < 5).ToList();
            ViewBag.Cars = new SelectList(availableCars, "CarID", "Numberplate");

            bool allCarsFull = !availableCars.Any();
            ViewBag.AllCarsFull = allCarsFull;

            var guest = new GuestDTO { FerryID = ferryId };
            return View(guest);
        }

        // POST: Guest/Add
        [HttpPost]
        public ActionResult Add(GuestDTO guest)
        {
            // Load cars
            var cars = carBLL.GetAllCarsForFerry(guest.FerryID);
            var availableCars = cars.Where(c => c.Guests.Count < 5).ToList();
            ViewBag.Cars = new SelectList(availableCars, "CarID", "Numberplate", guest.CarID);

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
            var availableCars = cars.Where(c => c.Guests.Count < 5 || c.CarID == guest.CarID).ToList();
            ViewBag.Cars = new SelectList(availableCars, "CarID", "Numberplate", guest.CarID);

            bool allCarsFull = !availableCars.Any();
            ViewBag.AllCarsFull = allCarsFull;

            return View(guest);
        }


        // POST: Guest/Edit/5
        [HttpPost]
        public ActionResult Edit(GuestDTO guest)
        {

            var cars = carBLL.GetAllCarsForFerry(guest.FerryID);
            var availableCars = cars.Where(c => c.Guests.Count < 5).ToList();
            ViewBag.Cars = new SelectList(availableCars, "CarID", "Numberplate");

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
        [ValidateAntiForgeryToken]
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
                return RedirectToAction("Details", "Ferry", new { id = guest.FerryID });
            }
        }

    }
}