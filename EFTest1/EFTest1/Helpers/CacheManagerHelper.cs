using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EFTest1.Helpers
{
    public static class CacheManagerHelper
    {
        public static T GetCacheObject<T>(string key, Func<T> dbContextCommand)
        {
            object objCache = HttpContext.Current.Cache[key];
            if (objCache == null)
            {
                objCache = dbContextCommand();
                HttpContext.Current.Cache[key] = objCache;
            }

            return (T)objCache;
        }
    }
}