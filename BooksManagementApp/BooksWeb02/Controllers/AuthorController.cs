using ConceptArchitect.BookManagement;
using Microsoft.AspNetCore.Mvc;

namespace BooksWeb02.Controllers
{
    public class AuthorController:Controller
    {
        IAuthorService authorService;
        List<string> response = new List<string>();
        bool one = true;
        public AuthorController(IAuthorService authors)
        {
            this.authorService = authors;
            if (one)
            {
                response.Add("dinkar");
                response.Add("dumas");
                response.Add("jeffrey-archer");
                response.Add("jk-rowling");
                response.Add("john-grisham");
                response.Add("mahatma-gandhi");
                response.Add("premchand");
                response.Add("vivek-dutta-mishra");
                one = false;
            }
            
        }


        public async Task<ViewResult> Index()
        {
            var authors = await authorService.GetAllAuthors();

            return View(authors);
        }

        public async Task<ViewResult> Details(string id)
        {
            var author = await authorService.GetAuthorById(id);

            return View(author);
        }
        public async Task<ViewResult> Search(string term)
        {
            var response = await authorService.SearchAuthors(term);
            return View("Index", response);
        }
        public async  Task<List<string>> GetAllAuthorsList()
        {
            await Task.Delay(500);
            return response;
        }
        public async Task<ActionResult> Delete(string id)
        {
            await authorService.DeleteAuthor(id);
            response.Remove(id);
            return RedirectToAction("Index");
        }
        public async Task<ViewResult> Edit(string id)
        {
            var author = await authorService.GetAuthorById(id);
            return View(author);
        }
        [HttpPost]
        public async Task<ActionResult> Edit(Author author)
        {
            if (ModelState.IsValid)
            {

                await authorService.UpdateAuthor(author);
                return RedirectToAction("Index");
            }

            return RedirectToAction("edit", "author", null);
        }
        
        [HttpGet]
        public ViewResult Add()
        {
            return View(new Author());
        }

        [HttpPost]
        public async Task<ActionResult> Add(Author author)
        {
            if(ModelState.IsValid)
            {
                await authorService.AddAuthor(author);
                response.Add(author.Id);
                return RedirectToAction("Index");
            }
            return View(author);
        }


        public async Task<ActionResult> SaveV2(Author author)
        {
            await authorService.AddAuthor(author);

            return RedirectToAction("Index");
        }



        public Author SaveV1(string id, string name, string bio, string email, string photourl, DateTime dob)
        {
            var author = new Author()
            {
               Id=id,
               Name=name,
               Biography=bio,
               Email=email,
               BirthDate=dob,
               Photo=photourl
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
