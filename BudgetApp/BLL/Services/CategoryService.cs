using Common.Entities;
using Common.Enums;
using DAL.Interfaces;
using System.Collections.Generic;

namespace BLL.Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Category> GetAll()
        {
            return _repository.GetAll();
        }

        public Category Add(Category category)
        {
            return _repository.Add(category);
        }

        public Category Update(Category category)
        {
            return _repository.Update(category);
        }

        public Category GetById(long id)
        {
            return _repository.GetById(id);
        }

        public IEnumerable<Category> GetByGroup(CategoryGroups group)
        {
            return _repository.GetByGroup(group);
        }
    }
}
