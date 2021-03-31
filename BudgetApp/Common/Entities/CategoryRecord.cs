using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Entities
{
    [Table("CategoryRecords")]
    public class CategoryRecord : IAppEntity
    {
        public long Id { get; set; }

        public long RecordId { get; set; }

        public long CategoryId { get; set; }

        public double Value { get; set; }

        public string Note { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
