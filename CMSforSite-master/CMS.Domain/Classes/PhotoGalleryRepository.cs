using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using CMS.Domain.Context;
using CMS.Domain.Interfaces;
using CMS.Domain.Models;

namespace CMS.Domain.Classes
{
    public class PhotoGalleryRepository : IPhoto
    {
        private readonly Dal _context = new Dal();

        public List<Albums> AlbumsList()
        {
            return _context.Albums.ToList();
        }

        public IList<Photos> AllPhotos()
        {
            return _context.Photos.ToList();
        }

        public Guid Change(Photos id)
        {

            _context.Entry(id).State = EntityState.Modified;
            _context.SaveChanges();
            return id.id;
        }
        public void DeleteAlbums(Guid id)
        {
            var result = _context.Albums.FirstOrDefault(p => p.id == id);
            _context.Albums.Remove(result);
            _context.SaveChanges();
        }
        public void DeletePhoto(Guid id)
        {
            var result = _context.Photos.FirstOrDefault(p => p.id == id);
            _context.Photos.Remove(result);
            _context.SaveChanges();
        }

        public IList<Albums> GetAlbums()
        {
            return _context.Albums.ToList();
        }

        public Photos photo(Guid id)
        {
            var result = _context.Photos.FirstOrDefault(p => p.id == id);
            return result;
        }

        public Albums AddAlbum(Albums album)
        {
            try
            {
                if (album.id == Guid.Empty)
                {
                    album.id = Guid.NewGuid();
                 
                    _context.Albums.Add(album);
                    _context.SaveChanges();
                }
                else
                {
                    _context.Entry(album).State = EntityState.Modified;
                    _context.SaveChanges();
                }
            }
            catch (Exception e)
            {

                throw new ApplicationException(e.Message);
            }

            return album;
        }
        public Guid AddPhoto(Photos photo, string contentType, byte[] Bytes, HttpPostedFileBase image)
        {
            photo.id = Guid.NewGuid();
            photo.ImageMimeType = contentType;
            photo.Photo = Bytes;
            image.InputStream.Read(photo.Photo, 0, image.ContentLength);
            _context.Photos.Add(photo);
            _context.SaveChanges();

            return photo.id;
        }



        public IList<Photos> PhotosForAlbum(Guid album)
        {
            var result = album == null ? _context.Photos.ToList() : _context.Photos.Where(p => p.Albums.id == album).ToList();

            return result;
        }

        public IEnumerable<Photos> GetPhotos()
        {
            return _context.Photos;
        }
    }
}
