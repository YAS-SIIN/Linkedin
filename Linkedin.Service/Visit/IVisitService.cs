
using System;
using System.Linq;
using System.Linq.Expressions;


namespace Linkedin.Service.Visit
{
    public interface IVisitService
    {

        IQueryable<Models.Visit> GetAll();
        IQueryable<Models.Visit> GetAll(Expression<Func<Models.Visit, bool>> predicate);
        Models.Visit GetById(int id);
        Models.Visit Insert(Models.Visit ObjVisit);
        Models.Visit Update(Models.Visit ObjVisit);
        Models.Visit Delete(Models.Visit ObjVisit);

    }
}
