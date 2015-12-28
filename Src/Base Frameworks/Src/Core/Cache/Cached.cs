using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#if NET4
using System.Runtime.Caching;


namespace Umc.Core
{
	public class Cached
	{
		private readonly MemoryCache memoryCache = MemoryCache.Default;
		private static readonly Cached sharedInstance = new Cached();

		private Cached() { }

		public void Add<T>(string key, T item, double seconds = 60)
		{
			memoryCache.Set(key, item, DateTimeOffset.Now.AddSeconds(seconds));
		}

		public Cached AddOrGetExisting<T>(string key, T item, int seconds = 60)
		{
			memoryCache.AddOrGetExisting(key, item, DateTimeOffset.Now.AddSeconds(seconds));
			return sharedInstance;
		}

		public T Get<T>(string key)
		{
			return (T) memoryCache.Get(key);
		}
	}
}
#endif