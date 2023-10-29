using StarlightPOS.Model;
using System;
using System.Globalization;
using System.Windows.Data;

namespace StarlightPOS.Converters
{
    internal class OrderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Order temp = (Order)value;
            string message = string.Empty;

            for (int i = 0; i < temp.Items.Count; i++)
            {
                if (temp.Items[i].IsDelivered == false)
                {
                    message += $"{temp.Items[i].Product.Name}\t{temp.Items[i].Quantity}개" + Environment.NewLine;
                }

            }


            return message;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
