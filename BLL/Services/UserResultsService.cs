using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class UserResultsService
    {
        IUnitOfWork UnitOfWork { get; set; }
        
        public UserResultsService(IUnitOfWork UnitOfWork)
        {
            this.UnitOfWork = UnitOfWork;
        }

        //public Task Add

    }
}
