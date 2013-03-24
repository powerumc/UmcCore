using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Umc.Core.Reflection;
using Microsoft.Win32;
using System.IO;
#if NET40
using System.Xml.Linq;
#endif

namespace Umc.Core
{
	internal static class GlobalConstants
	{
		internal static readonly string PATH_FRAMEWORK_ROOT_DIRECTORY = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
																					AssemblyEx.GetAssemblyCompany(null));

		internal static readonly string PATH_FRAMEWORK_CURRENT_DIRECTORY = Path.Combine(PATH_FRAMEWORK_ROOT_DIRECTORY, "v" + AssemblyEx.GetAssemblyVersion(null).ToString());

		internal static readonly RegistryHive REGISTRYHIVE_OF_FRAMEWORK = RegistryHive.LocalMachine;
		internal static readonly string REGISTRYSUBKEY_OF_FRAMEWORK = @"SOFTWARE\Microsoft\.NETFramework\v4.0\AssemblyFoldersEx\Umc.Core.v" + AssemblyEx.GetAssemblyVersion(null);
		//internal static readonly RegistryKey REGISTRYKEY_FRAMEWORK_SURFACE = Registry.LocalMachine.OpenSubKey();

		internal static readonly string FRAMEWORK_PUBLICKEYTOKEN = "eed8f2bc3bfc4c7a";

		internal static readonly string NAMESPACE_OF_UMC_CORE = "Umc.Core";



#if NET40
		internal static readonly XNamespace NAMESPACE_OF_UMC_CORE_IOC = "http://schema.powerumc.kr/umc/core/ioc"; 
#else
		internal static readonly string NAMESPACE_OF_UMC_CORE_IOC = "http://schema.powerumc.kr/umc/core/ioc";
#endif
		internal static readonly string ELEMENT_OF_UMC_CORE_IOC = "umc.core.ioc";
	}
}