using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using System.Linq.Expressions;

namespace Umc.Core.IoC
{
	/// <summary>
	///		IoC 컨테이너 기반 프레임워크마다 Lifecycle 또는 Lifetime 이 틀리기 때문에
	///		<see cref="IFrameworkContainer"/> 에서 이것을 매핑하는 인터페이스를 구현한 클래스입니다.
	/// </summary>
	/// <typeparam name="TLifetime">기반 프레임워크에서 사용하는 IoC 컨테이너의 Lifetime 타입입니다.</typeparam>
	public abstract class LifetimeMapping<TLifetime> : ILifetimeMapping<TLifetime>
	{
		private Dictionary<Func<LifetimeFlag, bool>, ILifetimeMappingReturn<TLifetime>> mapper
			= new Dictionary<Func<LifetimeFlag,bool>,ILifetimeMappingReturn<TLifetime>>();

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		protected LifetimeMapping()
		{
			this.InitializeMapping();
		}



		/// <summary>	
		/// 	파생되는 클래스에서 매핑을 초기화합니다.
		/// </summary>
		protected abstract void InitializeMapping();


		/// <summary>
		///		기반 프레임워크의 Lifetime 또는 LifeStyle 을 <see cref="IFrameworkContainer"/> 의 LifetimeFlag 로 매핑합니다.
		/// </summary>
		/// <param name="func">매핑하는 조건입니다.</param>
		/// <returns>매핑에 만족하는 <see cref="ILifetimeMappingReturn{TLifetime}"/> 객체를 반환합니다.</returns>
		public ILifetimeMappingReturn<TLifetime> Map(Func<LifetimeFlag, bool> func)
		{
			ILifetimeMappingReturn<TLifetime> @return = new LifetimeMappingReturn<TLifetime>(this);
			this.mapper.Add(func, @return);

			return @return;
		}


		/// <summary>
		///		<see cref="IFrameworkContainer"/>에 매핑된 <see cref="LifetimeFlag"/>를 제거합니다.
		/// </summary>
		/// <param name="flag">IoC 컨테이너의 Lifetime 플래그 입니다.</param>
		/// <returns>제거할 때 사용된 <see cref="ILifetimeMapping{TLifetime}"/> 객체를 반환합니다.</returns>
		public ILifetimeMapping<TLifetime> RemoveMap(LifetimeFlag flag)
		{
			var key = mapper.FirstOrDefault( o => o.Key(flag) == true).Key;
			if( key == null ) return this;

			mapper.Remove(key);
			
			return this;
		}


		/// <summary>
		///		<see cref="IFrameworkContainer"/>에 매핑된 <see cref="LifetimeFlag"/>를 제거합니다.
		/// </summary>
		/// <param name="flag">IoC 컨테이너의 Lifetime 플래그 입니다.</param>
		/// <returns>제거할 때 사용된 <see cref="ILifetimeMapping{TLifetime}"/> 객체를 반환합니다.</returns>
		public ILifetimeMapping<TLifetime> RemoveAllMap()
		{
			mapper.Clear();
			return this;
		}


		/// <summary>
		///		<see cref="IFrameworkContainer"/>에 매핑되지 않은 모든 Lifetime 을 가져온 후 일괄적으로 Lifetime을 매핑할 수 있습니다.
		/// </summary>
		/// <returns>매핑에 만족하는 <see cref="ILifetimeMappingReturn{TLifetime}"/> 객체를 반환합니다.</returns>
		public ILifetimeMappingReturn<TLifetime> MapAnothers()
		{
			var notMappedLifeTimeFlag = getNotMappingLifeTimeFlag();
			
			ILifetimeMappingReturn<TLifetime> @return = new LifetimeMappingReturn<TLifetime>(this);

			foreach (var lifetime in notMappedLifeTimeFlag)
			{
                // Dictionary 키로 등록하기 위해 동적 컴파일 한다.
				var o = Expression.Parameter(typeof(LifetimeFlag), "o");
				var expression = Expression.Lambda<Func<LifetimeFlag, bool>>(Expression.Equal(
																				Expression.Convert(o, typeof(int)), 
																				Expression.Constant((int)lifetime)), o);
				
				this.mapper.Add( expression.Compile(), @return);
			}

			return @return;
		}

		private IEnumerable<LifetimeFlag> getNotMappingLifeTimeFlag()
		{
			foreach (var enumName in Enum.GetNames(typeof(LifetimeFlag)))
			{
				var lifetime = (LifetimeFlag)Enum.Parse(typeof(LifetimeFlag), enumName);
				var existFlag = mapper.Any( o => o.Key(lifetime));

				if( existFlag == false)
					yield return lifetime;
			}
		}

		/// <summary>
		///		<see cref="IFrameworkContainer"/>에 매핑된 <see cref="LifetimeFlag"/>값으로 기반 프레임워크의 Lifetime 객체를 가져옵니다.
		/// </summary>
		/// <param name="flag">IoC 컨테이너의 Lifetime 플래그 입니다.</param>
		/// <returns></returns>
		public TLifetime GetLifetimeObject(LifetimeFlag flag)
		{
			return GetLifetimeObject(flag, default(TLifetime));
		}



		/// <summary>	
		/// 	<see cref="IFrameworkContainer"/>에 매핑된 <see cref="LifetimeFlag"/>값으로 기반 프레임워크의 Lifetime
		/// 	객체를 가져옵니다. 
		/// </summary>
		/// <param name="flag">	IoC 컨테이너의 Lifetime 플래그 입니다. </param>
		/// <param name="currentLifetimeObject">	The current lifetime object. </param>
		/// <returns>	
		/// 	The lifetime object. 
		/// </returns>
		public TLifetime GetLifetimeObject(LifetimeFlag flag, TLifetime currentLifetimeObject)
		{
			var result = mapper.FirstOrDefault( o => o.Key(flag) == true).Value;
			
			if( result == null )
				throw new FrameworkDependencyException(String.Format(ExceptionRS.O_값이_1_입니다, "LifeTimeFlag", "NULL"));

			
			return result.ReturnAction(currentLifetimeObject);
		}
	}
}