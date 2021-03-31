using Common.Entities;
using System;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IRecordRepository : IRepository<Record>
    {
        void Delete(Record record);

        IEnumerable<Record> GetByDate(DateTime startDate, DateTime endDate);
    }
}
