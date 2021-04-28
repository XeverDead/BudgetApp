using BLL.Services;
using Common.Enums;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using UI.Models;
using UI.Enums;

namespace UI.ViewModels
{
    public class BudgetDataViewModel : INotifyPropertyChanged
    {
        private DateTime _startDate;
        private DateTime _endDate;

        private double _expensesValue;
        private double _incomeValue;

        private readonly RecordService _recordService;
        private readonly CategoryService _categoryService;

        public BudgetDataViewModel(RecordService recordService, CategoryService categoryService)
        {
            _recordService = recordService ?? throw new ArgumentNullException(nameof(recordService));
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));

            IncomeCategoryRecordModels = new ObservableCollection<CategoryRecordModel>();
            ExpensesCategoryRecordModels = new ObservableCollection<CategoryRecordModel>();

            SetCurrentMonthDates();

            SetCategoryRecordModels();
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
                    RaisePropertyChanged();
                }
            }
        }

        public ObservableCollection<CategoryRecordModel> IncomeCategoryRecordModels { get; set; }

        public ObservableCollection<CategoryRecordModel> ExpensesCategoryRecordModels { get; set; }

        public double IncomeValue => _incomeValue;

        public double ExpensesValue => _expensesValue;

        public double Balance => _incomeValue - _expensesValue;

        private RelayCommand _refreshCommand;
        public RelayCommand RefreshCommand
        {
            get
            {
                return _refreshCommand ??= new RelayCommand((obj) => SetCategoryRecordModels(), null);
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

        private void SetCategoryRecordModels()
        {
            IncomeCategoryRecordModels.Clear();
            ExpensesCategoryRecordModels.Clear();

            foreach (var record in _recordService.GetByDate(_startDate, _endDate))
            {
                var recordModel = new RecordModel(record, _categoryService);

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

            _incomeValue = IncomeCategoryRecordModels.Sum(categoryRecordModel => categoryRecordModel.Value);
            _expensesValue = ExpensesCategoryRecordModels.Sum(categoryRecordModel => categoryRecordModel.Value);
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

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
