using BLL.Services;
using Common.Entities;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace UI.Models
{
    public class CategoryRecordModel : INotifyPropertyChanged
    {
        private readonly CategoryRecord _categoryRecord;

        private CategoryModel _categoryModel;

        public CategoryRecordModel(CategoryRecord categoryRecord, CategoryService categoryService)
        {
            _categoryRecord = (CategoryRecord)categoryRecord?.Clone();

            if (categoryService == null)
            {
                throw new ArgumentNullException(nameof(categoryService));
            }

            _categoryModel = new CategoryModel(categoryService.GetById(_categoryRecord.CategoryId));
        }

        public long Id => _categoryRecord.Id;

        public CategoryModel CategoryModel 
        {
            get => _categoryModel;
            set
            {
                _categoryModel = value;
                _categoryRecord.CategoryId = value.Id;
            }
        }

        public long RecordId
        {
            get => _categoryRecord.RecordId;
            set => _categoryRecord.RecordId = value;
        }

        public double Value
        {
            get => _categoryRecord.Value;
            set
            {
                if (_categoryRecord.Value != value)
                {
                    _categoryRecord.Value = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string Note
        {
            get => _categoryRecord.Note;
            set
            {
                if (_categoryRecord.Note != value)
                {
                    _categoryRecord.Note = value;
                    RaisePropertyChanged();
                }
            }
        }

        public CategoryRecord GetCategoryRecord()
        {
            return (CategoryRecord)_categoryRecord.Clone();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
