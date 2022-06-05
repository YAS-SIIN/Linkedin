﻿using Linkedin.Entities.UnitOfWork;
using Linkedin.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Linkedin.Service.Activity
{
  
    public class ActivityService : IActivityService
    { 
        private readonly IUnitOfWork _uw;
        public ActivityService(IUnitOfWork uw)
        {
            _uw = uw;
        }
        public IQueryable<Models.Activity> GetAll()
        {
            return _uw.GetRepository<Models.Activity>().GetAll();
        }

        public Models.Activity GetById(int id)
        {
            return _uw.GetRepository<Models.Activity>().GetById(id);
        }

        public Models.Activity Insert(Models.Activity ObjActivity)
        {

            _uw.GetRepository<Models.Activity>().Add(ObjActivity);
            _uw.SaveChanges();
            return ObjActivity;
        }

        public Models.Activity Update(Models.Activity ObjActivity)
        {

            _uw.GetRepository<Models.Activity>().Update(ObjActivity);
            _uw.SaveChanges();
            return ObjActivity;
        }

        public Models.Activity Delete(Models.Activity ObjActivity)
        {

            _uw.GetRepository<Models.Activity>().Delete(ObjActivity);
            _uw.SaveChanges();
            return ObjActivity;
        }
 
    }
}
