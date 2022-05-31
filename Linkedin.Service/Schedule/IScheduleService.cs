using Linkedin.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Linkedin.Service.Schedule
{ 
    public interface IScheduleService
    {

        IQueryable<Models.Schedule> GetAll();

        Models.Schedule GetById(int id);

        Models.Schedule Insert(Models.Schedule ObjSchedule);
        Models.Schedule Update(Models.Schedule ObjSchedule);
        Models.Schedule Delete(Models.Schedule ObjSchedule);

    }
}
