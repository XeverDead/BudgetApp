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
        private RecordService _recordService;
        private CategoryService _categoryService;

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
                IsCurrentRecordModelNotNull = _currentRecordModel != null;
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

                    if (_endDate == _endDate.Date)
                    {
                        _endDate = _endDate.AddDays(1).AddTicks(-1);
                    }

                    RaisePropertyChanged();
                }
            }
        }

        public bool IsCurrentRecordModelNotNull
        {
            get => _currentRecordModel != null;
            set
            {
                RaisePropertyChanged();
            }
        }

        public void UpdateServices(RecordService recordService, CategoryService categoryService)
        {
            _recordService = recordService ?? throw new ArgumentNullException(nameof(recordService));
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));

            _currentRecordModel = null;

            SetRecordModels();
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
                    null
                    );
            }
        }

        private void SetRecordModels()
        {
            RecordModels.Clear();

            foreach (var record in _recordService.GetByDate(_startDate, _endDate))
            {
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
            var today = DateTime.Today;
            var currentDayNum = (int)today.DayOfWeek - 1;
            currentDayNum = currentDayNum < 0 ? 6 : currentDayNum;
            StartDate = today.AddDays(-currentDayNum);
            EndDate = today.AddDays(6 - currentDayNum).AddDays(1).AddTicks(-1);
        }

        private void SetCurrentMonthDates()
        {
            var today = DateTime.Today;
            StartDate = new DateTime(today.Year, today.Month, 1);
            EndDate = new DateTime(today.Year, today.Month, DateTime.DaysInMonth(today.Year, today.Month)).AddDays(1).AddTicks(-1);
        }

        private void SetCurrentYearDates()
        {
            var today = DateTime.Today;
            StartDate = new DateTime(today.Year, 1, 1);
            EndDate = new DateTime(today.Year, 12, DateTime.DaysInMonth(today.Year, 12)).AddDays(1).AddTicks(-1);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
