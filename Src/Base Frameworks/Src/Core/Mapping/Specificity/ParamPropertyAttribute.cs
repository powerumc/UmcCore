using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.Mapping
{
	/// <summary>
	///		<see cref="ParamPropertyMappingProvider"/> 에서 매핑의 설명을 선언하는 클래스 입니다.
	/// </summary>
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes"), AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
	public class ParamPropertyAttribute : Attribute
	{
		/// <summary>
		///		속성의 데이터를 전달/회신할지 여부를 가져오거나 설정합니다.
		/// </summary>
		public ParamAction Action { get; set; }
		/// <summary>
		///		속성의 이름을 가져오거나 설정합니다.
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		///		이 속성이 필수인지 여부를 가져오거나 설정합니다.
		/// </summary>
		public bool IsRequired { get; set; }
		/// <summary>
		///		이 속성의 기본값을 가져오거나 설정합니다.
		/// </summary>
		public object DefaultValue { get; set; }

		public ParamPropertyAttribute() : this(null)
		{
		}

		public ParamPropertyAttribute(bool isRequired) : this(ParamAction.Send | ParamAction.Receive, null, isRequired)
		{
		}

		public ParamPropertyAttribute(string name) : this(ParamAction.Send | ParamAction.Receive, name, false)
		{
		}

		public ParamPropertyAttribute(ParamAction action, string name) : this(action, name, false)
		{
		}

		public ParamPropertyAttribute(string name, bool isRequired) : this(ParamAction.Send | ParamAction.Receive, name, isRequired)
		{
		}

		public ParamPropertyAttribute(ParamAction action, string name, bool isRequired)
		{
			this.Action     = action;
			this.Name       = name;
			this.IsRequired = isRequired;
		}
	}
}
