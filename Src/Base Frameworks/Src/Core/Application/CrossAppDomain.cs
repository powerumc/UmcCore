using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;

namespace Umc.Core.Application
{
	[Serializable]
	public class CrossAppDomain : ICrossAppDomain
	{
		public AppDomain AppDomain { get; protected set; }
		
		public CrossAppDomain(string friendlyName)
		{
			this.AppDomain = AppDomain.CreateDomain(friendlyName);
			this.AppDomain.UnhandledException += new UnhandledExceptionEventHandler(AppDomain_UnhandledException);
		}

		void AppDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			throw new CrossApplicationException("A", (Exception)e.ExceptionObject);
		}

		public ICrossApplication BuildUp(string assemblyName, string typeName)
		{
			try
			{
				var application = this.AppDomain
													.CreateInstanceAndUnwrap(assemblyName, typeName) as ICrossApplication;
                if (application == null)
					throw new CrossApplicationException("application == null");

				application.AssemblyName = assemblyName;
				application.TypeName = typeName;

				application.InitializeEvents();

				return application;
			}
			catch (FileNotFoundException ex)
			{
				throw ex;
			}
		}

		public void Unload()
		{
			if (this.AppDomain == null) return;

			System.AppDomain.Unload(this.AppDomain);
		}
	}
}
