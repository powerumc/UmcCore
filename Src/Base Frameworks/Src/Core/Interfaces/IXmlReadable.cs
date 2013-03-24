using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Umc.Core
{
	/// <summary>
	///		XML 데이터를 읽을 수 있는 인터페이스입니다.
	/// </summary>
	public interface IXmlReadable
	{
		/// <summary>
		///		XML 데이터 소스로부터 데이터를 가져옵니다.
		/// </summary>
		/// <param name="element">XML 데이터의 요소 입니다.</param>
		XDocument ReadXml(XElement element);
	}
}