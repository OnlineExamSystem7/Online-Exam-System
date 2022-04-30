using quiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace quiz.Controllers
{
    public class HomeController : Controller
    {
        DBQUIZEntities db = new DBQUIZEntities();
        [HttpGet]
        public ActionResult sregister()
        {


            return View();
        }
        [HttpPost]
        public ActionResult sregister(TBL_STUDENT svw, HttpPostedFileBase imgfile)
        {
            TBL_STUDENT s = new TBL_STUDENT();
            try
            {
                s.S_NAME = svw.S_NAME;
                s.S_PASSWORD = svw.S_PASSWORD;
                s.S_IMAGE = uploadimage(imgfile);
                db.TBL_STUDENT.Add(s);
                db.SaveChanges();
                return RedirectToAction("slogin");
            }
            catch (Exception)
            {
                ViewBag.msg = "Data Could not be inserted....!";
            }

            return View();
        }
        public string uploadimage(HttpPostedFileBase imgfile )
        {
            string path = "-1";

            try
            {
                if(imgfile!= null && imgfile.ContentLength>0)
                {
                    string extension = Path.GetExtension(imgfile.FileName);
                    if (extension.ToLower().Equals("jpg") || extension.ToLower().Equals("jpeg") || extension.ToLower().Equals("png"))
                    {
                        //Path.Combine(Server.MapPath("~/Content/upload"), random + Path.GetFileName(file.FileName));

                        Random r=new Random();
                        path = Path.Combine(Server.MapPath("~/Content/img"),r+Path.GetFileName(imgfile.FileName));
                        imgfile.SaveAs(path);
                        path = "~/Content/img" + r + Path.GetFileName(imgfile.FileName);
                    }
                }




            }
            catch(Exception)
            {

            }
            return path;
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session.Abandon();
            Session.RemoveAll();
            return RedirectToAction("Index");


        }

        public ActionResult tlogin()
        {


            return View();
        }
        [HttpPost]
        public ActionResult tlogin(TBL_ADMIN a)
        {
            TBL_ADMIN ad = db.TBL_ADMIN.Where(x => x.AD_NAME == a.AD_NAME && x.AD_PASSWORD == a.AD_PASSWORD).SingleOrDefault();
            if(ad != null)
            {
                Session["ad_id"] = ad.AD_ID;
                return RedirectToAction("Dashboard");
            }
            else
            {
                ViewBag.msg = "Invalid Username or Password";
            }

            return View();
        }
        public ActionResult slogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult slogin(TBL_STUDENT s)
        {
            TBL_STUDENT std=db.TBL_STUDENT.Where(x =>x.S_NAME==s.S_NAME && x.S_PASSWORD==s.S_PASSWORD).SingleOrDefault();
            if(std== null)
            {
                ViewBag.msg = "Invalid Email or Password!";
            }
            else
            {
                return RedirectToAction("StudentExam");
            }
            return View();
        }
        public ActionResult StudentExam()
        {
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
            List<tbl_category> li = db.tbl_category.Where(x => x.cat_fk_adid == adid).OrderByDescending(x => x.cat_id).ToList();
            ViewData["list"] = li;
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(tbl_category cat)
        {
            List<tbl_category> li = db.tbl_category.OrderByDescending(x => x.cat_id).ToList();
            ViewData["list"] = li;
            tbl_category c = new tbl_category();
            c.cat_name = cat.cat_name;
            c.cat_fk_adid = Convert.ToInt32(Session["ad_id"].ToString());
            db.tbl_category.Add(c);
            db.SaveChanges();

            return RedirectToAction("AddCategory");
        }
        [HttpGet]
        public ActionResult Addquestion()
        {
            int sid = Convert.ToInt32(Session["ad_id"]);
            List<tbl_category> li = db.tbl_category.Where(x=>x.cat_fk_adid==sid).ToList();
            ViewBag.list =new SelectList(li, "cat_id" , "cat_name");

            return View();
        }



        [HttpPost]
        public ActionResult Addquestion(TBL_QUESTIONS q)
        {
            int sid = Convert.ToInt32(Session["ad_id"]);
            List<tbl_category> li = db.tbl_category.Where(x => x.cat_fk_adid == sid).ToList();
            ViewBag.list = new SelectList(li, "cat_id", "cat_name");

            TBL_QUESTIONS QA = new TBL_QUESTIONS();
            QA.Q_TEXT = q.Q_TEXT;
            QA.OPA = q.OPA;
            QA.OPB = q.OPB;
            QA.OPC = q.OPC;
            QA.OPD = q.OPD;
            QA.COP = q.COP;
            QA.q_fk_catid = q.q_fk_catid;   


            db.TBL_QUESTIONS.Add(QA);
            db.SaveChanges();
            TempData["msg"] = "Question Added Successfully...";
            return RedirectToAction("Addquestion");

           
        }
        
        public ActionResult Index()
        { if(Session["ad_id"]!=null)
            {
                return RedirectToAction("Dashboard");
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
    }
}