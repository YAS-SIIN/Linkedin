using Linkedin.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Linkedin.Service.Visit
{ 
    public interface IVisitService
    {

        IQueryable<Models.Visit> GetAll();

        Models.Visit GetById(int id);

        Models.Visit Insert(Models.Visit ObjVisit);
        Models.Visit Update(Models.Visit ObjVisit);
        Models.Visit Delete(Models.Visit ObjVisit);

    }
}
