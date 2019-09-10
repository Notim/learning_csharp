using System.Collections;
using System.Collections.Generic;

using APP.AppUtils.Extensions;

using StackExchange.Redis;

namespace APP {

    public static class RedisComands {

        public static class List {
            /// <summary>
            /// BLPOP key1 [key2 ] timeout - Removes and gets the first element in a list, or blocks until one is available
            /// </summary>
            public static string BLPOP => "BLPOP";

            /// <summary>
            /// BRPOP key1 [key2 ] timeout - Removes and gets the last element in a list, or blocks until one is available
            /// </summary>
            public static string BRPOP => "BRPOP";

            /// <summary>
            /// BRPOPLPUSH source destination timeout - Pops a value from a list, pushes it to another list and returns it; or blocks until one is available
            /// </summary>
            public static string BRPOPLPUSH => "BRPOPLPUSH";

            /// <summary>
            /// LINDEX key index - Gets an element from a list by its index
            /// </summary>
            public static string LINDEX => "LINDEX";

            /// <summary>
            /// LINSERT key BEFORE|AFTER pivot value - Inserts an element before or after another element in a list
            /// </summary>
            public static string LINSERT => "LINDEX";

            /// <summary>
            /// LLEN key - Gets the length of a list
            /// </summary>
            public static string LLEN => "LLEN";

            /// <summary>
            /// LPOP key - Removes and gets the first element in a list
            /// </summary>
            public static string LPOP => "LPOP";

            /// <summary>
            /// LPUSH key value1 [value2] - Prepends one or multiple values to a list
            /// </summary>
            public static string LPUSH => "LPUSH";

            /// <summary>
            /// LPUSHX key value - Prepends a value to a list, only if the list exists
            /// </summary>
            public static string LPUSHX => "LPUSHX";

            /// <summary>
            /// LRANGE key start stop - Gets a range of elements from a list
            /// </summary>
            public static string LRANGE => "LRANGE";

            /// <summary>
            /// LREM key count value - Removes elements from a list
            /// </summary>
            public static string LREM => "LREM";

            /// <summary>
            /// LSET key index value - Sets the value of an element in a list by its index
            /// </summary>
            public static string LSET => "LSET";

            /// <summary>
            /// LTRIM key start stop - Trims a list to the specified range
            /// </summary>
            public static string LTRIM => "LTRIM";

            /// <summary>
            /// RPOP key - Removes and gets the last element in a list
            /// </summary>
            public static string RPOP => "RPOP";

            /// <summary>
            /// RPOPLPUSH source destination - Removes the last element in a list, appends it to another list and returns it
            /// </summary>
            public static string RPOPLPUSH => "RPOPLPUSH";

            /// <summary>
            /// RPUSH key value1 [value2] - Appends one or multiple values to a list
            /// </summary>
            public static string RPUSH => "RPUSH";

            /// <summary>
            /// RPUSHX key value - Appends a value to a list, only if the list exists
            /// </summary>
            public static string RPUSHX => "RPUSHX";
        }

        public static class KEYS {

            /// <summary>
            /// DEL key - Delete a key
            /// </summary>
            public static string DEL => "DEL";
        }

    }

}