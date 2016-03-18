using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace KinderhuisStageOpdracht.Helpers
{
    public class GebruikerHelper
    {
        public static string CreatePasswordHash(string pwd, string salt)
        {
            var saltAndPwd = String.Concat(pwd, salt);
#pragma warning disable 618
            var hashedPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(
#pragma warning restore 618
saltAndPwd, "sha1");
            return hashedPwd;
        }
    }
}