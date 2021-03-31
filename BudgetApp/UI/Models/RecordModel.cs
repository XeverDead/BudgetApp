using BLL.Services;
using Common.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace UI.Models
{
    public class RecordModel : INotifyPropertyChanged
    {
        private readonly Record _record;

        public RecordModel(Record record, CategoryService categoryService)
        {
            _record = (Record)record?.Clone();

            CategoryRecordModels = new ObservableCollection<CategoryRecordModel>();

            foreach (var categoryRecord in _record.CategoryRecords)
            {
                CategoryRecordModels.Add(new CategoryRecordModel(categoryRecord, categoryService));
            }
        }

        public long Id => _record.Id;

        public ObservableCollection<CategoryRecordModel> CategoryRecordModels { get; set; }

        public string Note
        {
            get => _record.Note;
            set
            {
                if (_record.Note != value)
                {
                    _record.Note = value;
                    RaisePropertyChanged();
                }
            }
        }

        public DateTime Date
        {
            get => _record.Date;
            set
            {
                if (_record.Date != value)
                {
                    _record.Date = value;
                    RaisePropertyChanged();
                }
            }
        }

        public Record GetRecord()
        {
            _record.CategoryRecords = new List<CategoryRecord>();

            foreach (var categoryRecordModel in CategoryRecordModels)
            {
                _record.CategoryRecords.Add(categoryRecordModel.GetCategoryRecord());
            }

            return (Record)_record.Clone();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
