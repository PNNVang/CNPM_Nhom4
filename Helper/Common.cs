using System.Globalization;

namespace Dot_Net_ECommerceWeb.Helper
{
    public class Common
    {

        // Format float with custom decimal places
        public static string FormatNumber(float number, int decimalPlaces = 0)
        {
            return string.Format(new CultureInfo("vi-VN"), "{0:N" + decimalPlaces + "}", number);
        }

        // Format currency with custom decimal places
        public static string FormatCurrency(float number, int decimalPlaces = 0)
        {
            return string.Format(new CultureInfo("vi-VN"), "{0:C" + decimalPlaces + "}", number);
        }

        // Optional: For decimal (if needed)
        public static string FormatNumber(decimal number, int decimalPlaces = 0)
        {
            return string.Format(new CultureInfo("vi-VN"), "{0:N" + decimalPlaces + "}", number);
        }

        public static string FormatCurrency(decimal number, int decimalPlaces = 0)
        {
            return string.Format(new CultureInfo("vi-VN"), "{0:C" + decimalPlaces + "}", number);
        }
    }
}
