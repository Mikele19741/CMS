using CMS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Domain.Models;
using CMS.Domain.Context;
using System.Data.Entity;

namespace CMS.Domain.Classes
{
    public class PostsRepository : IPosts
    {
        private Dal context = new Dal();
       

        public Guid CreatePost(Posts post)
        {
            if (post.id == Guid.Empty)
            {
                post.id = Guid.NewGuid();
                post.CreatedOn = DateTime.Now;
                post.Category = cat(post.Category.id);
                post.Album = alb(post.Album.id);
               // post.Category = null;
                context.Posts.Add(post);
                context.SaveChanges();
            }
            else
            {
                
                post.CreatedOn = DateTime.Now;
              
              //  post.Category = null;
           
               
                var newcat = cat(post.Category.id);
                post.Category = newcat;
                var newalb = alb(post.Album.id);
                post.Album = newalb;
                context.Entry(post).State = EntityState.Modified;

                context.SaveChanges();
            }
            return post.id;
        }
      

        public IList<Posts> PostsList()
        {
            var result = new List<Posts>();
                result=context.Posts.ToList();
            return result;
        }
        public IList<Categories> CategoriesList()
        {
            return context.Categories.OrderBy(p => p.Name).ToList();
        }

        
        public IList<Posts> PostsForCategory(string categorySlug)
        {
            return context.Posts.Where(p => p.Category.Name == categorySlug).ToList();
        }

        public IList<Posts> Posts(int pageNo, int pageSize, string sortColumn, bool sortByAscending)
        {
            IList<Posts> posts=new List<Posts>();
            IList<Guid> postIds;
            switch (sortColumn)
            {
                case "Title":
                    if (sortByAscending)
                    {
                        posts = context.Posts.OrderBy(p => p.Title).Skip(pageNo * pageSize).Take(pageSize).ToList();

                        postIds = posts.Select(p => p.id).ToList();

                        posts = context.Posts.Where(p => postIds.Contains(p.id)).OrderBy(p => p.Title).ToList();
                    }
                    else
                    {
                        posts = context.Posts.OrderByDescending(p => p.Title).Skip(pageNo * pageSize).Take(pageSize).ToList();

                        postIds = posts.Select(p => p.id).ToList();

                        posts = context.Posts.Where(p => postIds.Contains(p.id))
                                         .OrderByDescending(p => p.Title)
                                         .ToList();
                    }
                    break;
                case "Published":
                    if (sortByAscending)
                    {
                        posts = context.Posts.OrderBy(p => p.Published)
                                        .Skip(pageNo * pageSize)
                                        .Take(pageSize)
                                        .ToList();

                        postIds = posts.Select(p => p.id).ToList();

                        posts = context.Posts
                                         .Where(p => postIds.Contains(p.id))
                                         .OrderBy(p => p.Published)
                                         .ToList();
                    }
                    else
                    {
                        posts = context.Posts
                                        .OrderByDescending(p => p.Published)
                                        .Skip(pageNo * pageSize)
                                        .Take(pageSize)
                                        .ToList();

                        postIds = posts.Select(p => p.id).ToList();

                        posts =context.Posts
                                         .Where(p => postIds.Contains(p.id))
                                         .OrderByDescending(p => p.Published)
                                         .ToList();
                    }
                    break;
                case "CreatedOn":
                    if (sortByAscending)
                    {
                        posts = context.Posts
                                        .OrderBy(p => p.CreatedOn)
                                        .Skip(pageNo * pageSize)
                                        .Take(pageSize)
                                        .ToList();

                        postIds = posts.Select(p => p.id).ToList();

                        posts = context.Posts
                                         .Where(p => postIds.Contains(p.id))
                                         .OrderBy(p => p.CreatedOn)
                                       
                                         .ToList();
                    }
                    else
                    {
                        posts = context.Posts
                                        .OrderByDescending(p => p.CreatedOn)
                                        .Skip(pageNo * pageSize)
                                        .Take(pageSize)
                                        
                                        .ToList();

                        postIds = posts.Select(p => p.id).ToList();

                        posts = context.Posts
                                        .Where(p => postIds.Contains(p.id))
                                        .OrderByDescending(p => p.CreatedOn)
                                     
                                        .ToList();
                    }
                    break;
                
                default:
                    posts = context.Posts.OrderByDescending(p => p.CreatedOn)
                                    .Skip(pageNo * pageSize)
                                    .Take(pageSize)
                                   
                                    .ToList();

                    postIds = posts.Select(p => p.id).ToList();

                    posts = context.Posts.Where(p => postIds.Contains(p.id))
                                     .OrderByDescending(p => p.CreatedOn)
                                    .ToList();
                    break;
            }
            return posts;
        }
        public int TotalPosts(bool checkIsPublished = true)
        {
            return context.Posts.Where(p => !checkIsPublished || p.Published == true).Count();
        }

        public void DeletePost(Guid id)
        {
            var post = context.Posts.Where(p => p.id == id).FirstOrDefault();
            context.Posts.Remove(post);
            context.SaveChanges();
        }

        public void DeleteCategory(Guid id)
        {
            var cat = context.Categories.Where(p => p.id == id).FirstOrDefault();
        }

        public Posts Post(Guid id)
        {

            return context.Posts.Where(p => p.id == id).FirstOrDefault();
        }

        public Posts Post(int year, int month, string title)
        {
            return context.Posts
                           .Where(p => p.CreatedOn.Year == year && p.CreatedOn.Month == month && p.UrlSlug.Equals(title))
                           
                           .FirstOrDefault();
        }

        public Categories cat(Guid id)
        {
            
           var cat= context.Categories.Where(p => p.id == id).FirstOrDefault();
            return cat;
        }
        public Albums alb(Guid id)
        {

            var cat = context.Albums.Where(p => p.id == id).FirstOrDefault();
            return cat;
        }


    }
}
