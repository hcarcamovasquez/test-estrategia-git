using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xceed.Document.NET.Utils
{
    public static class EnumHelpers
    {
        public static bool TryParse<TEnum>(string value, out TEnum result)
            where TEnum : struct
        {
            try
            {
                result = (TEnum)Enum.Parse(typeof(TEnum), value);
            }
            catch
            {
                result = default;
                return false;
            }

            return true;
        }
    }


    public partial class String
    {
        public static bool IsNullOrWhiteSpace(string value)
        {
            if (value == null) return true;
            return string.IsNullOrEmpty(value.Trim());
        }
    }
}
