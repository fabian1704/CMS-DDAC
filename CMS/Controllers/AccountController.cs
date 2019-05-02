using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
//using CMS.Filters;
using CMS.Models;
using CMS.ViewModels;

namespace CMS.Controllers
{
    public class AccountController : Controller
    {
        CMSEntities1 db = new CMSEntities1();
        
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(tblLogin model, string returnUrl)
        {
            var dataItem = db.tblLogin.Where(x => x.username == model.username && x.password == model.password).FirstOrDefault();
            if (dataItem != null)
            {
                FormsAuthentication.SetAuthCookie(dataItem.username + "|" + dataItem.role + "|" + dataItem.userID, false);
                if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                         && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                {
                    
                    return Redirect(returnUrl);
                }
                else
                {
                    //Session["loginID"] = dataItem.loginID;
                    //Session["userRole"] = dataItem.role;
                    var urls = returnUrl;
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("error", "Invalid username or password");
                return View();
            }
        }

        [Authorize]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserRegistration model)
        {
            tblLogin loginTable = new tblLogin();
            tblUser userTable = new tblUser();
            if (ModelState.IsValid)
            {
                if (db.tblLogin.Any(x => x.username == model.username))
                {
                    ModelState.AddModelError("username", "Username is not available");
                    return View();
                }
                else
                {
                    Guid loginGUID = Guid.NewGuid();
                    Guid userGUID = Guid.NewGuid();

                    loginTable.loginID = userGUID.ToString();
                    loginTable.userID = userGUID.ToString();
                    loginTable.username = model.username;
                    loginTable.password = model.password;
                    loginTable.role = "Customer";

                    userTable.userID = userGUID.ToString();
                    userTable.name = model.name;
                    userTable.address = model.address;
                    userTable.email = model.email;
                    userTable.phone = model.phone;

                    db.tblLogin.Add(loginTable);
                    db.tblUser.Add(userTable);

                    db.SaveChanges();

                    return RedirectToAction("Index", "Home");
                    //var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "Home");
                    //return Json(new { Url = redirectUrl });
                }
            }
            return View();
        }
        //[HttpPost]
        //public ActionResult TestFunction()
        //{
        //    var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "Home");
        //    return Json(new { Url = redirectUrl });
        //}
    }
}
