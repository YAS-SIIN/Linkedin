using Linkedin.Entities.UnitOfWork;
using Linkedin.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Linkedin.Service.Visit
{
  
    public class VisitService : IVisitService
    {
        private readonly UnitOfWork _uw;
        public VisitService(UnitOfWork uw)
        {
            _uw = uw;
        }

        public IQueryable<Models.Visit> GetAll()
        {
            return _uw.GetRepository<Models.Visit>().GetAll();
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
