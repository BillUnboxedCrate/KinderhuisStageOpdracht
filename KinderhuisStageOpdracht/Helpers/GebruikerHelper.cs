using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Security;

namespace KinderhuisStageOpdracht.Helpers
{
    public class GebruikerHelper
    {
        public static string CreatePasswordHash(string pwd, string salt)
        {
            var saltAndPwd = String.Concat(pwd, salt);
            var hashedPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(
                saltAndPwd, "sha1");
            return hashedPwd;
        }
    }
}