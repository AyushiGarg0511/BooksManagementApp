using ConceptArchitect.Data;
using ConceptArchitect.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement.Repositories.Ado
{
    public class AdoBookRepository : IRepository<Book, string>
    {
        DbManager db;
        public AdoBookRepository(DbManager db)
        {
            this.db = db;
        }

        public async Task<Book> fav(Book book, string userId)
        {
            var query = $"insert into favorites(book_id, user_id, status) " +
                              $"values('{book.Id}','{userId}','Reading')";

            await db.ExecuteUpdateAsync(query);

            return book;
        }
        public async Task<Book> Add(Book book)
        {
            var query = $"insert into books(id,title,description,author_id,author_name, cover_photo) " +
                              $"values('{book.Id}','{book.title}','{book.desc}','{book.authorid}','{book.authorname}', '{book.coverphoto}')";

            await db.ExecuteUpdateAsync(query);

            return book;
        }
        public async Task DeleteFav(string id, string user_id)
        {
            await db.ExecuteUpdateAsync($"delete from favorites where book_id='{id}' and user_id='{user_id}'");
        }
        public async Task Delete(string id)
        {
            await db.ExecuteUpdateAsync($"delete from books where id='{id}'");
        }
        private Book AuthorExtractor2(IDataReader reader)
        {
            return new Book()
            {
                Id = reader["id"].ToString()
            };
        }
        private Book AuthorExtractor(IDataReader reader)
        {
            return new Book()
            {
                Id = reader["id"].ToString(),
                title = reader["title"].ToString(),
                desc = reader["description"].ToString(),
                authorid = reader["author_id"].ToString(),
                authorname = reader["author_name"].ToString(),
                coverphoto = reader["cover_photo"].ToString(),

            };
        }
        

        public async Task<List<Book>> GetAll()
        {
            return await db.QueryAsync("select * from books", AuthorExtractor);
        }

        public async Task<List<Book>> GetAll(Func<Book, bool> predicate)
        {
            var books = await GetAll();

            return (from book in books
                    where predicate(book)
                    select book).ToList();

        }

        public async Task<Book> GetById(string id)
        {
            return await db.QueryOneAsync($"select * from books where id='{id}'", AuthorExtractor);
        }

        public async Task<Book> Update(Book entity, Action<Book, Book> mergeOldNew)
        {
            var oldAuthor = await GetById(entity.Id);
            if (oldAuthor != null)
            {
                mergeOldNew(oldAuthor, entity);
                var query = $"update books set " +
                            $"title='{oldAuthor.title}', " +
                            $"description='{oldAuthor.desc}', " +
                            $"author_id='{oldAuthor.authorid}', " +
                            $"author_name='{oldAuthor.authorname}', " +
                            $"cover_photo='{oldAuthor.coverphoto}' " +
                            $"where id='{oldAuthor.Id}'";

                await db.ExecuteUpdateAsync(query);
            }

            return entity;



        }

        public async Task<List<Book>> GetAllF(string uname)
        {
            return await db.QueryAsync($"Select * from Books where id in(Select book_id from Favorites where user_id='{uname}');", AuthorExtractor);
        }

        public async Task<List<Book>> GetAllF(Func<Book, bool> predicate)
        {
            var books = await GetAllF("");

            return (from book in books
                    where predicate(book)
                    select book).ToList();

        }

        public Task Delete(string id, string uname)
        {
            throw new NotImplementedException();
        }
    }
}
