using CMS.Domain.Interfaces;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS.Domain.Models;
using Newtonsoft.Json;

namespace CMS.Web.Controllers
{
    public class FeedbackController : Controller
    {
        // GET: Feedback
        private readonly IFeedback _feedbackRepository;
        public FeedbackController(IFeedback feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }
        public ActionResult FeedBacks(int? page)
        {
            var news = _feedbackRepository.AllFeedbacks();
            if (news.Count > 0)
            {
                return View(news);
            }
            else
            {
                return View();
            }
        }
       [ValidateInput(false)]
       [HttpPost]
        public ActionResult AddFeedback(Feedback feedback)
        {
            if (TryValidateModel(feedback))
            {
               _feedbackRepository.CreateArticle(feedback);
                return View();
            }
            else
            {
                return View("Error");
            }
        }

    }
}