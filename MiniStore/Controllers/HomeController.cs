using MiniStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiniStore.Controllers
{
    public class HomeController : Controller
    {

        ProductDBContext db = new ProductDBContext();
        UserDBContext dbuser = new UserDBContext();
        int totalprice = 0;

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View(db.Products.ToList());
        }


        public ActionResult Sum(string id)
        {
            totalprice = totalprice + Convert.ToInt32(id);
           
            //string aux = totalprice.ToString();

            //ViewBag.Num = totalprice;

            ViewBag.Num = totalprice;

            return RedirectToAction("Index", new {ViewBag.Num});
            //return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User u)
        {
            // this action is for handle post (login)
            if (ModelState.IsValid) // this is check validity
            {
                using (UserDBContext dc = new UserDBContext())
                {
                    var v = dc.Users.Where(a => a.Username.Equals(u.Username) && a.Password.Equals(u.Password)).FirstOrDefault();
                    if (v != null)
                    {
                        Session["LogedUserID"] = v.UserID.ToString();
                        return RedirectToAction("AdminView");
                    }
                }
            }
            return View(u);
        }



        public ActionResult AdminView()
        {

            if (Session["LogedUserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult CrudUsers()
        {
            return RedirectToAction("Index", "Users");
        }

        public ActionResult CrudProducts()
        {
            return RedirectToAction("Index", "Products");
        }



        public ActionResult Export(String type)
        {
            System.Diagnostics.Debug.Write("My type: "+type);


            ExporterFactory exporterfactory = new ExporterFactory();

            Exporter exporter = exporterfactory.createExporter(type);

            exporter.export();

            return RedirectToAction("Index", "Home");
        }


    }
}
