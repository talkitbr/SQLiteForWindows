using Newtonsoft.Json;
using SQLiteModernApp.DataModel;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Data;
using System.Linq;

namespace SQLiteModernApp.Converters
{
    public class DepartmentListToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (!(value is List<Department>))
            {
                throw new InvalidCastException();
            }

            List<Department> departments = value as List<Department>;
            if (departments != null && departments.Count > 0)
            {
                return string.Join(", ", departments.Select(d => d.DepartmentName).ToArray<string>());
            }

            return "None";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
