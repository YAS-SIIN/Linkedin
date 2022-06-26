using Linkedin.Entities.UnitOfWork;
using Linkedin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using static Linkedin.Common.TypeEnum;

namespace Linkedin.Service.Activity
{

    public class ActivityService : IActivityService
    {
        private readonly IUnitOfWork _uw;
        public ActivityService(IUnitOfWork uw)
        {
            _uw = uw;
        }
        public IQueryable<Models.Activity> GetAll()
        {
            return _uw.GetRepository<Models.Activity>().GetAll();
        }

        public IQueryable<Models.Activity> GetAll(Expression<Func<Models.Activity, bool>> predicate)
        {
            return _uw.GetRepository<Models.Activity>().GetAll(predicate);
        }

        public Models.Activity GetById(int id)
        {
            return _uw.GetRepository<Models.Activity>().GetById(id);
        }

        public Models.Activity Insert(Models.Activity ObjActivity)
        {
            _uw.GetRepository<Models.Activity>().Add(ObjActivity);
            _uw.SaveChanges();
            return ObjActivity;
        }

        public Models.Activity Update(Models.Activity ObjActivity)
        {
            _uw.GetRepository<Models.Activity>().Update(ObjActivity);
            _uw.SaveChanges();
            return ObjActivity;
        }

        public List<Models.Activity> UpdateList(List<Models.Activity> ObjActivity)
        {
            _uw.GetRepository<Models.Activity>().UpdateRange(ObjActivity);
            _uw.SaveChanges();
            return ObjActivity;
        }

        public Models.Activity Delete(Models.Activity ObjActivity)
        {
            _uw.GetRepository<Models.Activity>().Delete(ObjActivity);
            _uw.SaveChanges();
            return ObjActivity;
        }


        public Models.Activity InsertNewActivity(User ObjUser, String ActivityId)
        {

            Models.Activity ObjActivity = new Models.Activity();

            ObjActivity.ActivityId = ActivityId;
            ObjActivity.User = ObjUser;
            ObjActivity.UserId = ObjUser.Id;

            ObjActivity.Status = (short)UserStatus.Submit;
            ObjActivity.CreateDateTime = DateTime.Now;
            ObjActivity.UpdateDateTime = DateTime.Now;

            _uw.GetRepository<Models.Activity>().Add(ObjActivity);

            _uw.SaveChanges();
            return ObjActivity;
        }
    }
}
