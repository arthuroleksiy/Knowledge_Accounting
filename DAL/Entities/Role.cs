using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class Role: BaseEntity
    {
        public string Name { get; set; }
        public string NormalizedName { get; set; }
    }
}
