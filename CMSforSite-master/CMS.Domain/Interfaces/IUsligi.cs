using CMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Domain.Interface
{
    public interface IUsligi
    {
        IList<Uslugis> ListOfService();

        Uslugis Service(Guid id);
        
       
        void DeleteServices(Guid id);

        Guid CreateService(Uslugis usligi);
    }
}
