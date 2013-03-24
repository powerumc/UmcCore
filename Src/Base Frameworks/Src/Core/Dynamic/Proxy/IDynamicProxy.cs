using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Umc.Core.Dynamic.Proxy.Lambda;
using Umc.Core.Reflection.Emit;

namespace Umc.Core.Dynamic.Proxy
{
    /// <summary>
    /// <para>Dynamic Proxy 를 생성하기 위한 인터페이스 입니다.</para>
    /// </summary>
    public interface IDynamicProxy<T>
        where T : class
    {

        /// <summary>	
        /// 	동적 프락시 객체를 생성합니다.
        /// </summary>
        /// <returns>	
        /// 	동적 프락시 객체의 <see cref="IReleaseType"/> 입니다.
        /// </returns>
        T CreateProxy();


        /// <summary>	
        /// 	동적 프락시 객체를 생성합니다. 
        /// </summary>
		/// <param name="object">동적 프락시를 생성할 때 필요한 매개 변수 입니다</param>
        /// <returns>	
        /// 	동적 프락시 객체의 <see cref="IReleaseType"/> 입니다. 
        /// </returns>
        T CreateProxy(params object[] @object);
    }
}
