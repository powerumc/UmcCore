using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Umc.Core.Reflection
{
	public static class AssemblyEx
	{
		public static string GetAssemblyCompany(Assembly assembly)
		{
			if( assembly == null )
				assembly = Assembly.GetExecutingAssembly();

			var attributes = assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);

			if (attributes != null && attributes.Length == 0)
				throw new FrameworkException(ExceptionRS.O_어셈블리에서_1_특성을_찾을_수_없습니다, assembly.FullName, typeof(AssemblyCompanyAttribute).Name);

			return attributes.Cast<AssemblyCompanyAttribute>().FirstOrDefault().Company;
		}

		public static Version GetAssemblyVersion(Assembly assembly)
		{
			if( assembly == null )
				assembly = Assembly.GetExecutingAssembly();

			return assembly.GetName().Version;
		}

		public static string GetPublicKeyToken(Assembly assembly)
		{
			if (assembly == null)
				assembly = Assembly.GetExecutingAssembly();

			var tokenBytes = assembly.GetName().GetPublicKeyToken();

			if (tokenBytes == null || tokenBytes.Length == 0)
				throw new FrameworkException(ExceptionRS.O_어셈블리는_강력한_이름의_서명이_되어있지않아_공개키토큰_값이_없습니다, assembly.FullName);

			StringBuilder sb = new StringBuilder(50);

			foreach (byte b in tokenBytes)
			{
				sb.Append(b.ToString("x"));
			}

			return sb.ToString();
		}
	}
}
