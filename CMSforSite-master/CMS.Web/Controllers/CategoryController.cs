using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS.Domain.Interfaces;
using CMS.Web.Models;
using PagedList;

namespace CMS.Web.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category

        private readonly IPhoto _photoRepository;
        private readonly ICategories categoriesRepository;
        public CategoryController(ICategories _categoriesRepository, IPhoto photoRepository)
        {
            categoriesRepository = _categoriesRepository;
            _photoRepository = photoRepository;
        }
     
        public ActionResult Categories(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var categories = categoriesRepository.Categories();
            if (categories.Count > 0)
            {
                return View(categories.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                return View();
            }
        }
        public ViewResult Category(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var categories = categoriesRepository.Categories();
            if (categories.Count > 0)
            {
                return View(categories);
            }
            else
            {
                return View();
            }
        }
        public ViewResult Article(Guid id)
        {
            var post = categoriesRepository.Category(id);

            if (post == null)
                throw new HttpException(404, "Post not found");

           

            return View(post);
        }
        [ChildActionOnly]
        public PartialViewResult Sidebars()
        {
            var widgetViewModel = new WidgetViewModel(categoriesRepository);
            return PartialView("_Sidebars", widgetViewModel);
        }
        [ChildActionOnly]
        public PartialViewResult PhotoBar(Guid id)
        {
            var widgetViewModel = new PhotoViewModel(id, _photoRepository, categoriesRepository);
            return PartialView("PhotoBar", widgetViewModel);
        }
    }
}