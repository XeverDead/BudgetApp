using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Entities
{
    [Table("Records")]
    public class Record : IAppEntity
    {
        public Record()
        {
            CategoryRecords = new List<CategoryRecord>();
        }

        public long Id { get; set; }

        public List<CategoryRecord> CategoryRecords { get; set; }

        public string Note { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public object Clone()
        {
            //var categoryRecords = new List<CategoryRecord>();

            //foreach (var categoryRecord in CategoryRecords)
            //{
            //    categoryRecords.Add((CategoryRecord)categoryRecord.Clone());
            //}

            //var record = new Record
            //{
            //    Id = Id,
            //    CategoryRecords = categoryRecords,
            //    Date = Date,
            //    Note = Note
            //};

            return MemberwiseClone();
        }
    }
}
