using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Umc.Core.IoC.Configuration;

namespace Umc.Core.IoC
{
	public static class FrameworkContainerFactory
	{
		public static IFrameworkContainer Create()
		{
			throw new NotImplementedException();
		}

		public static IFrameworkContainer Create(IFrameworkComposable composer)
		{
			throw new NotImplementedException();
		}

		public static IFrameworkContainer Create(ExeConfigurationFileMap mapFile)
		{
			throw new NotImplementedException();
		}

		public static IFrameworkContainer Create(string configFile)
		{
			throw new NotImplementedException();
		}
	}
}
