using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Registration.Lifestyle;
using Castle.MicroKernel;

namespace Umc.Core.IoC.Castle
{
	/// <summary>	
	/// 	Castle Windsor가 지원하는 객체 생명주기에 대한 매핑 클래스 입니다.
	/// </summary>
	public class LifetimeMappingForCastle : LifetimeMapping<Type>
	{
		protected override void InitializeMapping()
		{
			this.Map(o => o == LifetimeFlag.Default).Return(o => typeof(global::Castle.MicroKernel.Lifestyle.SingletonLifestyleManager))
				.Map(o => o == LifetimeFlag.PerThread).Return(o => typeof(global::Castle.MicroKernel.Lifestyle.PerThreadLifestyleManager))
				.Map(o => o == LifetimeFlag.Singleton).Return(o => typeof(global::Castle.MicroKernel.Lifestyle.SingletonLifestyleManager))
				.Map(o => o == LifetimeFlag.PerCall).Return(o => typeof(global::Castle.MicroKernel.Lifestyle.TransientLifestyleManager));
		}
	}
}
