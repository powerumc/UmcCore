using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core
{
    /// <summary>
    ///		객체를 최종 릴리즈 하기 위한 용도로 사용하는 인터페이스입니다.
    /// </summary>
    /// <typeparam name="TReturn">반환되는 개체의 타입입니다.</typeparam>
    public interface IReleasable<TReturn>
    {

        /// <summary>	
        /// 	객체를 최종 릴리즈 합니다.
        /// </summary>
        /// <returns>	
        /// 	릴리즈된 개체를 반환합니다.
        /// </returns>
        TReturn Release();
    }


	/// <summary>	
	/// 	객체를 최종 릴리즈 하기 위한 용도로 사용하는 인터페이스입니다.
	/// </summary>
	public interface IReleasable
	{

		/// <summary>	
		/// 	객체를 최종 릴리즈 합니다.
		/// </summary>
		void Release();
	}
}
