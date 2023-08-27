using ConceptArchitect.BookManagement;
using Microsoft.AspNetCore.Mvc;

namespace BooksWeb02.Controllers
{
    public class ReviewController : Controller
    {
        IReviewService authorService;

        public ReviewController(IReviewService authors)
        {
            this.authorService = authors;
        }
        public async Task<ViewResult> Index()
        {
            var books = await authorService.GetAllReviews();
            ViewBag.uname = HttpContext.Session.GetString("session");
            return View(books);
        }
        public async Task<ActionResult> Delete(string id)
        {
            string uname = HttpContext.Session.GetString("session");
            await authorService.DeleteReview(id, uname);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ViewResult> review(string id)
        {
            await Task.Delay(500);
            review author = new review();
            author.book_id = id;
            author.reviewer_email = HttpContext.Session.GetString("session");
            return  View(author);
        }
        [HttpPost]
        public async Task<ActionResult> review(review author)
        {
            if (ModelState.IsValid)
            {
                author.reviewer_email = HttpContext.Session.GetString("session");
                await authorService.AddReview(author);

                return RedirectToAction("Index");
            }
            return RedirectToAction("review", "review", author);
        }
    }
}
