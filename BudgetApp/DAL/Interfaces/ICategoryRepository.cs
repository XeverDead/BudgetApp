using Common.Entities;
using Common.Enums;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        IEnumerable<Category> GetByGroup(CategoryGroups group);
    }
}
