using Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Entities
{
    [Table("Categories")]
    public class Category : IAppEntity
    {
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        public CategoryGroups Group { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
