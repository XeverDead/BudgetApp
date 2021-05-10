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
        private CategoryService _categoryService;

        private CategoryModel _currentCategoryModel;

        public CategoriesListViewModel(CategoryService categoryService)
        {
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));

            ExpensesCategoryModels = new ObservableCollection<CategoryModel>();
            IncomeCategoryModels = new ObservableCollection<CategoryModel>();

            SetCategoryModels();
        }

        public ObservableCollection<CategoryModel> ExpensesCategoryModels { get; set; }

        public ObservableCollection<CategoryModel> IncomeCategoryModels { get; set; }

        public CategoryModel CurrentCategoryModel
        {
            get => _currentCategoryModel;
            set
            {
                _currentCategoryModel = value;
                IsCurrentCategoryModelNotNull = _currentCategoryModel != null;
                RaisePropertyChanged();
            }
        }

        public bool IsCurrentCategoryModelNotNull
        {
            get => _currentCategoryModel != null;
            set
            {
                RaisePropertyChanged();
            }
        }

        public void UpdateServices(CategoryService categoryService)
        {
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));

            SetCategoryModels();
        }

        private void SetCategoryModels()
        {
            ExpensesCategoryModels.Clear();

            foreach (var category in _categoryService.GetByGroup(CategoryGroups.Expenses))
            {
                ExpensesCategoryModels.Add(new CategoryModel(category));
            }

            IncomeCategoryModels.Clear();

            foreach (var category in _categoryService.GetByGroup(CategoryGroups.Income))
            {
                IncomeCategoryModels.Add(new CategoryModel(category));
            }

            CurrentCategoryModel = null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
