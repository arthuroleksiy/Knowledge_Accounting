﻿
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models
{
    public class UserModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
        public string ReturnUrl { get; set; }
    }
}
