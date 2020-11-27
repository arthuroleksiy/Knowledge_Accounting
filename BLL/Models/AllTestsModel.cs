using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models
{
    public class AllTestsModel
    {
        public int AllTestId { get; set; }
        public ICollection<Knowledge> Tests { get; set; }
    }
}
