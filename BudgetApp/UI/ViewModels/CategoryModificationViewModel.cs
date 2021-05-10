using BLL.Services;
using Common.Enums;
using System;
using System.Collections.Generic;
using UI.Models;
using System.Linq;

namespace UI.ViewModels
{
    public class CategoryModificationViewModel
    {
        private readonly CategoryService _categoryService;

        public CategoryModificationViewModel(CategoryModel categoryModel, CategoryService categoryService)
        {
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));

            CategoryModel = categoryModel ?? new CategoryModel(null);

            SetCategoryGroupsNames();
        }

        public CategoryModel CategoryModel { get; set; }

        public List<CategoryGroups> CategoryGroups { get; private set; }

        private RelayCommand _saveCommand;

        public RelayCommand SaveCommand
        {
            get
            {
                return _saveCommand ??= new RelayCommand(
                    (obj) =>
                    {
                        if (CategoryModel.Id == 0)
                        {
                            _categoryService.Add(CategoryModel.GetCategory());
                        }
                        else
                        {
                            _categoryService.Update(CategoryModel.GetCategory());
                        }

                    }, 
                    null
                    );
            }
        }

        private RelayCommand _deleteCommand;

        public RelayCommand DeleteCommand
        {
            get
            {
                return _deleteCommand ??= new RelayCommand(
                    (obj) => _categoryService.Delete(CategoryModel.GetCategory()),
                    (obj) => _categoryService.CanDelete(CategoryModel.GetCategory())
                    );
            }
        }

        private void SetCategoryGroupsNames()
        {
            CategoryGroups = new List<CategoryGroups>(Enum.GetValues(typeof(CategoryGroups)).Cast<CategoryGroups>());
        }
    }
}
