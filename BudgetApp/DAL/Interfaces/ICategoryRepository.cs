using Common.Entities;
using Common.Enums;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        bool CanDelete(Category category);

        void Delete(Category category);

        IEnumerable<Category> GetByGroup(CategoryGroups group);
    }
}
