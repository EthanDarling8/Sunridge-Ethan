using System;
using System.Collections.Generic;
using System.Text;

namespace Sunridge.Models.Models
{
    public class AuthSenderOptions
    {
        private string user = "Sunridge Webmaster";

        private string key = "M00$elakeM00$lake";

        public string SendGridUser { get { return user; } }

        public string SendGridKey { get { return key; } }

    }
}
