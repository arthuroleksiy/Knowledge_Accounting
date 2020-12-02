using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
namespace DAL.Entities
{
    public class UserRole : IdentityUserRole<int>
    {
        public UserRole() : base()
        {

        }
    }
}
