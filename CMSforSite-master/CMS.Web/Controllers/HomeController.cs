using CMS.Domain.Interfaces;
using CMS.Domain.Models;
using CMS.Web.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CMS.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPosts _blogRepository;
             private readonly IAdmin _adminRepository;
        public HomeController(IPosts blogRepository, IAdmin adminRepository)
        {
            _blogRepository = blogRepository;
            _adminRepository = adminRepository;
        }
        public ActionResult Index()
        {
       
            return View();
        }

        public ActionResult Contact()
        {

            return View();
        }
        [HttpPost]
        public ViewResult Contact(Contact contact)
        {
            if (ModelState.IsValid)
            {
               
                    var adminEmail =ConfigurationManager.AppSettings["AdminEmail"];

                var from = adminEmail;
                var to = adminEmail;

                   
                       
                        MailMessage m = new MailMessage(from, to);
                        m.Subject = $"{contact.Name} {contact.Subject} {contact.Email}";
                        m.Body = contact.Body;
                        SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                        smtp.Credentials = new NetworkCredential("gidvietnam2018@gmail.com", "E785bo21zx");
                        smtp.EnableSsl = true;
                        
                        try
                        {
                            smtp.Send(m);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.ToString());
                        }
                    
                

                return View("Thanks");
            }

            return View();
        }
        public ViewResult Searches(string s, int? page)
        {

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            
            var res = _adminRepository.FillSearchTable(s);
            if (res.Count> 0)
            {
                return View("Searches", res.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                return View("Searches");
            }
        }
        public RedirectToRouteResult News(Guid idd)
        {

          return   RedirectToAction("Art", "Feedback", new {id = idd});


        }
        public RedirectToRouteResult Posts(Guid idd)
        {

            return RedirectToAction("Art", "Blog", new { id = idd });


        }
     
    }
}