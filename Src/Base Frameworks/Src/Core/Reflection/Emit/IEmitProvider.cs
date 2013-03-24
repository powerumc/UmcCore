using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.Reflection.Emit
{
	/// <summary>
	/// Emit 바이트 코드를 처리할 프로바이더를 구현하는 인터페이스 입니다.
	/// </summary>
	public interface IEmitProvider
	{
		void OnCreateAssembly(params object[] @object);

		void OnCreateModule(params object[] @object);

		void OnCreateType(string TypeQualifiedName);

		void OnSetParent(Type parentType);

		void OnSetInterfaces(params Type[] interfaces);

		void OnSetConstructors();

		void OnReleaseType(params object[] @object);
	}
}
