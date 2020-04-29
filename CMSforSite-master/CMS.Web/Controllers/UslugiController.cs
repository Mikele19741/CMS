using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS.Domain.Interface;

namespace CMS.Web.Controllers
{
    public class UslugiController : Controller
    {
        // GET: Uslugis
        private readonly IUsligi _usligiRepository;
        public UslugiController(IUsligi usligiRepository)
        {
            _usligiRepository = usligiRepository;
        }
        public ViewResult Services(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var uslugi = _usligiRepository.ListOfService();
            if (uslugi.Count > 0)
            {
                return View(uslugi);
            }
            else
            {
                return View();
            }

        }
        public ViewResult Service(Guid id)
        {
            var service = _usligiRepository.Service(id);

            if (service == null)
                throw new HttpException(404, "Post not found");

           
            return View(service);
        }
    }
}