using Common.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;

namespace BLL.Services
{
    public class RecordService
    {
        private readonly IRecordRepository _repository;

        public RecordService(IRecordRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Record> GetAll()
        {
            return _repository.GetAll();
        }

        public Record Add(Record record)
        {
            return _repository.Add(record);
        }

        public Record Update(Record record)
        {
            return _repository.Update(record);
        }

        public Record GetById(long id)
        {
            return _repository.GetById(id);
        }

        public IEnumerable<Record> GetByDate(DateTime startDate, DateTime endDate)
        {
            return _repository.GetByDate(startDate, endDate);
        }

        public void Delete(Record record)
        {
            _repository.Delete(record);
        }
    }
}
