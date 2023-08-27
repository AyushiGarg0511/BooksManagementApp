using ConceptArchitect.BookManagement;
using Microsoft.AspNetCore.Mvc;

namespace BooksWeb02.Controllers
{
    public class BookController:Controller
    {
        IBookService bookService;

        public BookController(IBookService books)
        {
            this.bookService = books;
        }

        public async Task<ViewResult> Index()
        {
            var books = await bookService.GetAllBooks();

            return View(books);
        }
        public async Task<ViewResult> favs()
        {
            string uname = HttpContext.Session.GetString("session");
            var books = await bookService.GetAllfavs(uname);

            return View(books);
        }

        public async Task<ViewResult> Details(string id)
        {
            var books = await bookService.GetBookById(id);

            return View(books);
        }
        public async Task<ActionResult> deletefavs(string id)
        {
            string userid = HttpContext.Session.GetString("session");
            await bookService.DeleteFav(id, userid);

            return RedirectToAction("favs");
        }
        public async Task<ActionResult> Delete(string id)
        {
            await bookService.DeleteBook(id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ViewResult> Edit(string id)
        {
            var author = await bookService.GetBookById(id);

            AuthorController ac = new AuthorController(null);
            ac.ControllerContext = ControllerContext;
            List<string> response = await ac.GetAllAuthorsList();
            ViewBag.response = response;


            return View(author);
        }
        
        [HttpGet]
        public async Task<ActionResult> favorite(string id)
        {
            var author = await bookService.GetBookById(id);
            string uname = HttpContext.Session.GetString("session");
            await bookService.addFav(author, uname);

            return RedirectToAction("favs");
        }
        
       
        [HttpPost]
        public async Task<ActionResult> Edit(Book author)
        {
            if (ModelState.IsValid)
            {
                await bookService.UpdateBook(author);

                return RedirectToAction("Index");
            }
            return RedirectToAction("edit", "book");
        }
        
        [HttpGet]
        public async Task<ViewResult> AddAsync()
        {
            AuthorController ac = new AuthorController(null);
            ac.ControllerContext = ControllerContext;
            List<string> response = await ac.GetAllAuthorsList();
            ViewBag.response = response;
            return View(new Book());
        }

        [HttpPost]
        public async Task<ActionResult> Add(Book book)
        {
            if (ModelState.IsValid)
            {

                await bookService.AddBook(book);

                return RedirectToAction("Index");
            }

            return RedirectToAction("add", "book", null);
        }
        [HttpPost]
        

        public async Task<ActionResult> SaveV2(Book book)
        {
            await bookService.AddBook(book);

            return RedirectToAction("Index");
        }



        public Author SaveV1(string id, string name, string bio, string email, string photourl, DateTime dob)
        {
            var author = new Author()
            {
                Id = id,
                Name = name,
                Biography = bio,
                Email = email,
                BirthDate = dob,
                Photo = photourl
            };

            return author;
        }

        public Author SaveV0()
        {
            var author = new Author()
            {
                Id = Request.Form["id"],
                Name = Request.Form["name"],
                Biography = Request.Form["bio"],
                Email = Request.Form["email"],
                Photo = Request.Form["photourl"]
            };

            return author;
        }
    }
}
