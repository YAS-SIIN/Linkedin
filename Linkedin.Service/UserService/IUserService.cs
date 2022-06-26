using Linkedin.Models;

using System;
using System.Linq;
using System.Linq.Expressions;

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
        Models.Activity InsertActivity(Models.Activity ObjActivity);
        bool VisitUser(User UserId, int countVisitToRequest);
    }
}
