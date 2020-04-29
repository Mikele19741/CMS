using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CMS.Domain;
using CMS.Domain.Interfaces;
using CMS.Domain.Models;
using PagedList;

namespace CMS.Web.Models
{
    public class PhotoViewModel
    {
        MappingEntities mapping = new MappingEntities();
        public PhotoViewModel(int year, int month, string title, IPhoto photoRepository, IPosts blogRepository)
        {
            LatestPosts = blogRepository.Post(year, month, title);
            if (LatestPosts != null)
            {
                var post = mapping.GetPostsById(LatestPosts.id);
                if (post.Album != null)
                    Photos = photoRepository.PhotosForAlbum(post.Album.id);
            }

           
        }
        public PhotoViewModel(Guid Id, IPhoto photoRepository, ICategories categoryRepository)
        {
            Categories = categoryRepository.Category(Id);
            if (Categories != null)
            {
                var post = mapping.GetCategoryById(Categories.id);
                if (post.Album != null)
                {
                    var album = photoRepository.PhotosForAlbum(post.Album.id);
                    if (album != null)
                    {
                        Photos = photoRepository.PhotosForAlbum(post.Album.id);
                    }
                }
                   

            }
        }


        public IList<Photos> Photos
        { get; private set; }
        public Categories Categories
        { get; private set; }


        public Posts LatestPosts
        { get; private set; }
    }
}