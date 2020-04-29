using CMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CMS.Domain.Interfaces
{
   public interface IPhoto
    {
        IList<Photos> PhotosForAlbum(Guid album);
        List<Albums> AlbumsList();
        IList<Albums> GetAlbums();
        IEnumerable<Photos> GetPhotos();
        Albums AddAlbum(Albums album);
        Guid AddPhoto(Photos photo, string contentType, byte[] Bytes, HttpPostedFileBase image);
        IList<Photos> AllPhotos();
        void DeletePhoto(Guid id);
        Photos photo(Guid id);
        void DeleteAlbums(Guid id);
        Guid Change(Photos id);
    }
}
