using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.Collections;
using Umc.Core.Logger;

namespace Umc.Core.Mapping
{
	/// <summary>
	///		<para><see cref="IMappingProvider"/> 인터페이스를 구현하는 베이스클레스 입니다.</para>
	///		<para>개별 데이터소스로부터 매핑을 지원할 수 있는 개체를 구현하기 위해서 <see cref="MappingProvider{TInput, TReturn}"/> 클래스를 상속하십시오.</para>
	/// </summary>
	/// <typeparam name="TInput">입력 값의 타입입니다.</typeparam>
	/// <typeparam name="TReturn">반환되는 값의 타입입니다.</typeparam>
	public abstract class MappingProvider<TInput, TReturn> : InvocationMapping<TInput, TReturn>,
																IMappingProvider,
																IInitializable,
																IMatchable<TInput>
	{

		protected static IFrameworkLogger logger = FrameworkLogger.GetLogger(typeof(MappingProvider<,>));

		/// <summary>
		///		매핑 프로바이더의 매핑 데이터소스를 변경합니다.
		/// </summary>
		/// <param name="object">데이터소스로 변경할 객체입니다.</param>
		public abstract void SetObject(object @object);

		/// <summary>
		///		매핑 프로바이더의 매핑 데이터소스를 반환합니다.
		/// </summary>
		/// <returns>매핑 프로바이더와 연결된 객체를 반환합니다.</returns>
		public abstract object GetObject();

		/// <summary>
		///		매핑에 사용할 수 있는 키를 반환합니다.
		/// </summary>
		public abstract IEnumerable<object> MappingKeys { get; }


		/// <summary>
		/// 새로운 객체를 생성할 수 있는지 여부를 나타냅니다.
		/// </summary>
		public virtual bool CanCreateNewInstance { get; protected set; }

		/// <summary>	
		/// 	새로운 객체를 생성합니다.
		/// </summary>
		/// <returns>생성된 새로운 개체를 반환합니다.</returns>
		public abstract object CreateNewInstance();

		/// <summary>
		///		매핑이 수행되기 전에 초기화할 수 있는 작업입니다.
		/// </summary>
		/// <param name="sourceProvider"><see cref="IMappingProvider"/> 를 구현하는 원본 매핑 프로바이더 입니다.</param>
		/// <param name="targetProvider"><see cref="IMappingProvider"/> 를 구현하는 대상 매핑 프로바이더 입니다.</param>
		public virtual void StartOfAssign(IMappingProvider sourceProvider, IMappingProvider targetProvider) { }
		/// <summary>
		///		매핑이 수행된 후의 작업입니다.
		/// </summary>
		/// <param name="sourceProvider"><see cref="IMappingProvider"/> 를 구현하는 원본 매핑 프로바이더 입니다.</param>
		/// <param name="targetProvider"><see cref="IMappingProvider"/> 를 구현하는 대상 매핑 프로바이더 입니다.</param>
		public virtual void EndOfAssign(IMappingProvider sourceProvider, IMappingProvider targetProvider) { }

		/// <summary>	
		/// 	개체가 특정 객체를 반환할 수 있는지 여부 입니다.
		/// </summary>
		/// <param name="input">객체를 반환할 때 필요한 매개 변수입니다. </param>
		/// <returns>특정 객체를 반환할 수 있으면 True, 그렇지 않으면 False 입니다.</returns>
		public abstract object Getter(object input);

		/// <summary>	
		/// 	개체에 값을 설정합니다.
		/// </summary>
		/// <param name="input">객체를 설정할 때 필요한 매개 변수 입니다.</param>
		/// <param name="arg">객체에 설정하는 값입니다.</param>
		public abstract void Setter(object input, object arg);

		/// <summary>	
		/// 	초기화 작업을 수행합니다.
		/// </summary>
		public abstract void Initialize();
		
		/// <summary>	
		/// 	객체가 조건에 만족하는지 여부를 반환합니다.
		/// </summary>
		/// <param name="input">조건으로 사용하는 매개 변수 입니다.</param>
		/// <returns>조건에 만족하면 True, 그렇지 않으면 False 를 반환합니다.</returns>
		public abstract bool IsMatches(TInput input);


		/// <summary>	
		/// 	개체가 특정 객체를 반환할 수 있는지 여부 입니다.
		/// </summary>
		/// <param name="input">객체를 반환할 때 필요한 매개 변수입니다. </param>
		/// <returns>특정 객체를 반환할 수 있으면 True, 그렇지 않으면 False 입니다.</returns>
		public virtual bool CanGetter(object input) { return true; }
		/// <summary>	
		/// 	개체가 특정 객체를 설정할 수 있는지 여부 입니다.
		/// </summary>
		/// <param name="input">객체를 설정할 때 필요한 매개 변수 입니다.</param>
		/// <returns>개체가 특정 객체를 설정할 수 있으면 True, 그렇지 않으면 False</returns>
		public virtual bool CanSetter(object input) { return true; }

		/// <summary>	
		/// 	매핑을 수행할 수 있는지 여부를 가져옵니다.
		/// </summary>
		public virtual bool CanAssign { get; protected set; }

		/// <summary>
		/// 스트림을 다음으로 이동합니다.
		/// </summary>
		public abstract bool MoveNext();


		/// <summary>	
		/// 	현재 객체로 <paramref name="provider"/> 를 매핑합니다.
		/// </summary>
		/// <param name="provider"><see cref="IMappingProvider"/> 를 구현하는 객체입니다.</param>
		public virtual void AssignFrom(IMappingProvider provider)
		{
			Assign(provider, this);
		}


		/// <summary>	
		/// 	현재 객체를 <paramref name="provider"/> 로 매핑합니다.
		/// </summary>
		/// <param name="provider"><see cref="IMappingProvider"/> 를 구현하는 객체입니다.</param>
		public virtual void AssignTo(IMappingProvider provider)
		{
			Assign(this, provider);
		}

		/// <summary>	
		/// 	원본 프로바이더인 <paramref name="sourceProvider"/> 를 대상 프로바이더인 <paramref name="targetProvider"/> 로 매핑을 수행합니다.
		/// </summary>
		/// <param name="sourceProvider">	<see cref="IMappingProvider"/> 를 구현하는 원본 프로바이더 객체입니다. </param>
		/// <param name="targetProvider">	<see cref="IMappingProvider"/> 를 구현하는 원본 프로바이더 객체입니다.	</param>
		protected void Assign(IMappingProvider sourceProvider, IMappingProvider targetProvider)
		{
			var sourceKeys = sourceProvider.MappingKeys;

			//while( sourceProvider.MoveNext() && targetProvider.CanCreateNewInstance)
			//{
			//    sourceProvider.MoveNext();

			var sbDebug = new StringBuilder(2048);
			sbDebug.AppendLine("Assign");
			sbDebug.AppendLine(string.Format("sourceProvider:{0}, targetProvider:{1}", sourceProvider.GetObject().GetType(), targetProvider.GetObject().GetType()));

			targetProvider.StartOfAssign(sourceProvider, targetProvider);
			foreach (var key in sourceKeys)
			{
				if (sourceProvider.CanGetter(key) != true) continue;

				var sourceValue = sourceProvider.Getter(key);
				if (sourceValue is DBNull || sourceValue == DBNull.Value) continue;

				if (targetProvider.CanSetter(key) != true) continue;
				if (this.Mapper.ContainsKey((TInput)key))
				{
					this.GetMappingValue((TInput)key);
				}
				else
				{
					if (sourceProvider is IMappingCollectionProvider && targetProvider is IMappingCollectionProvider)
					{
						this.Map((TInput)key).Return(o => ((IMappingCollectionProvider)targetProvider).SetValues(targetProvider.CreateNewInstance(), ((IMappingCollectionProvider)sourceProvider).GetValues(sourceValue)));
						this.GetMappingValue((TInput)key);
						continue;
					}

					this.Map((TInput)key).Return(o => targetProvider.Setter(o, sourceValue));
					this.GetMappingValue((TInput) key);

					if (sourceValue != null && sourceValue.GetType() == typeof (byte[]))
					{
						sourceValue = "byte[] { " + BitConverter.ToString((byte[]) sourceValue) + " }";
					}

					sbDebug.AppendLine(string.Format("\t{0}.{1} => {2}.{3} = {4}", sourceProvider.GetObject().GetType().Name, key,
					                                 targetProvider.GetObject().GetType().Name, key,
					                                 sourceValue));
				}
			}

			targetProvider.EndOfAssign(sourceProvider, targetProvider);

			logger.Info(sbDebug);
			//}
		}
	}
}