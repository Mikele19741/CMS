using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CMS.Domain.Models
{
  public class Albums
    {
        public Guid id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public virtual IList<Posts> Posts
        { get; set; }
    }
}
