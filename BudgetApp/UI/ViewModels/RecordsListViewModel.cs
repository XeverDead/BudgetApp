using BLL.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UI.Enums;
using UI.Models;

namespace UI.ViewModels
{
    public class RecordsListViewModel : INotifyPropertyChanged
    {
        private readonly RecordService _recordService;
        private readonly CategoryService _categoryService;

        private RecordModel _currentRecordModel;

        private DateTime _startDate;
        private DateTime _endDate;

        public RecordsListViewModel(RecordService recordService, CategoryService categoryService)
        {
            _recordService = recordService ?? throw new ArgumentNullException(nameof(recordService));
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));

            RecordModels = new ObservableCollection<RecordModel>();

            SetCurrentMonthDates();

            SetRecordModels();
        }

        public ObservableCollection<RecordModel> RecordModels { get; set; }

        public RecordModel CurrentRecordModel
        {
            get => _currentRecordModel;
            set
            {
                _currentRecordModel = value;
                RaisePropertyChanged();
            }
        }

        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                if (_startDate != value)
                {
                    _startDate = value;
                    RaisePropertyChanged();
                    SetRecordModels();
                }
            }
        }

        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                if (_endDate != value)
                {
                    _endDate = value;
                    RaisePropertyChanged();
                    SetRecordModels();
                }
            }
        }

        private RelayCommand _refreshCommand;
        public RelayCommand RefreshCommand
        {
            get
            {
                return _refreshCommand ??= new RelayCommand((obj) => SetRecordModels(), null);
            }
        }

        private RelayCommand _setDates;
        public RelayCommand SetDates
        {
            get
            {
                return _setDates ??= new RelayCommand(
                    (obj) => SetDatesInterval((DateTypes)obj),
                    (obj) => obj is DateTypes
                    );
            }
        }

        public void SetRecordModels()
        {
            RecordModels.Clear();

            foreach (var record in _recordService.GetByDate(_startDate, _endDate))
            {
                //исправить на передачу categoryService из DI
                RecordModels.Add(new RecordModel(record, _categoryService));
            }
        }

        private void SetDatesInterval(DateTypes dateType)
        {
            if (dateType == DateTypes.CurrentWeek)
            {
                SetCurrentWeekDates();
            }
            else if (dateType == DateTypes.CurrentMonth)
            {
                SetCurrentMonthDates();
            }
            else if (dateType == DateTypes.CurrentYear)
            {
                SetCurrentYearDates();
            }
        }

        private void SetCurrentWeekDates()
        {
            var now = DateTime.Now;
            var currentDayNum = (int)now.DayOfWeek - 1;
            currentDayNum = currentDayNum < 0 ? 6 : currentDayNum;
            StartDate = DateTime.Now.AddDays(-currentDayNum);
            EndDate = DateTime.Now.AddDays(6 - currentDayNum);
        }

        private void SetCurrentMonthDates()
        {
            var now = DateTime.Now;
            StartDate = new DateTime(now.Year, now.Month, 1);
            EndDate = new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month));
        }

        private void SetCurrentYearDates()
        {
            var now = DateTime.Now;
            StartDate = new DateTime(now.Year, 1, 1);
            EndDate = new DateTime(now.Year, 12, DateTime.DaysInMonth(now.Year, 12));
        }

        public bool IsCurrentRecordModelNotNull => _currentRecordModel != null;

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
