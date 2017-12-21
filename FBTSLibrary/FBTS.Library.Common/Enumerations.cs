using System;

namespace FBTS.Library.Common
{
    public static class Enumerations
    {
        public static String ConvertToString(this Enum eff)
        {
            return Enum.GetName(eff.GetType(), eff);
        }

        public static TEnumType ConvertToEnum<TEnumType>(this String enumValue)
        {
            return (TEnumType) Enum.Parse(typeof (TEnumType), enumValue);
        }
    }
}