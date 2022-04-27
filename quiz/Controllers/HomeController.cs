using quiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace quiz.Controllers
{
    public class HomeController : Controller
    {
        DBQUIZEntities db = new DBQUIZEntities();
        public ActionResult tlogin()
        {
            return View();
        }
        public ActionResult slogin()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Dashboard()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AddCategory()
        {
            Session["ad_id"] = 1;
            int adid = Convert.ToInt32(Session["ad_id"].ToString());
            List <tbl_category> li = db.tbl_category.Where(x => x.cat_fk_adid == adid).OrderByDescending(x => x.cat_id).ToList();
            ViewData["list"] = li;
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(tbl_category cat)
        {
            List<tbl_category> li = db.tbl_category.OrderByDescending(x => x.cat_id).ToList();
            ViewData["list"] = li;
            tbl_category c=new tbl_category();
            c.cat_name = cat.cat_name;
            c.cat_fk_adid=Convert.ToInt32(Session["ad_id"].ToString());
            db.tbl_category.Add(c);
            db.SaveChanges();

            return RedirectToAction("AddCategory");
        }
    }
}