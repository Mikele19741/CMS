using CMS.Domain;
using CMS.Domain.Interfaces;
using CMS.Domain.Models;
using CMS.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using CMS.Domain.Interface;

namespace CMS.Web.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
       
        private readonly IPosts _blogRepository;
        private readonly IAdmin _authProvider;
        private readonly IFeedback _feedbackRepository;
        private readonly IUsligi _servicesRepository;
        private readonly IPhoto _photoRepository;
        private readonly ICategories _catRepository;
        public AdminController(IAdmin authProvider, IPosts blogRepository = null, IFeedback feedbackRepository=null, IUsligi services=null, IPhoto photo=null, ICategories cat=null)
        {
           _blogRepository = blogRepository;
            _authProvider = authProvider;
            _feedbackRepository = feedbackRepository;
            _servicesRepository = services;
            _photoRepository = photo;
            _catRepository = cat;
        }
        // GET: Admin
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            // If already logged-in redirect the user to manage page.
            if (_authProvider.IsLoggedIn)
                return RedirectToUrl(returnUrl);

            ViewBag.ReturnUrl = returnUrl;

            return View();
        }
       
        [HttpPost]
        [AllowAnonymous]
       // [ValidateAntiForgeryToken]
        public ActionResult Login(Login model, string returnUrl)
        {
            if (ModelState.IsValid && _authProvider.Login(model.UserName, model.Password))
            {
                
                if (model.UserName == "admin" && model.Password == "E785bo21zx")
                {
               
                    return RedirectToUrl(returnUrl);
                }
                       
            }
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }
        [Authorize(Users ="admin")]
        public ActionResult Manage()
        {
            return View();
        }

        private ActionResult RedirectToUrl(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Manage");
            }
        }
        public ActionResult Logout()
        {
            _authProvider.Logout();

            return RedirectToAction("Login", "Admin");
        }
        [HttpPost, ValidateInput(false)]
        public ContentResult AddPost(Posts post)
        {
            string json;

            ModelState.Clear();

            if (TryValidateModel(post))
            {
                var id = _blogRepository.CreatePost(post);

                json = JsonConvert.SerializeObject(new
                {
                    id = id,
                    success = true,
                    message = "Post added successfully."
                });
            }
            else
            {
                json = JsonConvert.SerializeObject(new
                {
                    id = 0,
                    success = false,
                    message = "Failed to add the post."
                });
            }

            return Content(json, "application/json");
        }
       
        public ContentResult Posts(JqInViewModel jqParams)
        {
            var posts = _blogRepository.Posts(jqParams.page - 1, jqParams.rows, jqParams.sidx, jqParams.sord == "asc");
            var totalPosts = _blogRepository.TotalPosts(false);
            var post = (from i in posts
                        select new
                        {
                            id = i.id.ToString(),
                            Urlslug = i.UrlSlug,
                            Title = i.Title,
                            CategoryId = i.Category.id,
                            Body = i.Body,
                            UrlSlug=i.UrlSlug,
                           
                            CreatedOn = i.CreatedOn,
                            Published = i.Published,
                            Header = i.Header

                        }
                    ).ToList();
            var result = Content(JsonConvert.SerializeObject(new
            {
                page = jqParams.page,
                records = totalPosts,
              rows = post,
                total = Math.Ceiling(Convert.ToDouble(totalPosts) / jqParams.rows)
            }), "application/json");
            return result;
        }
       
        [HttpPost]
        public ContentResult DeletePost(string id)
        {
            _blogRepository.DeletePost(new Guid(id));

            var json = JsonConvert.SerializeObject(new
            {
                success = true,
                message = "Post deleted successfully."
            });

            return Content(json, "application/json");
        }
       
        [HttpPost, ValidateInput(false)]
        public ContentResult EditPost(Posts post)
        {
            string json;
             ModelState.Clear();

            if (TryValidateModel(post))
            {
                _blogRepository.CreatePost(post);
                json = JsonConvert.SerializeObject(new
                {
                    id = post.id,
                    success = true,
                    message = "Changes saved successfully."
                });
            }
            else
            {
                json = JsonConvert.SerializeObject(new
                {
                    id = 0,
                    success = false,
                    message = "Failed to save the changes."
                });
            }

            return Content(json, "application/json");
        }
        [ValidateInput(false)]
        [HttpPost]
        public ContentResult AddCategory([Bind(Exclude = "id")]Categories category)
        {
            string json;

            if (TryValidateModel(category))
            {
                var id = _catRepository.CreateCategory(category);
                json = JsonConvert.SerializeObject(new
                {
                    id = id,
                    success = true,
                    message = "Category added successfully."
                });
            }
            else
            {
                json = JsonConvert.SerializeObject(new
                {
                    id = 0,
                    success = false,
                    message = "Failed to add the category."
                });
            }

            return Content(json, "application/json");
        }

        public ContentResult Services()
        {
            var sevices = _servicesRepository.ListOfService();

            return Content(JsonConvert.SerializeObject(new
            {
                page = 1,
                records = sevices.Count,
                rows = sevices,
                total = 1
            }), "application/json");

        }
        [ValidateInput(false)]
        [HttpPost]
        public ContentResult AddService(Uslugis service)
        {
            string json;

            ModelState.Clear();

            if (TryValidateModel(service))
            {
                var id = _servicesRepository.CreateService(service);

                json = JsonConvert.SerializeObject(new
                {
                    id = id,
                    success = true,
                    message = "Post added successfully."
                });
            }
            else
            {
                json = JsonConvert.SerializeObject(new
                {
                    id = 0,
                    success = false,
                    message = "Failed to add the post."
                });
            }

            return Content(json, "application/json");
        }
        public ContentResult DeleteServices(string  id)
        {
            _servicesRepository.DeleteServices(new Guid(id));

            var json = JsonConvert.SerializeObject(new
            {
                success = true,
                message = "Category deleted successfully."
            });

            return Content(json, "application/json");
        }
        [ValidateInput(false)]
        [HttpPost]
        public ContentResult EditServices(Uslugis service)
        {
            string json;

            if (ModelState.IsValid)
            {
                _servicesRepository.CreateService(service);
                json = JsonConvert.SerializeObject(new
                {
                    id = service.id,
                    success = true,
                    message = "Changes saved successfully."
                });
            }
            else
            {
                json = JsonConvert.SerializeObject(new
                {
                    id = 0,
                    success = false,
                    message = "Failed to save the changes."
                });
            }

            return Content(json, "application/json");
        }
        public ContentResult Categories()
        {
            var categories = _blogRepository.CategoriesList();

            return Content(JsonConvert.SerializeObject(new
            {
                page = 1,
                records = categories.Count,
                rows = categories,
                total = 1
            }), "application/json");

        }
        public ContentResult DeleteCategory(string id)
        {
            _blogRepository.DeleteCategory(new Guid(id));

            var json = JsonConvert.SerializeObject(new
            {
                success = true,
                message = "Category deleted successfully."
            });

            return Content(json, "application/json");
        }
        [ValidateInput(false)]
        [HttpPost]
        public ContentResult EditCategory(Categories category)
        {
            string json;

            if (ModelState.IsValid)
            {
                _catRepository.CreateCategory(category);
                json = JsonConvert.SerializeObject(new
                {
                    id = category.id,
                    success = true,
                    message = "Changes saved successfully."
                });
            }
            else
            {
                json = JsonConvert.SerializeObject(new
                {
                    id = 0,
                    success = false,
                    message = "Failed to save the changes."
                });
            }

            return Content(json, "application/json");
        }
        public ContentResult GetCategoriesHtml()
        {
            var categories = _blogRepository.CategoriesList().OrderBy(s => s.Name);

            var sb = new StringBuilder();
            sb.AppendLine(@"<select>");

            foreach (var category in categories)
            {
                sb.AppendLine(string.Format(@"<option value=""{0}"">{1}</option>", category.id, category.Name));
            }

            sb.AppendLine("<select>");
            return Content(sb.ToString(), "text/html");
        }
        public ContentResult GetPhotoAlbumsHtml()
        {
            var photoAlbumses = _photoRepository.GetAlbums().OrderBy(s => s.Name);

            var sb = new StringBuilder();
            sb.AppendLine(@"<select>");

            foreach (var category in photoAlbumses)
            {
                sb.AppendLine(string.Format(@"<option value=""{0}"">{1}</option>", category.id, category.Name));
            }

            sb.AppendLine("<select>");
            return Content(sb.ToString(), "text/html");
        }
        public ActionResult GoToPost(Guid id)
        {
            var post = _blogRepository.Post(id);
            return RedirectToRoute(new { controller = "Blog", action = "Post", year = post.CreatedOn.Year, month = post.CreatedOn.Month, title = post.UrlSlug });
        }
       

    }
}