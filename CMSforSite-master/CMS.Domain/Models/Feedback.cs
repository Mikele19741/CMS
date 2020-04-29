using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Domain.Models
{
     public class Feedback
    {
        public Guid id { get; set; }
        public string Name { get; set; }
        public string LinkProfile { get; set; }
        public string Body { get; set; }
        public DateTime PostedOn { get; set; }
    }
}
