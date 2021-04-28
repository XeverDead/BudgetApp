﻿using Common.Entities;
using Common.Enums;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DAL.EFRepositories
{
    public class EFCategoryRepository : ICategoryRepository
    {
        private readonly BudgetContext _dbContext;

        public EFCategoryRepository(BudgetContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Category> GetAll()
        {
            return _dbContext.Categories.ToList();
        }

        public Category Add(Category category)
        {
            var changedCategoryEntry = _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();

            return changedCategoryEntry.Entity;
        }

        public Category Update(Category category)
        {
            var changedCategoryEntry = _dbContext.Categories.Update(category);
            _dbContext.SaveChanges();

            return changedCategoryEntry.Entity;
        }

        public Category GetById(long id)
        {
            return _dbContext.Categories.FirstOrDefault(category => category.Id == id);
        }

        public IEnumerable<Category> GetByGroup(CategoryGroups group)
        {
            return _dbContext.Categories.Where(category => category.Group == group).ToList();
        }
    }
}
