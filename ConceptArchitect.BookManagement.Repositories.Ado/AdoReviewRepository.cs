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
    public class AdoReviewRepository : IRepository<review, string>
    {
        DbManager db;
        public AdoReviewRepository(DbManager db)
        {
            this.db = db;
        }
        public async Task DeleteReview(string id, string uname)
        {
            await db.ExecuteUpdateAsync($"delete from REVIEWS where reviewer_email='{uname}' and book_id='{id}'");
            
        }
        public async Task<review> Add(review author)
        {
            var query = $"insert into REVIEWS(reviewer_email,book_id,rating,title,details) " +
                              $"values('{author.reviewer_email}','{author.book_id}','{author.rating}','{author.title}','{author.details}')";

            await db.ExecuteUpdateAsync(query);

            return author;
        }

        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteFav(string bookId, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<review> fav(review entity, string userId)
        {
            throw new NotImplementedException();
        }
        private review AuthorExtractor(IDataReader reader)
        {
            return new review()
            {
                reviewer_email = reader["reviewer_email"].ToString(),
                book_id = reader["book_id"].ToString(),
                rating = Convert.ToInt32(reader["rating"]),
                title = reader["rating"].ToString(),
                details = reader["details"].ToString()
            };
        }
        public async Task<List<review>> GetAll()
        {
            return await db.QueryAsync("select * from reviews", AuthorExtractor);
        }

        public async Task<List<review>> GetAll(Func<review, bool> predicate)
        {
            var authors = await GetAll();

            return (from author in authors
                    where predicate(author)
                    select author).ToList();

        }

        public Task<List<review>> GetAllF()
        {
            throw new NotImplementedException();
        }

        public Task<review> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<review> Update(review entity, Action<review, review> mergeOldNew)
        {
            throw new NotImplementedException();
        }

        

        public Task<List<review>> GetAllF(string uname)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(string id, string uname)
        {
            await db.ExecuteUpdateAsync($"delete from REVIEWS where reviewer_email='{uname}' and book_id='{id}'");
        }
    }
}
