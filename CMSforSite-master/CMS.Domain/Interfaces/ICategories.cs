using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Domain.Interfaces
{
   public interface ICategories
   {
       List<Categories> Categories();
       Categories Category(Guid id);
        Guid CreateCategory(Categories cat);


    }
}
