﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#if NET40
using System.Collections.Concurrent;

#endif

namespace Umc.Core.IoC.Catalog
{
	internal class FrameworkDynamicTypeCatalog : FrameworkTypeCatalog
	{
		public FrameworkDynamicTypeCatalog(Type[] types)
			: this(types.AsEnumerable())
		{
		}

		public FrameworkDynamicTypeCatalog(IEnumerable<Type> types)
			: base(types)
		{
		}


		/// <summary>	
		/// 	<para>관리되는 대상 목록의 조건에 만족하는 타입을 반환합니다. </para>
		///		<para>.NET Framework 4.0 빌드는 병렬적으로 작업을 처리합니다.</para>
		/// </summary>
		/// <returns>	
		/// 	조건에 만족하는 타입 목록입니다. 
		/// </returns>
		public override IEnumerable<Type> GetMatchingTypes()
		{
			foreach (var type in this.types)
			{
				if (type.IsDynamicAttribute())
				{
					yield return type;
				}
			}
		}
	}
}
