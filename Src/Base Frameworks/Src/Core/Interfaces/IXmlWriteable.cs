using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Umc.Core
{
	/// <summary>
	///		XML 데이터로 쓸 수 있는 인터페이스 입니다.
	/// </summary>
	public interface IXmlWriteable
	{
		/// <summary>
		///		XML 데이터 소스로 데이터를 저장합니다.
		/// </summary>
		/// <param name="element">XML 데이터의 요소 입니다.</param>
		void WriteXml(XElement element);
	}
}
