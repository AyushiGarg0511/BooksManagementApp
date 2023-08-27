using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement
{
    public interface IUserService
    {
        Task<List<User>> GetAllUser();
        Task<User> GetUserById(string id);

        Task<User> AddUser(User author);

        Task<User> UpdateUser(User author);

        Task DeleteUser(string authorId);

        Task<List<User>> SearchUser(string term);
    }
}
