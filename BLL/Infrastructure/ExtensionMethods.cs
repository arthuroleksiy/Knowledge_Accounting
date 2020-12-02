using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Infrastructure
{
    public static class ExtensionMethods
    {
        public static User WithoutPassword(this User user)
        {
            if (user == null) return null;

            user.Password = null;
            return user;
        }
    }
}
