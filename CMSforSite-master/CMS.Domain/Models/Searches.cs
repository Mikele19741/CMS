using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Domain.Models
{
   public class Searches
    {
        public Guid id { get; set; }
        public string SearchWord { get; set; }
        public string EntityName { get; set; }
    }
}
