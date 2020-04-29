using CMS.Domain;
using CMS.Domain.Interfaces;
using CMS.Domain.Models;
using PagedList;
using System.Collections.Generic;

namespace CMS.Web.Models
{
    public class WidgetViewModel
    {
        public WidgetViewModel(IPosts blogRepository)
        {
            Categories = blogRepository.CategoriesList();
           
            LatestPosts = blogRepository.PostsList();
        }
        public WidgetViewModel(ICategories _categories)
        {
            Categories = _categories.Categories();
        }


        public IList<Categories> Categories
        { get; private set; }


        public IList<Posts> LatestPosts
        { get; private set; }

      
    }
}