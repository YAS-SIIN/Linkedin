using Linkedin.Entities.UnitOfWork;
using Linkedin.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Linkedin.Service.Schedule
{
  
    public class ScheduleService : IScheduleService
    {
        private readonly UnitOfWork _uw;
        public ScheduleService(UnitOfWork uw)
        {
            _uw = uw;
        }

        public IQueryable<Models.Schedule> GetAll()
        {
            return _uw.GetRepository<Models.Schedule>().GetAll();
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
    }
}
