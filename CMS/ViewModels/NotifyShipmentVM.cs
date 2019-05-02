using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.ViewModels
{
    public class NotifyShipmentVM
    {
        public List<Shipment> List { get; set; }
        public string notificationID { get; set; }
        public string shippingID { get; set; }
        public Nullable<System.DateTime> notificationDate { get; set; }

        public string userID { get; set; }
        public Nullable<System.DateTime> dateDepart { get; set; }
        public string itemCategory { get; set; }
        public Nullable<decimal> itemWeight { get; set; }
        public string locationFrom { get; set; }
        public string locationTo { get; set; }
        public string status { get; set; }

    }
}