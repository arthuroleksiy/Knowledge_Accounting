using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Entities.Results;

namespace DAL.Interfaces
{
    public interface ITestResultsRepository : IRepository<KnowledgeResult>
    {
        public IQueryable<KnowledgeResult> FindByDateAndPoint(int minimalPoint, int maxPoint, DateTime minDate, DateTime maxDate);
    }
}
