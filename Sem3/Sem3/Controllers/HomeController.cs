using Sem3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Sem3.Controllers
{
    public class HomeController : Controller
    {
        eProjectSem3Entities3 objModel = new eProjectSem3Entities3();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
        //==============================================START=POST===========================================================//

     

        // GET: posts/Details/5
       

        // GET: posts/Create
        public ActionResult Theater()
        {
         
            return View();
        }

        // POST: posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Theater([Bind(Include = "Id,Title,content,userId")] posts posts)
        {
            if (ModelState.IsValid)
            {
               
            }

            return View(posts);
        }

        // GET: posts/Edit/5
        //==============================================END=POST===========================================================//

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string name, string password)
        {
            if (ModelState.IsValid)
            {
                var f_password = GetMD5(password);
                var data = objModel.user.Where(s => s.Name.Equals(name) && s.PassWord.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    //add session   
                    Session["Name"] = data.FirstOrDefault().Name;
                    Session["Email"] = data.FirstOrDefault().Email;
                    Session["idUser"] = data.FirstOrDefault().Id;
                    Session["isAdmin"] = data.FirstOrDefault().is_admin;
                    return RedirectToAction("Index");
                }
                else
                {

                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login");
                }
            }
            return View();

        }


        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        //HTTP Post Home/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(user _user)
        {
            if (ModelState.IsValid)
            {
                var check = objModel.user.FirstOrDefault(s => s.Email == _user.Email);
                if (check == null)
                {
                    _user.PassWord = GetMD5(_user.PassWord);
                    objModel.Configuration.ValidateOnSaveEnabled = false;
                    objModel.user.Add(_user);
                    objModel.SaveChanges();
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }


            }
            return View();


        }

        //create a string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }


        public ActionResult Feedback()
        {
            return View();
        }

 



        public ActionResult FreeRecipes()
        {

            return View(objModel.recipe.ToList());
        }

        public ActionResult TheContests()
        {
            return View();
        }

        public ActionResult FAQ()
        {
            return View();
        }



    }
}