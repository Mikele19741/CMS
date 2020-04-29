using CMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Domain.Interfaces
{
  public interface IAdmin
    {
        bool IsLoggedIn { get; }
        bool Login(string username, string password);
        void Logout();
        IList<Searches> FillSearchTable(string Word);
      

    }
}
