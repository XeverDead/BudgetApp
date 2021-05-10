using Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Windows.Data;

namespace UI.Converters
{
    public class CategoryGroupToDisplayNameConverter : IValueConverter
    {
        private readonly Dictionary<CategoryGroups, string> _categoryGroupsDisplayNames;

        public CategoryGroupToDisplayNameConverter()
        {
            _categoryGroupsDisplayNames = new Dictionary<CategoryGroups, string>();

            SetDisplayNames();
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var displayName = string.Empty;

            if (value is CategoryGroups categoryGroup)
            {
                displayName = Enum.GetName(typeof(CategoryGroups), categoryGroup);

                var attributesCollection = typeof(CategoryGroups)
                    .GetField(displayName)
                    .GetCustomAttributes(typeof(DisplayAttribute), false);

                if (attributesCollection.Length > 0)
                {
                    displayName = ((DisplayAttribute)attributesCollection[0]).Name;
                }
            }

            return displayName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var categoryGroup = CategoryGroups.Expenses;

            if (value is string displayName)
            {
                foreach (var categoryGroupName in _categoryGroupsDisplayNames)
                {
                    if (categoryGroupName.Value == displayName)
                    {
                        categoryGroup = categoryGroupName.Key;
                        break;
                    }
                }
            }

            return categoryGroup;
        }

        private void SetDisplayNames()
        {
            foreach (CategoryGroups categoryGroup in Enum.GetValues(typeof(CategoryGroups)))
            {
                var displayName = Enum.GetName(typeof(CategoryGroups), categoryGroup);

                var attributesCollection = typeof(CategoryGroups)
                    .GetField(displayName)
                    .GetCustomAttributes(typeof(DisplayAttribute), false);

                if (attributesCollection.Length > 0)
                {
                    displayName = ((DisplayAttribute)attributesCollection[0]).Name;
                }

                _categoryGroupsDisplayNames.Add(categoryGroup, displayName);
            }
        }
    }
}
