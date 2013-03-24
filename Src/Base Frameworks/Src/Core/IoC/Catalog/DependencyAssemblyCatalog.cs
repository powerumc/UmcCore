using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

#if NET40
using System.Threading.Tasks;
using System.Collections.Concurrent;
#endif

namespace Umc.Core.IoC.Catalog
{

	/// <summary>	
	/// 	어셈블리 수준에서 관리 대상의 목록을 관리하는 클래스 입니다.
	/// </summary>
	public class DependencyAssemblyCatalog : DependencyCatalog
	{
		private IEnumerable<Assembly> assemblies;

		public DependencyAssemblyCatalog(params Assembly[] assemblies)
			: this(assemblies.AsEnumerable())
		{
		}

		public DependencyAssemblyCatalog(IEnumerable<Assembly> assemblies)
		{
			this.assemblies = assemblies;
		}


		/// <summary>	
		/// 	관리되는 대상 목록의 조건에 만족하는 타입을 반환합니다. 
		/// </summary>
		/// <returns>	
		/// 	조건에 만족하는 타입 목록입니다. 
		/// </returns>
		public override IEnumerable<Type> GetMatchingTypes()
		{
			foreach(var assembly in this.assemblies)
			{
				DependencyTypeCatalog catalog = new DependencyTypeCatalog(assembly.GetTypes());
				var types = catalog.GetMatchingTypes();

				foreach (var type in types)
				{
					yield return type;
				}
			}
		}
	}
}