using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Web.Security;
using prjDBHomework.Models;

namespace prjDBHomework.Controllers
{
    [Authorize]
      //指定Member控制器所有的動作方法必須通過授權才能執行。
    public class MemberController : Controller
    {
        //建立可存取dbShoppingCar.mdf 資料庫的dbShoppingCarEntities 類別物件db
        dbHomeworkEntities8 db = new dbHomeworkEntities8();


        // GET: Member/Index
        public ActionResult Index()
        {
            //取得所有產品放入products
           
            return View("../Home/Index", "_LayoutMember");
        }

        //Get:Member/Logout
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();   // 登出
            return RedirectToAction("Login", "Home");
        }
       public ActionResult browse(string No)
        {
            string user = User.Identity.Name;
            ViewBag.theGallery = db.Gallery.Where(m => m.gNo == No).FirstOrDefault().name;
            TextModel vm = new TextModel()
            {
                art = db.Art.Where(m => m.gNo == No).ToList(),
                /*collects = db.Collects.ToList(),
                member = db.Member.Where(m => m.fUserId == user).ToList()*/
            };
            return View(vm);
        }

        public ActionResult favorite(string aNo = "")
        {
            string user = User.Identity.Name;
            var collection = db.Collects.Where(m => m.aNo == aNo && m.fUserId == user).FirstOrDefault();

            if (aNo != "" && collection == null)
            {
                Collects col = new Collects();
                col.aNo = aNo;
                col.fUserId = user;
                db.Collects.Add(col);
                db.SaveChanges();
            }

            TextModel vm = new TextModel()
            {
                collects = db.Collects.Where(m => m.fUserId == user).ToList(),
                art = db.Art.ToList()
            };
            return View(vm);
        }

        public ActionResult DeleteFavorite(string aNo)
        {
            var user = User.Identity.Name;
            var collection = db.Collects.Where(m => m.aNo == aNo && m.fUserId == user).FirstOrDefault();
            db.Collects.Remove(collection);
            db.SaveChanges();
            return RedirectToAction("favorite");
        }
        /* public ActionResult visit(string gNo = "")
         {
             string user = User.Identity.Name;
             var collections = db.Visits.Where(m => m.gNo == gNo).FirstOrDefault();

             if (gNo != "" && collections == null)
             {
                 Visits col = new Visits();
                 col.gNo = gNo;
                 db.Visits.Add(col);
                 db.SaveChanges();
             }

             TextModel vm = new TextModel()
             {
                 visits = db.Visits.ToList(),
                 gallry = db.Gallery.ToList()
             };
             return View(vm);
         }

         public ActionResult DeleteVisit(string gNo)
         {
             var collections = db.Visits.Where(m => m.gNo == gNo).FirstOrDefault();
             db.Visits.Remove(collections);
             db.SaveChanges();
             return RedirectToAction("visit");
         }*/
        public ActionResult visit(string gNo = "")
        {
            string user = User.Identity.Name;
            var collection = db.Visits.Where(m => m.gNo == gNo && m.fUserId == user).FirstOrDefault();

            if (gNo != "" && collection == null)
            {
                Visits col = new Visits();
                col.gNo = gNo;
                col.fUserId = user;
                db.Visits.Add(col);
                db.SaveChanges();
            }

            TextModel vm = new TextModel()
            {
                visits = db.Visits.Where(m => m.fUserId == user).ToList(),
                gallry = db.Gallery.ToList()
            };
            return View(vm);
        }

        public ActionResult DeleteVisit(string gNo)
        {
            var user = User.Identity.Name;
            var collection = db.Visits.Where(m => m.gNo == gNo && m.fUserId == user).FirstOrDefault();
            db.Visits.Remove(collection);
            db.SaveChanges();
            return RedirectToAction("visit");
        }




        //Get:Member/ShoppingCar



        //Get:Member/AddCar


        //Get:Member/DeleteCar


        //Post:Member/ShoppingCar


    }
}