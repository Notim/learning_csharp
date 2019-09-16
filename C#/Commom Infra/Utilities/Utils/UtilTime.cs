using System;

namespace UTIL.Utils {

    public static class UtilTimeSpan {
        public static TimeSpan? toTimeSpan(this string value) {
            return TimeSpan.TryParse(value, out var time) ? time : (TimeSpan?) null;
        }

        public static bool isValid(string time) {

            return TimeSpan.TryParse(time.Trim(), out _);
        }
    }

}