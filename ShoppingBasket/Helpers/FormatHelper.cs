using System.Globalization;

namespace ShoppingBasket.Helpers
{
    public static class FormatHelper
    {
        private static readonly CultureInfo culture = new CultureInfo("en-US");
        public static string FormatAmount(this decimal value)
        {
            return string.Format(culture, "{0:C2}", value);
        }
    }
}
