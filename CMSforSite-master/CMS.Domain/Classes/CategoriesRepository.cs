using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Domain.Context;
using CMS.Domain.Interfaces;

namespace CMS.Domain.Classes
{
    public class CategoriesRepository : ICategories
    {
        Dal context = new Dal();
        public List<Categories> Categories()
        {
            return context.Categories.ToList();
        }

        public Categories Category(Guid id)
        {
            return (Categories)context.Categories.FirstOrDefault(t => t.id == id);
        }
        public Guid CreateCategory(Categories cat)
        {
            if (cat.id == Guid.Empty)
            {
                cat.id = Guid.NewGuid();

                context.Categories.Add(cat);
                ///// cat.Album = alb(cat.Album.id);
                context.SaveChanges();
            }
            else
            {

                context.Entry(cat).State = EntityState.Modified;
                //var newalb = alb(cat.Album.id);
                //cat.Album = newalb;
                context.SaveChanges();
            }
            return cat.id;
        }


    }
}
