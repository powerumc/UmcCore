using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Umc.Core.Data;

namespace Umc.Core
{
	/// <summary>
	/// <para>
	///		이 특성을 선언하는 클래스는 특정 데이터베이스에 종속된 것을 나타내는 특성입니다.
	/// </para>
	/// </summary>
	/// <seealso cref="IDataModelDependOnDatabase"/>
	[AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple=false, Inherited=true)]
	public class DatabaseAttribute : Attribute, IDatabaseAttribute
	{

		/// <summary>	
		/// 	데이터베이스 이름을 가져옵니다. 
		/// </summary>
		public string Name { get; protected set; }

		public DatabaseAttribute(string databaseName)
		{
			this.Name = databaseName;
		}
	}
}
