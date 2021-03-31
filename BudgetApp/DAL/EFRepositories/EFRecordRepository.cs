using Common.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.EFRepositories
{
    public class EFRecordRepository : IRecordRepository
    {
        private readonly BudgetContext _dbContext;

        public EFRecordRepository(BudgetContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Record> GetAll()
        {
            return _dbContext.Records.ToList();
        }

        public void Add(Record record)
        {
            _dbContext.Records.Add(record);
            _dbContext.SaveChanges();
        }

        public void Update(Record record)
        {
            _dbContext.Records.Update(record);
            _dbContext.SaveChanges();
        }

        public Record GetById(long id)
        {
            return _dbContext.Records.FirstOrDefault(record => record.Id == id);
        }

        public IEnumerable<Record> GetByDate(DateTime startDate, DateTime endDate)
        {
            return _dbContext.Records.Where(record => record.Date >= startDate && record.Date <= endDate);
        }

        public void Delete(Record record)
        {
            _dbContext.Records.Remove(record);
            _dbContext.SaveChanges();
        }
    }
}
