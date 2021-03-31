using Common.Entities;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Common.Enums;

namespace UI.Models
{
    public class CategoryModel : INotifyPropertyChanged
    {
        private readonly Category _category;

        public CategoryModel(Category category)
        {
            _category = (Category)category?.Clone();
        }

        public long Id => _category.Id;

        public string Name
        {
            get => _category.Name;
            set
            {
                if (_category.Name != value)
                {
                    _category.Name = value;
                    RaisePropertyChanged();
                }
            }
        }

        public CategoryGroups Group
        {
            get => _category.Group;
            set
            {
                if (_category.Group != value)
                {
                    _category.Group = value;
                    RaisePropertyChanged();
                }
            }
        }

        public Category GetCategory()
        {
            return (Category)_category.Clone();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
