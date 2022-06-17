using Linkedin.Entities.UnitOfWork;
using Linkedin.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using static Linkedin.Common.TypeEnum;

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
        public IQueryable<User> GetAll(Expression<Func<User, bool>> predicate)
        {
            return _uw.GetRepository<Models.User>().GetAll(predicate);
        }
        public User GetByUserId(string UserId)
        {
            return _uw.GetRepository<User>().Get(x=>x.ExternalUserId== UserId);
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


        public bool VisitUser(string UserId, int countVisitToRequest)
        {

            User ObjUser = GetByUserId(UserId);

            ObjUser.VisitCount += 1;
            _uw.GetRepository<User>().Update(ObjUser);

            if (ObjUser.VisitCount >= countVisitToRequest)
            {
                Models.Request RecivedRequestRow = _uw.GetRepository<Models.Request>().GetAll(a => a.UserId == ObjUser.Id).FirstOrDefault();
                RecivedRequestRow.Status = (short)RequestStatus.Scheduled;
                RecivedRequestRow.UpdateDateTime = DateTime.Now;
                _uw.GetRepository<Models.Request>().Update(RecivedRequestRow);
            }

            Models.Visit ObjVisit = new Models.Visit();
            ObjVisit.CreateDateTime = DateTime.Now;
            ObjVisit.UserId = ObjUser.Id;

             _uw.GetRepository<Models.Visit>().Add(ObjVisit);
                                                      
            _uw.SaveChanges();
            return true;
        }

    }
}
