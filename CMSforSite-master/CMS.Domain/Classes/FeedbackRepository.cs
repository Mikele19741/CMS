using CMS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Domain.Models;
using CMS.Domain.Context;
using System.Data.Entity;
using System.Runtime.Remoting.Messaging;

namespace CMS.Domain.Classes
{
    public class FeedbackRepository : IFeedback
    {
        private readonly Dal _context = new Dal();

        public IList<Feedback> AllFeedbacks()
        {
            return _context.Feedback.ToList();
        }

        public void CreateArticle(Feedback article)
        {
            try
            {
                if (article.id == Guid.Empty)
                {
                    article.id = Guid.NewGuid();
                    article.PostedOn = DateTime.Now;
                    _context.Feedback.Add(article);
                    _context.SaveChanges();
                }
            }
            catch (Exception e)
            {
               
                throw new ApplicationException(e.Message);
            }
            

            
        }
    }
}
