using BLL.Services;
using Common.Entities;
using Common.Enums;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UI.Models;

namespace UI.ViewModels
{
    public class RecordModificationViewModel : INotifyPropertyChanged
    {
        private readonly RecordService _recordService;
        private readonly CategoryService _categoryService;

        private CategoryRecordModel _currentCategoryRecordModel;

        public RecordModificationViewModel(RecordModel recordModel, RecordService recordService, CategoryService categoryService)
        {
            _recordService = recordService ?? throw new ArgumentNullException(nameof(recordService));
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));

            ExpensesCategoryRecordModels = new ObservableCollection<CategoryRecordModel>();
            IncomeCategoryRecordModels = new ObservableCollection<CategoryRecordModel>();

            if (recordModel != null)
            {
                RecordModel = recordModel;

                foreach (var categoryRecordModel in recordModel.CategoryRecordModels)
                {
                    if (categoryRecordModel.CategoryModel.Group == CategoryGroups.Expenses)
                    {
                        ExpensesCategoryRecordModels.Add(categoryRecordModel);
                    }
                    else if (categoryRecordModel.CategoryModel.Group == CategoryGroups.Income)
                    {
                        IncomeCategoryRecordModels.Add(categoryRecordModel);
                    }
                }
            }
            else
            {
                RecordModel = new RecordModel(null, categoryService);
            }
        }

        public RecordModel RecordModel { get; set; }

        public ObservableCollection<CategoryRecordModel> ExpensesCategoryRecordModels { get; set; }

        public ObservableCollection<CategoryRecordModel> IncomeCategoryRecordModels { get; set; }

        public CategoryRecordModel CurrentCategoryRecordModel
        {
            get => _currentCategoryRecordModel;
            set
            {
                _currentCategoryRecordModel = value;
                IsCurrentCategoryRecordModelNotNull = _currentCategoryRecordModel != null;
                RaisePropertyChanged();
            }
        }

        public bool IsCurrentCategoryRecordModelNotNull
        {
            get => _currentCategoryRecordModel != null;
            set
            {
                RaisePropertyChanged();
            }
        }

        private RelayCommand _saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return _saveCommand ??= new RelayCommand(
                    (obj) =>
                    {
                        RecordModel.CategoryRecordModels.Clear();

                        foreach (var categoryRecordModel in ExpensesCategoryRecordModels)
                        {
                            RecordModel.CategoryRecordModels.Add(categoryRecordModel);
                        }

                        foreach (var categoryRecordModel in IncomeCategoryRecordModels)
                        {
                            RecordModel.CategoryRecordModels.Add(categoryRecordModel);
                        }

                        if (RecordModel.Id == 0)
                        {
                            _recordService.Add(RecordModel.GetRecord());
                        }
                        else
                        {
                            _recordService.Update(RecordModel.GetRecord());
                        }

                    }, 
                    (obj) => IncomeCategoryRecordModels.Count + ExpensesCategoryRecordModels.Count > 0
                    );
            }
        }

        private RelayCommand _deleteCommand;
        public RelayCommand DeleteCommand
        {
            get
            {
                return _deleteCommand ??= new RelayCommand(
                    (obj) => _recordService.Delete(RecordModel.GetRecord()),
                    (obj) => RecordModel.Id != 0
                    );
            }
        }

        private RelayCommand _deleteCategoryRecordCommand;

        public RelayCommand DeleteCategoryRecordCommand
        {
            get
            {
                return _deleteCategoryRecordCommand ??= new RelayCommand(
                    (obj) =>
                    {
                        if (ExpensesCategoryRecordModels.Contains(CurrentCategoryRecordModel))
                        {
                            ExpensesCategoryRecordModels.Remove(CurrentCategoryRecordModel);
                        }
                        else if (IncomeCategoryRecordModels.Contains(CurrentCategoryRecordModel))
                        {
                            IncomeCategoryRecordModels.Remove(CurrentCategoryRecordModel);
                        }

                        CurrentCategoryRecordModel = null;
                    },
                    (obj) => IsCurrentCategoryRecordModelNotNull
                    );
            }
        }

        public void AddCategoryRecord(CategoryModel categoryModel)
        {
            if (categoryModel == null)
            {
                return;
            }

            var categoryRecord = new CategoryRecord
            {
                CategoryId = categoryModel.Id,
                RecordId = RecordModel.Id
            };

            var categoryRecordModel = new CategoryRecordModel(categoryRecord, _categoryService);

            if (categoryModel.Group == CategoryGroups.Expenses)
            {
                ExpensesCategoryRecordModels.Add(categoryRecordModel);
            }
            else if (categoryModel.Group == CategoryGroups.Income)
            {
                IncomeCategoryRecordModels.Add(categoryRecordModel);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
