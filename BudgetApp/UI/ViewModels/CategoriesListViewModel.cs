using BLL.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Common.Enums;
using UI.Models;
using System.Collections.Generic;
using System.Linq;

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

            //SortCategoryModels(ExpensesCategoryModels);
            //SortCategoryModels(IncomeCategoryModels);

            CurrentCategoryModel = null;
        }

        //private void SortCategoryModels(ObservableCollection<CategoryModel> collection)
        //{
        //    collection = (ObservableCollection<CategoryModel>)(from category in collection
        //                                                       orderby category.Position
        //                                                       select category);
        //}

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
