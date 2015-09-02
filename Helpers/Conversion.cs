using System;
using System.Globalization;
using System.Threading;

namespace NPager.Helpers
{
    public static class Conversions
    {
        /// <summary>
        /// Safely Converts a value to integer
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ToInt(this object value)
        {
            var retVal = 0;

            if (value != null && value != DBNull.Value)
            {
                if (value is bool)
                {
                    if (Convert.ToBoolean(value, CultureInfo.InvariantCulture))
                    {
                        return 1;
                    }
                }
                string numberToParse = value.ToString();

                if (int.TryParse(numberToParse, out retVal))
                {
                    return retVal;
                }
            }

            return retVal;
        }

        public static decimal ToDecimal(this object value)
        {
            decimal retVal = 0;

            if (value == null || value == DBNull.Value) return retVal;
            var numberToParse = value.ToString();
            return decimal.TryParse(numberToParse, out retVal) ? retVal : retVal;
        }

        
    }
}
