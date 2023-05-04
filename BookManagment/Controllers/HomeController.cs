using BookManagment.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookManagment.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(BookInfo bookInfo)
        {
            if (ModelState.IsValid)
            {
                string[] selectedTypes = Request.Form.GetValues("Type[]");
                if (selectedTypes != null)
                {
                    bookInfo.BookType = string.Join(",", selectedTypes);
                }

                var db = new BookEntities();
                db.BookInfoes.Add(bookInfo);
                db.SaveChanges();
                return RedirectToAction("BookList", "Home");
            }
            return View();
        }

        public ActionResult BookList()
        {
            ViewBag.Message = "Books List";

            var db = new BookEntities();
            var BookInfo = db.BookInfoes.ToList();
            return View(BookInfo);
        }

        public ActionResult EditBook(int id)
        {
            var db = new BookEntities();
            var Product = (from p in db.BookInfoes
                           where p.Id == id
                           select p).SingleOrDefault();
            return View(Product);
        }
        [HttpPost]
        public ActionResult EditBook(BookInfo update)
        {
            var db = new BookEntities();
            var UpdateProduct = (from p in db.BookInfoes
                                 where p.Id == update.Id
                                 select p).SingleOrDefault();
            UpdateProduct.Name = update.Name;
            UpdateProduct.PublisherName = update.PublisherName;
            UpdateProduct.Age = update.Age;
            UpdateProduct.Date = update.Date;
            UpdateProduct.Page = update.Page;
            UpdateProduct.BookType = update.BookType;
            db.SaveChanges();
            return RedirectToAction("BookList");
        }
        public ActionResult DeleteBook(int id)
        {
            var db = new BookEntities();
            var delete = (from d in db.BookInfoes
                          where d.Id == id
                          select d).SingleOrDefault();
            db.BookInfoes.Remove(delete);
            db.SaveChanges();
            return RedirectToAction("BookList");
        }
    }
}