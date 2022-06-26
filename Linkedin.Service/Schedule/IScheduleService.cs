
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace Linkedin.Service.Schedule
{
    public interface IScheduleService
    {

        IQueryable<Models.Schedule> GetAll();
        IQueryable<Models.Schedule> GetAll(Expression<Func<Models.Schedule, bool>> predicate);
        Models.Schedule GetById(int id);

        Models.Schedule Insert(Models.Schedule ObjSchedule);
        Models.Schedule Update(Models.Schedule ObjSchedule);
        Models.Schedule Delete(Models.Schedule ObjSchedule);
        void ScheduleUsers(List<Models.User> ObjUsers);

    }
}
