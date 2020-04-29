using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.Web.Models
{
    public class JqInViewModel
    {
        public int rows { get; set; }

        /// <summary>
        /// Page number.
        /// </summary>
        public int page { get; set; }

        /// <summary>
        /// Sort column name.
        /// </summary>
        public string sidx { get; set; }

        /// <summary>
        /// Sort direction (ex. asc).
        /// </summary>
        public string sord { get; set; }
    }
}