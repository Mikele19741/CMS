using CMS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using CMS.Domain.Models;
using CMS.Web.Models;

namespace CMS.Web.Controllers
{
    public class BlogController : Controller
    {
        // GET: Blog

        private readonly IPosts _blogRepository;
        private readonly IPhoto _photoRepository;
        private readonly ICategories _categoryRepository;
        public BlogController(IPosts blogRepository, IPhoto photoRepository, ICategories categoryRepository)
        {
            _blogRepository = blogRepository;
            _photoRepository = photoRepository;
            _categoryRepository = categoryRepository;
        }
        public ViewResult Posts(int ? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var posts = _blogRepository.PostsList();
            if (posts.Count > 0)
            {
                return View(posts.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                return View();
            }

        }
        public ViewResult Post(int year, int month, string title)
        {
            var post = _blogRepository.Post(year, month, title);

            if (post == null)
                throw new HttpException(404, "Post not found");

            if (post.Published == false && User.Identity.IsAuthenticated == false)
                throw new HttpException(401, "The post is not published");

            return View(post);
        }
        public ViewResult Art(Guid id)
        {
            var post = _blogRepository.Post(id);

            if (post == null)
                throw new HttpException(404, "Post not found");

            if (post.Published == false && User.Identity.IsAuthenticated == false)
                throw new HttpException(401, "The post is not published");

            return View("Post", post);
        }
        public ViewResult Category(string category)
        {
           
            //int pageSize = 10;
            //int pageNumber = (page ?? 1);
            var catt = _blogRepository.CategoriesList().FirstOrDefault(t => t.UrlSlug == category);
          //  var cat = _blogRepository.PostsForCategory(category);
           if(catt != null)
            {
                return View("Article", catt);
            }
           else
            {
                return View("Posts");
            }
        }
        [ChildActionOnly]
        public PartialViewResult Sidebars()
        {
            var widgetViewModel = new WidgetViewModel(_blogRepository);
            return PartialView("_Sidebars", widgetViewModel);
        }
        [ChildActionOnly]
        public PartialViewResult PhotoBar(int year, int month, string title)
        {
            var widgetViewModel = new PhotoViewModel(year, month, title, _photoRepository, _blogRepository);
            return PartialView("PhotoBar", widgetViewModel);
        }
        //[ChildActionOnly]
        //public PartialViewResult Sidebar()
        //{
        //    var widgetViewModel = new WidgetViewModel(_categoryRepository);
        //    return PartialView("_Sidebars", widgetViewModel);
        //}
        [ChildActionOnly]
        public PartialViewResult PhotoBars(Guid id)
        {
            var widgetViewModel = new PhotoViewModel(id, _photoRepository, _categoryRepository);
            return PartialView("PhotoBars", widgetViewModel);
        }

    }
}