using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CMS.ViewModels
{
    public class Shipment
    {
        public List<Shipment> List { get; set; }
        [Display(Name = "Location From")]
        [Required]
        public string locationFrom { get; set; }
        [Display(Name = "Location To")]
        [Required]
        public string locationTo { get; set; }

        public string shippingID { get; set; }
        public string userID { get; set; }
        [Display(Name = "Date Depart")]
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> dateDepart { get; set; }
        [Display(Name = "Category")]
        [Required]
        public string itemCategory { get; set; }
        [Display(Name = "Weight")]
        [Required]
        public Nullable<decimal> itemWeight { get; set; }
        public string dateArrived { get; set; }
        public string status { get; set; }
    }
}