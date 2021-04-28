using BLL.Services;
using Common.Enums;
using System;
using System.Collections.ObjectModel;
using UI.Models;

namespace UI.ViewModels
{
    public class RecordModificationViewModel
    {
        private readonly RecordService _recordService;

        public RecordModificationViewModel(RecordModel recordModel, RecordService recordService, CategoryService categoryService)
        {
            _recordService = recordService ?? throw new ArgumentNullException(nameof(recordService));

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

                        if (RecordModel.Id <= 0)
                        {
                            _recordService.Add(RecordModel.GetRecord());
                        }
                        else
                        {
                            _recordService.Update(RecordModel.GetRecord());

                        }

                    }, 
                    null
                    );
            }
        }
    }
}
