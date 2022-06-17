using Linkedin.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace Linkedin.Service.Activity
{ 
    public interface IActivityService
    {

        IQueryable<Models.Activity> GetAll();
        IQueryable<Models.Activity> GetAll(Expression<Func<Models.Activity, bool>> predicate);

        Models.Activity GetById(int id);

        Models.Activity Insert(Models.Activity ObjActivity);
        Models.Activity Update(Models.Activity ObjActivity);
        List<Models.Activity> UpdateList(List<Models.Activity> ObjActivity);
        Models.Activity Delete(Models.Activity ObjActivity);     

    }
}
