using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;

namespace System
{
	public static class StringExtension
	{

		public static byte[] ToBytes(this string str, Encoding encoding)
		{
			return encoding.GetBytes(str);
		}

		public static byte[] ToBytes(this string str)
		{
			return ToBytes(str, Encoding.Default);
		}

		public static ManifestResourceInfo GetManifestResourceInfoGet(this string resourceName, Assembly assm)
		{
			if (resourceName == null) throw new ArgumentNullException("resourceName");

			return assm.GetManifestResourceInfo(resourceName);
		}

		public static Stream GetManifestResourceStream(this string resourceName, Assembly assm)
		{
			if (resourceName == null) throw new ArgumentNullException("resourceName");

			return assm.GetManifestResourceStream(resourceName);
		}

		public static IEnumerable<Stream> GetContainsManifestResourceStreamOrDefault(this string containsString, Assembly assm)
		{
			if (containsString == null) throw new ArgumentNullException("containsString");

			foreach (var name in assm.GetManifestResourceNames())
			{
				if (name.Contains(containsString))
					yield return assm.GetManifestResourceStream(name);
			}
		}
	}
}
