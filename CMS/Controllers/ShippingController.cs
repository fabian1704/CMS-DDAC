using CMS.Models;
using CMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CMS.Controllers
{
    public class ShippingController : Controller
    {
        CMSEntities1 db = new CMSEntities1();



        [Authorize(Roles="Customer")]
        public ActionResult BookShipment()
        {
            string userStr = User.Identity.Name;
            string[] words = userStr.Split('|');
            string username = words[0];

            ViewBag.Username = username;

            return View();
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        public ActionResult BookShipment(Shipment model)
        {
            if (ModelState.IsValid)
            {
                if(model.dateDepart < DateTime.Now)
                {
                    ModelState.AddModelError("dateDepart", "Please select one day after today");
                    return View();
                }
                else
                {
                    string userStr = User.Identity.Name;
                    string[] words = userStr.Split('|');
                    string username = words[0];
                    string userID = words[2];
                    Guid shippingID = Guid.NewGuid();

                    tblShipping shippingTable = new tblShipping();

                    shippingTable.shippingID = shippingID.ToString();
                    shippingTable.userID = userID;
                    shippingTable.dateDepart = model.dateDepart;
                    shippingTable.itemCategory = model.itemCategory;
                    shippingTable.itemWeight = model.itemWeight;
                    shippingTable.locationFrom = model.locationFrom;
                    shippingTable.locationTo = model.locationTo;
                    shippingTable.dateArrived = "Pending";
                    shippingTable.status = "Pending";

                    db.tblShipping.Add(shippingTable);
                    db.SaveChanges();

                    //TempData["Success"] = "Register Successfully";
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        [Authorize]
        public ActionResult ViewShipment()
        {
            string userStr = User.Identity.Name;
            string[] words = userStr.Split('|');
            string username = words[0];
            string userID = words[2];

            var joinTable = (from s in db.tblShipping
                                join d in db.tblUser on s.userID equals d.userID
                                //join f in db.tblShippingRoute on s.shippingRouteID equals f.shippingRouteID
                                where s.userID == userID
                                select new Shipment
                                {
                                    shippingID = s.shippingID,
                                    dateDepart = s.dateDepart,
                                    itemCategory = s.itemCategory,
                                    itemWeight = s.itemWeight,
                                    locationFrom = s.locationFrom,
                                    locationTo = s.locationTo,
                                    dateArrived = s.dateArrived,
                                    status = s.status
                                }).ToList();

            return View(joinTable);
        }
    }
}
