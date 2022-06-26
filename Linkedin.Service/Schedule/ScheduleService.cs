using Linkedin.Entities.UnitOfWork;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using static Linkedin.Common.TypeEnum;

namespace Linkedin.Service.Schedule
{

    public class ScheduleService : IScheduleService
    {
        private readonly IUnitOfWork _uw;
        public ScheduleService(IUnitOfWork uw)
        {
            _uw = uw;
        }
        public IQueryable<Models.Schedule> GetAll()
        {
            return _uw.GetRepository<Linkedin.Models.Schedule>().GetAll();
        }

        public IQueryable<Models.Schedule> GetAll(Expression<Func<Models.Schedule, bool>> predicate)
        {
            return _uw.GetRepository<Models.Schedule>().GetAll(predicate);
        }

        public Models.Schedule GetById(int id)
        {
            return _uw.GetRepository<Models.Schedule>().GetById(id);
        }

        public Models.Schedule Insert(Models.Schedule ObjSchedule)
        {

            _uw.GetRepository<Models.Schedule>().Add(ObjSchedule);
            _uw.SaveChanges();
            return ObjSchedule;
        }

        public Models.Schedule Update(Models.Schedule ObjSchedule)
        {

            _uw.GetRepository<Models.Schedule>().Update(ObjSchedule);
            _uw.SaveChanges();
            return ObjSchedule;
        }

        public Models.Schedule Delete(Models.Schedule ObjSchedule)
        {
            _uw.GetRepository<Models.Schedule>().Delete(ObjSchedule);
            _uw.SaveChanges();
            return ObjSchedule;
        }

        public void ScheduleUsers(List<Models.User> ObjUsers)
        {
           
            List<Models.Schedule> ObjLstSchedule = new List<Models.Schedule>();
            List<Models.User> ObjLstUser = new List<Models.User>();
            List<Models.Request> ObjLstRequest = new List<Models.Request>();
            foreach (var item in ObjUsers)
            {
                Models.Schedule objSchedule = new Models.Schedule();

                objSchedule.UserId = item.Id;
                objSchedule.Status = (short)ScheduleStatus.Submit;
                objSchedule.CreateDateTime = DateTime.Now;
                objSchedule.UpdateDateTime = DateTime.Now;

                _uw.GetRepository<Models.Schedule>().Add(objSchedule);


                item.Status = (short)UserStatus.InProgress;
                _uw.GetRepository<Models.User>().Update(item);

                Models.Request objRequest = new Models.Request();

                objRequest.UserId = item.Id;
                objRequest.Status = (short)ScheduleStatus.Submit;
                objRequest.CreateDateTime = DateTime.Now;
                objRequest.UpdateDateTime = DateTime.Now;
                _uw.GetRepository<Models.Request>().Add(objRequest);
            }


            _uw.SaveChanges();


        }
    }
}
