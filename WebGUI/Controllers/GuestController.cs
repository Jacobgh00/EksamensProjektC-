using BusinessLogic.BLL;
using DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebGUI.Controllers
{
    public class GuestController : Controller
    {

        private GuestBLL guestBLL = new GuestBLL();
        private FerryBLL ferryBLL = new FerryBLL();

        // GET: Guest/AddGuest/ferryId
        public ActionResult AddGuest(int? ferryId)
        {
            var model = new GuestDTO();
            if (ferryId.HasValue)
            {
                ViewBag.FerryId = ferryId.Value;
                model.FerryId = ferryId.Value;
            }
            return View(model);
        }

        // POST: Guest/AddGuest
        [HttpPost]
        public ActionResult AddGuest(GuestDTO guest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    guestBLL.AddGuestToFerry(guest.FerryId, guest);
                    if (guest.FerryId != null)
                        return RedirectToAction("Details", "Ferry", new { id = guest.FerryId});
                    else
                        return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error adding guest: " + ex.Message);
            }
            return View(guest);
        }

        // GET: Guest/Edit/{id}
        public ActionResult Edit(int id)
        {
            var guest = guestBLL.GetGuest(id);
            if (guest == null)
            {
                return HttpNotFound("Guest not found.");
            }
            ViewBag.FerryId = guest.FerryId;
            return View(guest);
        }

        // POST: Guest/Edit/{id}
        [HttpPost]
        public ActionResult Edit(GuestDTO guest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    guestBLL.UpdateGuest(guest);
                    if (guest.FerryId != null)
                        return RedirectToAction("Details", "Ferry", new { id = guest.FerryId });
                    else
                        return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error editing guest: " + ex.Message);
            }
            return View(guest);
        }

        // GET: Guest/Delete/{id}
        public ActionResult Delete(int id)
        {
            var guest = guestBLL.GetGuest(id);
            if (guest == null)
            {
                return HttpNotFound("Guest not found.");
            }
            return View(guest);
        }

        // POST: Guest/Delete/{id}
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var guest = guestBLL.GetGuest(id);
            if (guest == null)
            {
                return HttpNotFound("Guest not found.");
            }
            try
            {
                guestBLL.DeleteGuest(id);
                if (guest.FerryId != null)
                    return RedirectToAction("Details", "Ferry", new { id = guest.FerryId});
                else
                    return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error deleting guest: " + ex.Message);
                return View("Delete", guest);
            }
        }

    }
}