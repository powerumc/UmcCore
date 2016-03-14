using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core
{
	/// <summary>
	///		서로 다른 데이터소스의 매핑을 지원할 수 있는 매핑 프로바이더 인터페이스 입니다.
	/// </summary>
	public interface IMappingProvider : IGetable,
										ISetable,
										INewInstance
	{
		/// <summary>
		///		매핑에 사용할 수 있는 키를 반환합니다.
		/// </summary>
		IEnumerable<object> MappingKeys { get; }

		/// <summary>
		///		매핑 프로바이더의 매핑 데이터소스를 변경합니다.
		/// </summary>
		/// <param name="object">데이터소스로 변경할 객체입니다.</param>
		void SetObject(object @object);

		/// <summary>
		///		매핑 프로바이더의 매핑 데이터소스를 반환합니다.
		/// </summary>
		/// <returns>매핑 프로바이더와 연결된 객체를 반환합니다.</returns>
		object GetObject();

		/// <summary> 
		///		대상 개체의 데이터소스로부터 현재 개체의 데이터소스로 매핑을 수행합니다.
		/// </summary>
		/// <param name="provider"><see cref="IMappingProvider"/> 를 구현하는 매핑 프로바이더 입니다.</param>
		void AssignFrom(IMappingProvider provider);

		/// <summary>
		///		현재 개체의 데이터소스로부터 대상 개체의 데이터소스로 매핑을 수행합니다.
		/// </summary>
		/// <param name="provider"><see cref="IMappingProvider"/> 를 구현하는 매핑 프로바이더 입니다.</param>
		void AssignTo(IMappingProvider provider);

		/// <summary>
		///		매핑이 수행되기 전에 초기화할 수 있는 작업입니다.
		/// </summary>
		/// <param name="sourceProvider"><see cref="IMappingProvider"/> 를 구현하는 원본 매핑 프로바이더 입니다.</param>
		/// <param name="targetProvider"><see cref="IMappingProvider"/> 를 구현하는 대상 매핑 프로바이더 입니다.</param>
		void StartOfAssign(IMappingProvider sourceProvider, IMappingProvider targetProvider);

		/// <summary>
		///		매핑이 수행된 후의 작업입니다.
		/// </summary>
		/// <param name="sourceProvider"><see cref="IMappingProvider"/> 를 구현하는 원본 매핑 프로바이더 입니다.</param>
		/// <param name="targetProvider"><see cref="IMappingProvider"/> 를 구현하는 대상 매핑 프로바이더 입니다.</param>
		void EndOfAssign(IMappingProvider sourceProvider, IMappingProvider targetProvider);

		/// <summary>
		/// 새로운 객체를 생성할 수 있는지 여부를 나타냅니다.
		/// </summary>
		bool CanCreateNewInstance { get; }

		object CreateNewInstance();
	}
}
