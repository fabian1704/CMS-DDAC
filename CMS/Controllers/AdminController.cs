using CMS.Models;
using CMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        CMSEntities1 db = new CMSEntities1();

        public ActionResult ApprovedShipment()
        {
            //var dataItem = db.tblShipping.Where(x => x.status == "Approved" || x.status == "Shipping" || x.status == "Arrived").OrderBy(x=>x.status).ToList();
            var dataItem = (from s in db.tblUser
                            join d in db.tblShipping on s.userID equals d.userID
                            where d.status == "Approved" || d.status == "Shipping"
                            select new Shipment
                            {
                                shippingID = d.shippingID,
                                userID = d.userID,
                                dateDepart = d.dateDepart,
                                itemCategory = d.itemCategory,
                                itemWeight = d.itemWeight,
                                locationFrom = d.locationFrom,
                                locationTo = d.locationTo,
                                status = d.status
                            }).OrderBy(x => x.status).ToList();
            return View(dataItem);
        }

        [Authorize(Roles="Admin")]
        public ActionResult ShipmentPending()   
        {
            //var dataItem = db.tblShipping.Where(x => x.status == "Pending").ToList();
            var dataItem = (from s in db.tblUser
                            join d in db.tblShipping on s.userID equals d.userID
                            where d.status == "Pending"
                            select new Shipment
                            {
                                shippingID = d.shippingID,
                                userID = d.userID,
                                dateDepart = d.dateDepart,
                                itemCategory = d.itemCategory,
                                itemWeight = d.itemWeight,
                                locationFrom = d.locationFrom,
                                locationTo = d.locationTo,
                                status = d.status
                            }).ToList();
            return View(dataItem);
        }

        
        public ActionResult ApproveRequest(string id)
        {
            var dataItem = db.tblShipping.Where(x => x.shippingID == id).FirstOrDefault();
            dataItem.status = "Approved";

            tblNotification notifyTable = new tblNotification();

            Guid notifyID = Guid.NewGuid();
            notifyTable.notificationID = notifyID.ToString();
            notifyTable.shippingID = id;
            notifyTable.notificationDate = DateTime.Now;

            db.tblNotification.Add(notifyTable);
            db.SaveChanges();

            return RedirectToAction("ShipmentPending", "Admin");
        }

        public ActionResult ChangeStatus(string id)
        {
            var dataItem = db.tblShipping.Where(x => x.shippingID == id).FirstOrDefault();
            dataItem.status = "Shipping";

            db.SaveChanges();

            return RedirectToAction("ApprovedShipment", "Admin");
        }
    }
}
