using BusinessLogic.BLL;
using DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebGUI2.Controllers
{
    public class CarController : Controller
    {
        private CarBLL carBLL = new CarBLL();
        private FerryBLL ferryBLL = new FerryBLL();
        private GuestBLL guestBLL = new GuestBLL();
        // GET: Car/Add
        public ActionResult Add(int ferryId)
        {
            ViewBag.FerryId = ferryId;
            return View(new CarDTO { FerryID = ferryId });
        }

        // POST: Car/Add
        [HttpPost]
        public ActionResult Add(int ferryId, CarDTO car)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    carBLL.AddCarToFerry(ferryId, car);
                    return RedirectToAction("Details", "Ferry", new { id = ferryId });
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error creating car: " + ex.Message);
            }

            ViewBag.FerryId = ferryId;
            return View(car);
        }

        // GET: Car/Edit/5
        public ActionResult Edit(int id)
        {
            var car = carBLL.GetCar(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // POST: Car/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, CarDTO car)
        {
            
            try
            {
                if (ModelState.IsValid)
                {
                    carBLL.UpdateCar(car);
                    return RedirectToAction("Details", "Ferry", new { id = car.FerryID });
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error updating car: " + ex.ToString());
            }

            return View(car);
        }

        // GET: Car/Delete/5
        public ActionResult Delete(int id)
        {
            var car = carBLL.GetCar(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }


        // POST: Car/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CarDTO car = carBLL.GetCar(id);
            if (car == null)
            {
                return HttpNotFound();
            }

            try
            {
                foreach (var guest in car.Guests.ToList())
                {
                    guestBLL.DeleteGuest(guest.GuestID);
                }

                carBLL.DeleteCar(id);

                return RedirectToAction("Details", "Ferry", new { id = car.FerryID });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error deleting car: " + ex.Message);
                return View(car);
            }
        }
    }
}