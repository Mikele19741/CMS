using CMS.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Domain
{
  public  class Categories
    {
        public virtual Guid id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Meta { get; set; }
        public virtual string NameEN { get; set; }
       public virtual string Description { get; set; }
       public virtual string Location { get; set; } 
       public virtual string UrlSlug { get; set; }
        [JsonIgnore]
        public virtual IList<Posts> Posts
        { get; set; }
        [JsonIgnore]
        public virtual Albums Album { get; set; } 
    }
}
