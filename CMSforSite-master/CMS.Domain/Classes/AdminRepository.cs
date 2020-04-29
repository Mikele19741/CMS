using CMS.Domain.Context;
using CMS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using CMS.Domain.Models;

namespace CMS.Domain.Classes
{
    public class AdminRepository :IAdmin
    {
        public bool IsLoggedIn
        {
            get
            {
                return HttpContext.Current.User.Identity.IsAuthenticated;
            }
        }
        Dal context = new Dal();
        /// <summary>
        /// Authenticate an user and set cookie if user is valid.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool Login(string username, string password)
        {
            username = "admin";
            password = "E785bo21zx";
            var result = true;//FormsAuthentication.Authenticate(username, password); // TODO: User Membership APIs

            if (result)
                FormsAuthentication.SetAuthCookie(username, false);

            return result;
        }

        /// <summary>
        /// Logout the user.
        /// </summary>
        public void Logout()
        {
            FormsAuthentication.SignOut();
        }

        public IList<Searches> FillSearchTable(string Word)
        {
            System.Data.SqlClient.SqlParameter param = new System.Data.SqlClient.SqlParameter("@Word", Word);
            var x = new List<Searches>();
            try
            {
                 x = context.Database.SqlQuery<Searches>("p_Search @Word", param).ToList();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return x;     
        }

       
    }
}
