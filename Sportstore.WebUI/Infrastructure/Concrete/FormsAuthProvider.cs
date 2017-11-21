using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sportstore.WebUI.Infrastructure.Abstract;
using System.Web.Security;


namespace Sportstore.WebUI.Infrastructure.Concrete
{
    public class FormsAuthProvider : IAuthProvider
    {
        public bool Authenticate(string username, string password)
        {
            var result = FormsAuthentication.Authenticate(username, password);
            if (result)
            {
                FormsAuthentication.SetAuthCookie(username, false);
            }
            return result;
        }
    }
}