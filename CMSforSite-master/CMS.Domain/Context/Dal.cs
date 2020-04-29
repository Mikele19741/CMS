using CMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Domain.Context
{
  public  class Dal :DbContext
    {
            public DbSet<Posts> Posts { get; set; }
            public DbSet<Categories> Categories { get; set; }
            public DbSet<Feedback> Feedback { get; set; }
        public DbSet<Albums> Albums { get; set; }
        public DbSet<Photos> Photos { get; set; }
        public DbSet<Uslugis> Uslugis { get; set; }

        public DbSet<Searches> Search { get; set; }


    }
}
