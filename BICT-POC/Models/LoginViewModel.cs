using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolAPI.Models
{
    public class LoginViewModel
    {
        public string username { get; set; }
        public string password { get; set; }
        public string ReturnUrl { get; set; }
    }
}