
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace Linkedin.Service.Request
{
    public interface IRequestService
    {

        IQueryable<Models.Request> GetAll();
        IQueryable<Models.Request> GetAll(Expression<Func<Models.Request, bool>> predicate);
        Models.Request GetById(int id);
        Models.Request Insert(Models.Request ObjRequest);
        Models.Request Update(Models.Request ObjRequest);
        Models.Request Delete(Models.Request ObjRequest);
        List<Models.Request> DeleteRange(List<Models.Request> LstObjRequest);
    }
}
