﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Sunridge.Models.Models
{
    public class AuthSenderOptions
    {
        private string user = "Rich Fry"; // The name you want to show up on your email
                                          // Make sure the string passed in below matches your API Key
        
        private string key = "SG.n_u_YourAPIKeyExample";

        public string SendGridUser { get { return user; } }

        public string SendGridKey { get { return key; } }

    }
}
