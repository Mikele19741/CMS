using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Domain.Context;
using CMS.Domain.Interface;
using CMS.Domain.Interfaces;
using CMS.Domain.Models;

namespace CMS.Domain.Classes
{
    public class UsligiRepository : IUsligi
    {
        private Dal context = new Dal();
        public Guid CreateService(Uslugis uslugis)
        {
            if (uslugis.id == Guid.Empty)
            {
                uslugis.id = Guid.NewGuid();
                
               
                // post.Category = null;
                context.Uslugis.Add(uslugis);
                context.SaveChanges();
            }
            else
            {
               //  post.Category = null;

                context.Entry(uslugis).State = EntityState.Modified;
                context.SaveChanges();
            }
            return uslugis.id; ;
        }

        public void DeleteServices(Guid id)
        {
            var uslugi = context.Uslugis.FirstOrDefault(p => p.id == id);
            context.Uslugis.Remove(uslugi ?? throw new InvalidOperationException());
            context.SaveChanges();
        }

        public IList<Uslugis> ListOfService()
        {
            return context.Uslugis.ToList();
        }

        public Uslugis Service(Guid id)
        {
           return context.Uslugis.FirstOrDefault(p => p.id == id);
        }
    }
}
