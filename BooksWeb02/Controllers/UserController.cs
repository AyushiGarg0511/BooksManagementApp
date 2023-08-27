using ConceptArchitect.BookManagement;
using Microsoft.AspNetCore.Mvc;


namespace BooksWeb02.Controllers
{
    public class UserController : Controller
    {
        IUserService authorService;

        public UserController(IUserService authors)
        {
            this.authorService = authors;
        }

        public async Task<ViewResult> Index()
        {
            var authors = await authorService.GetAllUser();

            return View(authors);
        }

        public async Task<ViewResult> Details(string id)
        {
            var author = await authorService.GetUserById(id);

            return View(author);
        }
        public async Task<ViewResult> Search(string term)
        {
            var response = await authorService.SearchUser(term);
            return View("Index", response);
        }
        [HttpGet]
        public async Task<ViewResult> Edit()
        {
            var id = HttpContext.Session.GetString("session");
            var author = await authorService.GetUserById(id);
            return View("~/Views/User/check.cshtml", author);
        }
        [HttpPost]
        public async Task<ActionResult> Edit(User author)
        {
            if (ModelState.IsValid)
            {
                await authorService.UpdateUser(author);

                return RedirectToAction("index", "author");
            }

            return RedirectToAction("edit");
        }
        public async Task<ActionResult> Delete(string id)
        {
            if (ModelState.IsValid)
            {
                await authorService.DeleteUser(id);
                return RedirectToAction("login", "user", null);
            }
            return RedirectToAction("edit", "user", id);
        }

        [HttpGet]
        public ViewResult Add()
        {
            return View(new User());
        }
        [HttpGet]
        public ViewResult Login()
        {
            return View(new User());
        }
        [HttpPost]
        public async Task<ActionResult> Login(User user)
        {
            var response = await authorService.GetUserById(user.email);
            if(response == null || response.password != user.password)
            {
                return View(new User());
            }

            HttpContext.Session.SetString("session", user.email);
            return RedirectToAction("Index", "author", null);
        }

        [HttpPost]
        public async Task<ActionResult> Add(User user)
        {
            if (ModelState.IsValid)
            {
                await authorService.AddUser(user);

                return RedirectToAction("Index", "Home", null);
            }
            return RedirectToAction("add", "user", null);
        }


        public async Task<ActionResult> SaveV2(User author)
        {
            await authorService.AddUser(author);

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
