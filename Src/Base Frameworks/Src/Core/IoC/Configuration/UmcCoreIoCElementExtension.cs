using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;
using Umc.Core.IoC.Unity;

namespace Umc.Core.IoC.Configuration
{

	/// <summary>	
	/// 	<see cref="UmcCoreIoCElement"/> 의 확장 클래스 입니다.
	/// </summary>
	public static class UmcCoreIoCElementExtension
	{

		/// <summary>	
		/// 	<see cref="UmcCoreIoCElement"/> 의 구성 요소의 내용이 올바른지 검사합니다.
		/// </summary>
		/// <param name="root">루트 구성 요소의 참조입니다.</param>
		/// <returns>구성 요소가 올바른지에 대한 결과를 반환합니다.</returns>
		public static RootElementVerifyResult Verify(this UmcCoreIoCElement root)
		{
			if( root.containers == null ) 
				return new RootElementVerifyResult(true);

			var result = new RootElementVerifyResult(false);

            foreach (var container in root.containers)
			{
				var grouping = from r in container.register
							group r by new { r.contract, r.key } into g
							select g;

				var groupingList = grouping.ToList();
				groupingList.ForEach(o => Trace.WriteLine("contract:" + o.Key.contract + ", key:" + o.Key.key + ""));

				foreach (var g in groupingList)
				{
					var mustRegisterCount = container.register.Count( o => o.contract == g.Key.contract && o.key == g.Key.key);

					if (mustRegisterCount == 1)
						result.Result = true;
					else
					{
						result.Result = false;

						result.Add(g.First());
					}
				}

			}

			return result;
		}
	}
}
