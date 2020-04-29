using CMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Domain.Interfaces
{
  public  interface IFeedback
    {
        IList<Feedback> AllFeedbacks();
        void CreateArticle(Feedback article);
      
    }
}
