using CMS.Models;
using CMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.Controllers
{
    [Authorize(Roles = "ArrivalP")]
    public class ArrivalPortController : Controller
    {
        //
        // GET: /ArrivalPort/
        CMSEntities1 db = new CMSEntities1();
        
        public ActionResult Notifications()
        {
            var dataItem = (from s in db.tblNotification
                            join d in db.tblShipping on s.shippingID equals d.shippingID
                            where d.status == "Approved" || d.status == "Shipping"
                            select new NotifyShipmentVM { 
                                shippingID = s.shippingID,
                                notificationDate = s.notificationDate,
                                dateDepart = d.dateDepart,
                                itemCategory = d.itemCategory,
                                itemWeight = d.itemWeight,
                                locationFrom = d.locationFrom,
                                status = d.status
                            }).OrderByDescending(x => x.notificationDate).ToList();

            return View(dataItem);
        }

        public ActionResult UpdateArrival(string id)
        {
            var dataItem = db.tblShipping.Where(x => x.shippingID == id).FirstOrDefault();
            dataItem.status = "Arrived";
            dataItem.dateArrived = DateTime.Now.ToString("MM/dd/yyyy H:mm");
            db.SaveChanges();

            return RedirectToAction("Notifications", "ArrivalPort");
        }
    }
}
