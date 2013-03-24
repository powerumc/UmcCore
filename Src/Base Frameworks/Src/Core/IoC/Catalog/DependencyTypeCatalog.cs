using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#if NET40
using System.Collections.Concurrent;
using System.Threading.Tasks;
#endif


namespace Umc.Core.IoC.Catalog
{

	/// <summary>	
	/// 	타입 수준에서 관리 대상의 목록을 관리하는 클래스 입니다.
	/// </summary>
	public class DependencyTypeCatalog : DependencyCatalog
	{
		protected IEnumerable<Type> types;

		public DependencyTypeCatalog(Type[] types)
			: this(types.AsEnumerable())
		{
			
		}

		public DependencyTypeCatalog(IEnumerable<Type> types)
		{
			this.types = types;
		}


		/// <summary>	
		/// 	<para>관리되는 대상 목록의 조건에 만족하는 타입을 반환합니다. </para>
		///		<para>.NET Framework 4.0 빌드는 병렬적으로 작업을 처리합니다.</para>
		/// </summary>
		/// <returns>	
		/// 	조건에 만족하는 타입 목록입니다. 
		/// </returns>
		public override IEnumerable<Type> GetMatchingTypes()
		{
#if NET40

			BlockingCollection<Type> blockedTypeCollection = new BlockingCollection<Type>();
			Parallel.ForEach( this.types, (type, loop) =>
			{
			    if( type.IsDependencyAttribute() )
			        blockedTypeCollection.Add(type);
			});

			return blockedTypeCollection.AsEnumerable();

#else
			foreach (var type in this.types)
			{
				if (type.IsDependencyAttribute())
				{
					yield return type;
				}
				else if ( type.IsDynamicAttribute() )
				{
					yield return type;
				}
			}
#endif
		}
	}
}