using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement
{
    public interface IReviewService
    {
        Task<List<review>> GetAllReviews();
        Task<review> AddReview(review review);
        Task DeleteReview(string id, string uname);
    }
}
