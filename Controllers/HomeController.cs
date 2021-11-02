using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using prjDBHomework.Models;
using System.Web.Security;

namespace prjDBHomework.Controllers
{
    
    public class HomeController : Controller
    {
        // GET: Home
        dbHomeworkEntities8 db = new dbHomeworkEntities8();
        public ActionResult Index()
        {
            
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        //Post: Home/Login
        [HttpPost]
        
        public ActionResult Login(string fUserId, string fPwd)
        {
            // 依帳密取得會員並指定給member
            var member = db.Member
                .Where(m => m.fUserId == fUserId && m.fPwd == fPwd)
                .FirstOrDefault();
            //若member為null，表示會員未註冊
            if (member == null)
            {
                ViewBag.Message = "帳密錯誤，登入失敗";
                return View();
            }
            //使用Session變數記錄歡迎詞
            Session["WelCome"] = member.fName + "歡迎光臨";
            FormsAuthentication.RedirectFromLoginPage(fUserId, true);
            return RedirectToAction("Index", "Member");
        }

        //Get:Home/Register
        public ActionResult Register()
        {
            return View();
        }
        //Post:Home/Register
        [HttpPost]
        public ActionResult Register(Member pMember)
        {
            //若模型沒有通過驗證則顯示目前的View
            if (ModelState.IsValid == false)
            {
                return View();
            }
            // 依帳號取得會員並指定給member
            var member = db.Member
                .Where(m => m.fUserId == pMember.fUserId)
                .FirstOrDefault();
            //若member為null，表示會員未註冊
            if (member == null)
            {
                //將會員記錄新增到tMember資料表
                db.Member.Add(pMember);
                db.SaveChanges();
                //執行Home控制器的Login動作方法
                return RedirectToAction("Login");
            }
            ViewBag.Message = "此帳號己有人使用，註冊失敗";
            return View();
        }

    }
}
