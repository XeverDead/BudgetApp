using Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Windows.Data;

namespace UI.Converters
{
    class CategoryGroupsToDisplayNamesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var displayNames = new List<string>();

            if (value is IEnumerable<CategoryGroups> categoryGroups)
            {
                foreach (var categoryGroup in categoryGroups)
                {
                    var displayName = Enum.GetName(typeof(CategoryGroups), categoryGroup);

                    var attributesCollection = typeof(CategoryGroups)
                        .GetField(displayName)
                        .GetCustomAttributes(typeof(DisplayAttribute), false);

                    if (attributesCollection.Length > 0)
                    {
                        displayName = ((DisplayAttribute)attributesCollection[0]).Name;
                    }

                    displayNames.Add(displayName);
                }
            }

            return displayNames;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
