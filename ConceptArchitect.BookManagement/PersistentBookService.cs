using ConceptArchitect.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement
{
    public class PersistentBookService: IBookService
    {
        IRepository<Book, string> repository;

        //constructor based DI
        public PersistentBookService(IRepository<Book, string> repository)
        {
            this.repository = repository;
        }

        public async Task<Book> addFav(Book author, string userId)
        {
            return await repository.fav(author, userId);
        }
        public async Task<Book> AddBook(Book book)
        {
            //perform some validation if needed
            if (book == null)
                throw new InvalidDataException("book can't be null");

            if (string.IsNullOrEmpty(book.Id))
            {
                book.Id = await GenerateId(book.title);
            }
            return await repository.Add(book);
        }

        private async Task<string> GenerateId(string name)
        {
            var id = name.ToLower().Replace(" ", "-");

            if (await repository.GetById(id) == null)
                return id;

            int d = 1;
            while (await repository.GetById($"{id}-{d}") != null)
                d++;

            return $"{id}-{d}";

        }

        public async Task DeleteFav(string bookId, string userId)
        {
            await repository.DeleteFav(bookId, userId);
        }
        public async Task DeleteBook(string bookId)
        {
            await repository.Delete(bookId);
        }

        public async Task<List<Book>> GetAllBooks()
        {
            return await repository.GetAll();
        }
        public async Task<List<Book>> GetAllfavs(string uname)
        {
            return await repository.GetAllF(uname);
        }

        public async Task<Book> GetBookById(string id)
        {
            return await repository.GetById(id);
        }

        public async Task<List<Book>> SearchBooks(string term)
        {
            term = term.ToLower();

            return await repository.GetAll(a => a.title.ToLower().Contains(term) || a.desc.ToLower().Contains(term));
        }

        public async Task<Book> UpdateBook(Book book)
        {

            return await repository.Update(book, (old, newDetails) =>
            {
                old.title = newDetails.title;
                old.desc = newDetails.desc;
                old.authorid = newDetails.authorid;
                old.authorname = newDetails.authorname;
                old.coverphoto = newDetails.coverphoto;
            });
        }
        

    }
}
