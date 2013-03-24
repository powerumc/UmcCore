using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;

namespace Umc.Core.IoC
{

	/// <summary>	
	/// 	Unity Application Block 이 지원하는 객체 생명주기에 대한 매핑 클래스 입니다.
	/// </summary>
	public class LifetimeMappingForUnity : LifetimeMapping<LifetimeManager>
	{
		protected override void InitializeMapping()
		{
			this.Map(o => o == LifetimeFlag.External).Return(o => new ExternallyControlledLifetimeManager())
				.Map(o => o == LifetimeFlag.Hierarchy).Return(o => new HierarchicalLifetimeManager())
				.Map(o => o == LifetimeFlag.PerCall).Return(o => new TransientLifetimeManager())
				.Map(o => o == LifetimeFlag.PerThread).Return(o => new PerThreadLifetimeManager())
				.Map(o => o == LifetimeFlag.Singleton).Return(o => new ContainerControlledLifetimeManager())
				.MapAnothers().Return( o => new TransientLifetimeManager());
		}
	}
}
