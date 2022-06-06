using Linkedin.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Linkedin.Service.Request
{ 
    public interface IRequestService
    {

        IQueryable<Models.Request> GetAll();

        Models.Request GetById(int id);

        Models.Request Insert(Models.Request ObjRequest);
        Models.Request Update(Models.Request ObjRequest);
        Models.Request Delete(Models.Request ObjRequest);
        List<Models.Request> DeleteRange(List<Models.Request> LstObjRequest);
    }
}
