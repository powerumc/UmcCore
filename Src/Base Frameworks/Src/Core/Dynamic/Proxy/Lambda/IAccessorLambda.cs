using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Umc.Core.Reflection;

namespace Umc.Core.Dynamic.Proxy.Lambda
{
	/// <summary>
	/// <para>동적 개체에 포함되는 엑세스 한정자의 제한을 기술하는 인터페이스 입니다.</para>
	/// </summary>
	/// <typeparam name="TReturn"></typeparam>
	public interface IAccessorLambda<TReturn>
	{

		/// <summary>	
		/// 	Public 엑세스 제한자 설정을 가져옵니다.
		/// </summary>
		TReturn Public { get; }


		/// <summary>	
		/// 	Internal 엑세스 제한자 설정을 가져옵니다.
		/// </summary>
		TReturn Internal { get; }


		/// <summary>	
		/// 	Protected 엑세스 제한자 설정을 가져옵니다.
		/// </summary>
		TReturn Protected { get; }


		/// <summary>	
		/// 	Private 엑세스 제한자 설정을 가져옵니다.
		/// </summary>
		TReturn Private { get; }


		/// <summary>	
		/// 	Static 엑세스 제한자 설정을 가져옵니다.
		/// </summary>
		TReturn Static { get; }


		/// <summary>	
		/// 	엑세스 제한자에 ReadOnly 를 설정합니다.
		/// </summary>
		TReturn ReadOnly { get; }


		/// <summary>	
		/// 	타입의 정보에 Abstract 을 설정합니다.
		/// </summary>
		TReturn Abstract { get; }


		/// <summary>	
		/// 	타입의 정보에 Sealed 를 설정합니다.
		/// </summary>
		TReturn Sealed { get; }


		/// <summary>	
		/// 	타입의 구현에 Override 를 설정합니다.
		/// </summary>
		TReturn Override { get; }


		/// <summary>	
		/// 	타입의 구현에 Virtual 을 설정합니다.
		/// </summary>
		TReturn Virtual { get; }
	}

	/// <summary>
	/// <para>동적 개체에 포함되는 엑세스 한정자의 제한을 기술하는 인터페이스 입니다.</para>
	/// </summary>
	public interface IAccessorLambda : IAccessorLambda<IAccessorLambda>
	{
	}
}
