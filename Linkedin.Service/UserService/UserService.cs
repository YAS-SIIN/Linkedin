using Linkedin.Entities.UnitOfWork;
using Linkedin.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkedin.Service.UserService
{
  public class UserService:IUserService
    { 
        private readonly IUnitOfWork _uw;
        public UserService(IUnitOfWork uw)
        {
            _uw = uw;
        }

        public IQueryable<User> GetAll()
        {
            return _uw.GetRepository<User>().GetAll();
        }

        public User GetById(int id)
        {
            return _uw.GetRepository<User>().GetById(id);
        }

        public User Insert(User ObjUser)
        {

            _uw.GetRepository<User>().Add(ObjUser);
            _uw.SaveChanges();
            return ObjUser;
        }

        public User Update(User ObjUser)
        {               
            _uw.GetRepository<User>().Update(ObjUser);
            _uw.SaveChanges();
            return ObjUser;
        }

        public User Delete(User ObjUser)
        {

            _uw.GetRepository<User>().Delete(ObjUser);
            _uw.SaveChanges();
            return ObjUser;
        }
    }
}
