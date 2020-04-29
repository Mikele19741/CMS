using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Domain.Models
{
    public class Photos
    {
        public Guid id { get; set; }

        public byte[] Photo { get; set; }
        [Required(ErrorMessage = "Поле должно быть установлено")]
        public string Description { get; set; }
        public Guid? Albumsid { get; set; }
        public virtual Albums Albums { get; set; }
        public string ImageMimeType { get; set; }
    }
}
