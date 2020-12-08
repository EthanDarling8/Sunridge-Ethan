using System;
using System.Collections.Generic;
using System.Text;

namespace Sunridge.Models.Models
{
    public class AuthSenderOptions
    {
        private string user = "Brayden Perry"; // The name you want to show up on your email
                                               // Make sure the string passed in below matches your API Key

        // private string key = "M00$elakeM00$lake";

        private string key = "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";

        public string SendGridUser { get { return user; } }

        public string SendGridKey { get { return key; } }

    }
}
