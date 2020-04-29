using CMS.Domain;
using CMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace CMS.Web
{
    public static class ActionLinkExtensions
    {
        public static MvcHtmlString PostLink(this HtmlHelper helper, Posts post)
        {
            return helper.ActionLink(post.Title, "Post", "Blog", new { year = post.CreatedOn.Year, month = post.CreatedOn.Month, title = post.UrlSlug }, new { title = post.Title });
        }

        public static MvcHtmlString CategoryLink(this HtmlHelper helper, Categories category)
        {
            return helper.ActionLink(category.Name, "Category", "Blog", new { category = category.Name }, new { title = String.Format("See all posts in {0}", category.Name) });
        }

      
        public static MvcHtmlString SearchLink(this HtmlHelper helper, Searches search)
        {
            return helper.ActionLink(search.EntityName, search.EntityName, search.EntityName, new { id=search.id});
        }
    }
}