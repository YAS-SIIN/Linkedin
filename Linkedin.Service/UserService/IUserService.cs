using Linkedin.Models;         

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Linkedin.Service.UserService
{
   public interface IUserService 
    {
        IQueryable<User> GetAll();
        IQueryable<Models.User> GetAll(Expression<Func<Models.User, bool>> predicate);

        User GetById(int id);
        User GetByUserId(string UserId);

        User Insert(User ObjUser);
        User Update(User ObjUser);
        User Delete(User ObjUser);
        bool VisitUser(string UserId, int countVisitToRequest);
    }
}
