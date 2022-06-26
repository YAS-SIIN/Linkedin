using Linkedin.Entities.UnitOfWork;

using System;
using System.Linq;
using System.Linq.Expressions;


namespace Linkedin.Service.Visit
{

    public class VisitService : IVisitService
    {
        private readonly IUnitOfWork _uw;
        public VisitService(IUnitOfWork uw)
        {
            _uw = uw;
        }
        public IQueryable<Models.Visit> GetAll()
        {
            return _uw.GetRepository<Models.Visit>().GetAll();
        }
        public IQueryable<Models.Visit> GetAll(Expression<Func<Models.Visit, bool>> predicate)
        {
            return _uw.GetRepository<Models.Visit>().GetAll(predicate);
        }
        public Models.Visit GetById(int id)
        {
            return _uw.GetRepository<Models.Visit>().GetById(id);
        }

        public Models.Visit Insert(Models.Visit ObjVisit)
        {

            _uw.GetRepository<Models.Visit>().Add(ObjVisit);
            _uw.SaveChanges();
            return ObjVisit;
        }

        public Models.Visit Update(Models.Visit ObjVisit)
        {

            _uw.GetRepository<Models.Visit>().Update(ObjVisit);
            _uw.SaveChanges();
            return ObjVisit;
        }

        public Models.Visit Delete(Models.Visit ObjVisit)
        {

            _uw.GetRepository<Models.Visit>().Delete(ObjVisit);
            _uw.SaveChanges();
            return ObjVisit;
        }
    }
}
