using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.Cache
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class CachedAttribute : Attribute
    {
        private const int SECONDS_OF_EXPIRING_DEFAULT = 60;


	    public int ExpiringSeconds { get; set; }


	    public CachedAttribute()
	    {
		    this.ExpiringSeconds = SECONDS_OF_EXPIRING_DEFAULT;
	    }

        public CachedAttribute(int seconds)
        {
            this.ExpiringSeconds = seconds;
        }


        public static IEnumerable<CachedAttribute> GetCachedAttributes(Type type)
        {
            return type.GetCustomAttributes(typeof(CachedAttribute), true).Cast<CachedAttribute>();
        }

        public static CachedAttribute GetCachedAttribute(Type type)
        {
            return type.GetCustomAttributes(typeof (CachedAttribute), true).FirstOrDefault() as CachedAttribute;
        }
    }
}
