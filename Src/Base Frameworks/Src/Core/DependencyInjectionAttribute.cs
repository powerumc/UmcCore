using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Umc.Core.IoC;

namespace Umc.Core
{
	/// <summary>
	///		<see cref="IFrameworkContainer"/> 의 객체를 주입하기 위한 특성입니다.
	/// </summary>
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes"), AttributeUsage(AttributeTargets.Property | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
	public class DependencyInjectionAttribute : Attribute, IDependencyAttribute
	{

		/// <summary>	
		/// 	종속성 주입 시 사용될 고유한 키를 가져오거나 설정합니다.
		/// </summary>
		public object Key { get; set; }


		/// <summary>	
		/// 	종속성 주입 시 사용할 기본 값을 가져오거나 설정합니다.
		/// </summary>
		public object DefaultValue { get; set; }

		public DependencyInjectionAttribute()
			: this(null)
		{
		}

		public DependencyInjectionAttribute(object key)
		{
			this.Key = key;
		}
	}
}
