using BLL.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Common.Enums;
using UI.Models;

namespace UI.ViewModels
{
    public class CategoriesListViewModel : INotifyPropertyChanged
    {
        private readonly CategoryService _categoryService;

        private CategoryModel _currentCategoryModel;

        public CategoriesListViewModel(CategoryService categoryService)
        {
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));

            ExpensesCategoryModels = new ObservableCollection<CategoryModel>();

            foreach (var category in _categoryService.GetByGroup(CategoryGroups.Expenses))
            {
                ExpensesCategoryModels.Add(new CategoryModel(category));
            }

            IncomeCategorYModels = new ObservableCollection<CategoryModel>();

            foreach (var category in _categoryService.GetByGroup(CategoryGroups.Income))
            {
                IncomeCategorYModels.Add(new CategoryModel(category));
            }
        }

        public ObservableCollection<CategoryModel> ExpensesCategoryModels { get; set; }

        public ObservableCollection<CategoryModel> IncomeCategorYModels { get; set; }

        public CategoryModel CurrentCategoryModel
        {
            get => _currentCategoryModel;
            set
            {
                _currentCategoryModel = value;
                RaisePropertyChanged();
            }
        }

        public bool IsCurrentCategoryModelNotNull => _currentCategoryModel != null;

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
