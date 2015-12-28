using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Umc.Core.IoC;

namespace Umc.Core
{
	/// <summary>
	/// <para><see cref="DependencyContractAttribute"/> 특성은 인터페이스 또는 클래스를 통해 종속성을 제거하기 위한 특성을 나타냅니다.</para>
	/// <para>구현 클래스에 선언하십시오.</para>
	/// <para>여러 계약 타입을 선언할 수 있지만, 중복된 계약/키값은 사용할 수 없습니다.</para>
	/// </summary>
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes"), AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
	public class DependencyContractAttribute : Attribute, IDependencyAttribute
	{
		//public bool IsDefault { get; set; }
		public Type TypeOfContract { get; set; }
		public string Key { get; set; }
		public LifetimeFlag LifetimeFlag { get; set; }

		public DependencyContractAttribute()
		{
			this.LifetimeFlag = IoC.LifetimeFlag.Default;
		}

		public DependencyContractAttribute(Type contractType) 
			: this(contractType, null, LifetimeFlag.Default)
		{
		}

		public DependencyContractAttribute(Type contractType, string key) 
			: this(contractType, key, LifetimeFlag.Default)
		{
		}

		public DependencyContractAttribute(Type contractType, LifetimeFlag flag) 
			: this(contractType, null, flag)
		{
		}

		public DependencyContractAttribute(Type type, string key, LifetimeFlag flag)
		{
			this.TypeOfContract = type;
			this.Key            = key;
			this.LifetimeFlag   = flag;
		}
	}
}
