using CMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Domain.Interfaces
{
    public interface IPosts
    {
        IList<Posts> PostsList();
        IList<Posts> PostsForCategory(string categorySlug);
        Guid CreatePost(Posts post);
        //Guid CreateCategory(Categories cat);
        IList<Categories> CategoriesList();
        IList<Posts> Posts(int pageNo, int pageSize, string sortColumn, bool sortByAscending);
        int TotalPosts(bool checkIsPublished = true);
        void DeletePost(Guid id);
        void DeleteCategory(Guid guid);
        Posts Post(Guid id);
        Posts Post(int year, int month, string title);
        Categories cat(Guid id);
       
    }
}
