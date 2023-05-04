using BookManagment.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookAPI.Controllers
{
    public class BookController : ApiController
    {
        private BookEntities db = new BookEntities();
        [HttpGet]
        [Route("api/books")]
        public IEnumerable<BookInfo> GetBooks(string keyword = "", string age = "", string[] bookTypes = null)
        {
            try
            {
                var query = db.BookInfoes.AsQueryable();

                // Filter by keyword
                if (!string.IsNullOrEmpty(keyword))
                {
                    query = query.Where(b => b.Name.Contains(keyword) || b.PublisherName.Contains(keyword));
                }

                // Filter by age
                if (!string.IsNullOrEmpty(keyword))
                {
                    query = query.Where(b => b.Age == age);
                }

                // Filter by book types
                if (bookTypes != null && bookTypes.Length > 0)
                {
                    var bookTypesList = new List<string>(bookTypes);
                    query = query.Where(b => bookTypesList.Contains(b.BookType));
                }

                return query.ToList();
            }
            catch (Exception ex)
            {
                return (IEnumerable<BookInfo>)BadRequest(ex.Message);
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
