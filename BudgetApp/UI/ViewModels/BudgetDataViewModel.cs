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
        private double _balance;

        private RecordService _recordService;
        private CategoryService _categoryService;

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

                    if (_endDate == _endDate.Date)
                    {
                        _endDate = _endDate.AddDays(1).AddTicks(-1);
                    }

                    RaisePropertyChanged();
                }
            }
        }

        public ObservableCollection<CategoryRecordModel> IncomeCategoryRecordModels { get; set; }

        public ObservableCollection<CategoryRecordModel> ExpensesCategoryRecordModels { get; set; }

        public double IncomeValue
        {
            get => _incomeValue;
            set
            {
                if (_incomeValue != value)
                {
                    _incomeValue = value;
                    RaisePropertyChanged();
                }
            }
        }

        public double ExpensesValue
        {
            get => _expensesValue;
            set
            {
                if (_expensesValue != value)
                {
                    _expensesValue = value;
                    RaisePropertyChanged();
                }
            }
        }

        public double Balance
        {
            get => _balance;
            set
            {
                if (_balance != value)
                {
                    _balance = value;
                    RaisePropertyChanged();
                }
            }
        }

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
                    null
                    );
            }
        }

        public void UpdateServices(RecordService recordService, CategoryService categoryService)
        {
            _recordService = recordService ?? throw new ArgumentNullException(nameof(recordService));
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));

            SetCategoryRecordModels();
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
                        AddCategoryRecordModelToCollection(ExpensesCategoryRecordModels, categoryRecordModel);
                    }
                    else if (categoryRecordModel.CategoryModel.Group == CategoryGroups.Income)
                    {
                        AddCategoryRecordModelToCollection(IncomeCategoryRecordModels, categoryRecordModel);
                    }
                }
            }

            IncomeValue = Math.Round(IncomeCategoryRecordModels.Sum(categoryRecordModel => categoryRecordModel.Value), 2);
            ExpensesValue = Math.Round(ExpensesCategoryRecordModels.Sum(categoryRecordModel => categoryRecordModel.Value), 2);
            Balance = IncomeValue - ExpensesValue;
        }

        private void AddCategoryRecordModelToCollection(ObservableCollection<CategoryRecordModel> collection, CategoryRecordModel model)
        {
            var sameCategoryCollection = collection.Where(categoryRecordModel => categoryRecordModel.CategoryModel.Id == model.CategoryModel.Id)
                            .ToList();

            if (sameCategoryCollection.Count() > 0)
            {
                sameCategoryCollection[0].Value += model.Value;
            }
            else
            {
                collection.Add(model);
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
