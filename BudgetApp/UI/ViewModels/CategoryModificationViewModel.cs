using BLL.Services;
using System;
using UI.Models;

namespace UI.ViewModels
{
    public class CategoryModificationViewModel
    {
        private readonly CategoryService _categoryService;

        public CategoryModificationViewModel(CategoryModel categoryModel, CategoryService categoryService)
        {
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));

            CategoryModel = categoryModel ?? new CategoryModel(null);
        }

        public CategoryModel CategoryModel { get; set; }

        private RelayCommand _saveCommand;

        public RelayCommand SaveCommand
        {
            get
            {
                return _saveCommand ??= new RelayCommand(
                    (obj) =>
                    {
                        if (CategoryModel.Id <= 0)
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
    }
}
