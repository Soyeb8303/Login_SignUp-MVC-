using LS1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LS1.Controllers
{
    public class HomeController : Controller
    {
      readonly  LS1Entities1 db = new LS1Entities1();

        public ActionResult First()
        {
            return View();
        }
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(user1 u)
        {
            if (ModelState.IsValid == true)
            {
                var data = db.user1.Where(model => model.name == u.name && model.password == u.password).FirstOrDefault();
                if (data == null)
                {
                    ViewBag.ErrorMessage = "Name and Password not matching";
                }
                else
                {

                    HttpCookie cookie = new HttpCookie("name");
                    cookie.Value = u.name;
                    HttpContext.Response.Cookies.Add(cookie);
                    cookie.Expires = DateTime.Now.AddMinutes(5);
                    
                    return RedirectToAction("Data", "Home");
                }
            }
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(user1 u)
        {
            if (ModelState.IsValid == true)
            {

                var data = db.user1.Where(model=>model.email==u.email).FirstOrDefault();
                if (data != null)
                {
                    ViewBag.ErrorMessage = "Email is Aready Exist";
                }
                else
                {
                    db.user1.Add(u);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
           
            }
            return View();

        }
        public ActionResult Data()
        {
            return View();
        }
    }
}