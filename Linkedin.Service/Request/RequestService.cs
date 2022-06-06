﻿using Linkedin.Entities.UnitOfWork;
using Linkedin.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Linkedin.Service.Request
{
  
    public class RequestService : IRequestService
    { 
        private readonly IUnitOfWork _uw;
        public RequestService(IUnitOfWork uw)
        {
            _uw = uw;
        }
        public IQueryable<Models.Request> GetAll()
        {
            return _uw.GetRepository<Models.Request>().GetAll();
        }

        public Models.Request GetById(int id)
        {
            return _uw.GetRepository<Models.Request>().GetById(id);
        }

        public Models.Request Insert(Models.Request ObjRequest)
        {

            _uw.GetRepository<Models.Request>().Add(ObjRequest);
            _uw.SaveChanges();
            return ObjRequest;
        }

        public Models.Request Update(Models.Request ObjRequest)
        {

            _uw.GetRepository<Models.Request>().Update(ObjRequest);
            _uw.SaveChanges();
            return ObjRequest;
        }

        public Models.Request Delete(Models.Request ObjRequest)
        {

            _uw.GetRepository<Models.Request>().Delete(ObjRequest);
            _uw.SaveChanges();
            return ObjRequest;
        }

        public List<Models.Request> DeleteRange(List<Models.Request> LstObjRequest)
        {

            _uw.GetRepository<Models.Request>().DeleteRange(LstObjRequest);
            _uw.SaveChanges();
            return LstObjRequest;
        }

    }
}
