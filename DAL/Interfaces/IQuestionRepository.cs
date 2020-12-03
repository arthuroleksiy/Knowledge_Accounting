using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IQuestionRepository : IRepository<Question>
    {

        public Task DeleteAnswerByQuestionIdAsync(int id);
            
     }
}
