using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS.Domain.Interfaces;
using CMS.Domain.Models;
using Newtonsoft.Json;

namespace CMS.Web.Controllers
{
    //[Authorize]
    public class AdminPhotoController : Controller
    {
        // GET: AdminPhoto


        private readonly IPhoto _photoRepository;
        public AdminPhotoController(IPhoto photoRepository)
        {
            _photoRepository = photoRepository;
        }
        [HttpGet]
        public ViewResult AddAlbum()
        {
            var alb = _photoRepository.GetAlbums();

            SelectList teams = new SelectList(alb, "id", "Name");
            ViewBag.Albums = teams;
            return View("AddAlbum", new Albums());
        }
        [HttpPost]
        public ActionResult AddAlbum(Albums album)
        { 
          var alb= _photoRepository.AddAlbum(album);
            if(alb.id!=Guid.Empty)
            {
                return View();
            }
            else
            {
                return View("Error");
            }
           
        }
        [ValidateInput(false)]
        public ActionResult AllAlbums()
        {
            var albums = _photoRepository.AlbumsList();
            if (albums.Count > 0)
            {
                return View(albums);
            }
            else
            {
                return View();
            }

        }
        [HttpGet]
        public ViewResult AddPhoto()
        {
            var alb = _photoRepository.GetAlbums();

            SelectList teams = new SelectList(alb, "id", "Name");
            ViewBag.Albums = teams;
            return View("AddPhoto", new Photos());
        }
        [HttpPost]
        public ActionResult AddPhoto(Photos photo, HttpPostedFileBase image)
        {
            string json;
            ModelState.Clear();
            if (TryValidateModel(photo))
            {
                var id = _photoRepository.AddPhoto(photo, image.ContentType, new byte[image.ContentLength], image);
               

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
            return RedirectToAction("AllAlbums", "AdminPhoto");

        }
      
        public ActionResult DeletePhoto(Guid id)
        {
            _photoRepository.DeletePhoto(id);
            return RedirectToAction("Photos", "AdminPhoto");
        }
        public ActionResult DeleteAlbum(Guid id)
        {
            _photoRepository.DeleteAlbums(id);
            return RedirectToAction("AllAlbums", "AdminPhoto");
        }
        [HttpGet]
        public ViewResult EditPhoto(Guid id)
        {


            var alb = _photoRepository.GetAlbums();
            var photos = _photoRepository.photo(id);
            ViewBag.Albumsid = new SelectList(alb, "id", "Name", photos.Albumsid);

            return View(photos);

        }
        [HttpPost, ValidateInput(false)]
        public ActionResult EditPhoto(Photos photo)
        {
            string json;
            ModelState.Clear();

            if (TryValidateModel(photo))
            {
                _photoRepository.Change(photo);
                return RedirectToAction("AllPhotos", "AdminPhoto");

            }
            else
            {
                return RedirectToAction("Error", "AdminPhoto");
            }



        }

        public ActionResult Error()
        {
            return View("Error");
        }
        public ActionResult AllPhotos()
        {
            var photos = _photoRepository.AllPhotos();
            if (photos.Count > 0)
            {
                return View(photos);
            }
            else
            {
                return View();
            }

        }
        //public ActionResult PhotoAlbum(Guid album)
        //{
        //    var cat = _photoRepository.PhotosForAlbum(album);
        //    return View("Photos", cat);
        //}
    }
}