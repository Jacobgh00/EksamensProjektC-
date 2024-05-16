using BusinessLogic.BLL;
using DTO.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebGUI.Controllers
{
    public class CarController : Controller
    {

        private CarBLL carBLL = new CarBLL();
        private GuestBLL guestBLL = new GuestBLL();
        private FerryBLL ferryBLL = new FerryBLL();

        // GET: Car/AddCar/1
        public ActionResult AddCar(int ferryId)
        {
            ViewBag.FerryId = ferryId;
            ViewBag.Guests = new SelectList(guestBLL.GetAllGuests(ferryId), "GuestId", "Name");
           
            return View(new CarDTO { FerryId = ferryId });
        }

        /*
        // POST: Car/AddCar
        [HttpPost]
        public ActionResult AddCar(CarDTO car, List<int> passengerIds)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    carBLL.AddCarToFerry(car.FerryId, car, passengerIds);
                    return RedirectToAction("Details", "Ferry", new { id = car.FerryId });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Failed to add car: " + ex.Message);
                }
            }

            ViewBag.FerryId = car.FerryId;
            ViewBag.Guests = new SelectList(guestBLL.GetAllGuests(car.FerryId), "GuestId", "Name");


            return View(car);
        }

        */

        [HttpPost]
        public ActionResult AddCar(CarDTO car, List<int> passengerIds)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Ensure the ferry exists
                    var ferry = ferryBLL.GetFerry(car.FerryId);
                    if (ferry == null)
                    {
                        ModelState.AddModelError("", "Ferry does not exist.");
                        PrepareCarViewData(car.FerryId);
                        return View(car);
                    }

                    // Validate and fetch passengers; ensure they all belong to the specified ferry
                    var validPassengers = guestBLL.GetAllGuests(car.FerryId)
                                                  .Where(g => passengerIds.Contains(g.GuestId))
                                                  .ToList();

                    if (validPassengers.Count != passengerIds.Count)
                    {
                        ModelState.AddModelError("", "One or more passengers are invalid for this ferry.");
                        PrepareCarViewData(car.FerryId);
                        return View(car);
                    }

                    // Add the car to the ferry, including its passengers
                    carBLL.AddCarToFerry(car.FerryId, car, passengerIds);
                    return RedirectToAction("Details", "Ferry", new { id = car.FerryId });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Failed to add car: " + ex.Message);
                }
            }

            PrepareCarViewData(car.FerryId);
            return View(car);
        }

        private void PrepareCarViewData(int ferryId)
        {
            ViewBag.FerryId = ferryId;
            ViewBag.Guests = new SelectList(guestBLL.GetAllGuests(ferryId), "GuestId", "Name");
        }




        // GET: Car/Edit/5
        public ActionResult Edit(int id)
        {
            var car = carBLL.GetCar(id);
            
            ViewBag.Guests = new SelectList(guestBLL.GetAllGuests(car.FerryId), "GuestId", "Name");
            ViewBag.SelectedPassengerIds = new HashSet<int>(car.Guests.Select(p => p.GuestId)); //måske ikke nødvendig
            return View(car);
        }

        // POST: Car/Edit/5
        [HttpPost]
        public ActionResult Edit(CarDTO car, List<int> passengerIds)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    carBLL.UpdateCar(car);
                    return RedirectToAction("Details", "Ferry", new { id = car.FerryId });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Failed to edit car: " + ex.Message);
                }
            }

            ViewBag.Guests = new SelectList(guestBLL.GetAllGuests(car.FerryId), "GuestId", "Name");
            ViewBag.SelectedPassengerIds = new HashSet<int>(passengerIds ?? new List<int>()); //måske ikke nødvendig, bemærk også ekstra parameter i metoden
            return View(car);
        }

        // GET: Car/Delete/5
        public ActionResult Delete(int id)
        {
            var car = carBLL.GetCar(id);
   
            if (car == null)
            {
                return HttpNotFound("Car not found or driver does not exist.");
            }
            return View(car);
        }

        // POST: Car/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                carBLL.DeleteCar(id);
                return RedirectToAction("Index", "Ferry");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Failed to delete car: " + ex.Message);
            }
            return View();
        }

    }
}