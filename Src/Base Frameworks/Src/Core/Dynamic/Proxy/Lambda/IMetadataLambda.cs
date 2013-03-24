using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.Dynamic.Proxy.Lambda
{

	/// <summary>	
	/// 	람다(Lambda) 객체의 메타데이터 정보를 기술할 수 있는 인터페이스 입니다.
	/// </summary>
	/// <typeparam name="TReturn">반환되는 객체의 타입입니다.</typeparam>
	public interface IMetadataLambda<TReturn>
	{

		/// <summary>	
		/// 	특성을 선업합니다.
		/// </summary>
		TReturn Attribute();


		/// <summary>	
		/// 	매개 변수와 함께 특성을 선언합니다.
		/// </summary>
		/// <param name="object">매개 변수 입니다.</param>
		TReturn Attribute(params object[] @object);
	}

	/// <summary>	
	/// 	람다(Lambda) 객체의 메타데이터 정보를 기술할 수 있는 인터페이스 입니다.
	/// </summary>
	public interface IMetadataLambda : IMetadataLambda<IMetadataLambda>
	{
	}
}
