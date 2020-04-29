using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Domain.Classes;

namespace CMS.Domain.Models
{

    public class Posts
    {
    
        public virtual Guid id { get; set; }
        public virtual string Title { get; set; }
       
        public virtual string Header { get; set; }
        public virtual string Body { get; set; }
       
        public virtual DateTime CreatedOn { get; set; }
       
        public virtual string UrlSlug { get; set; }

        public virtual bool Published { get; set; }
        [JsonIgnore]
        public virtual Categories Category { get; set; }

        [JsonIgnore]
        public virtual Albums Album { get; set; }
        public virtual string Meta { get; set; }=String.Empty;
    }
}
