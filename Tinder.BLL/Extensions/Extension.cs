using System;

namespace Tinder.BLL.Extensions
{
    public static class Extension
    {
        public static int Age(this DateTime dateTime)
        {
            var today = DateTime.Today;
            var age = today.Year - dateTime.Year;
            if (dateTime.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}
