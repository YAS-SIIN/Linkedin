using Linkedin.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkedin.Service.UserService
{
   public interface IUserService
    {

        IQueryable<User> GetAll();

        User GetById(int id);

        User Insert(User ObjUser);
        User Update(User ObjUser);
        User Delete(User ObjUser);

    }
}
