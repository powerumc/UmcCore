using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Umc.Core.Reflection;
using System.Reflection;

namespace Umc.Core.Dynamic.Proxy.Lambda
{
	/// <summary>
	/// <para>동적 필드에 포함되는 엑세스 한정자의 제한을 기술하는 <see cref="IAccessorLambda{TReturn}"/> 를 구현한 클래스 입니다.</para>
	/// </summary>
	public class FieldAccessor :	IAccessorLambda,
									IAccessorConfirmLambda
	{
		public FieldAttributes FieldAttribute;

		private ITypeLambda typeLambda;

		public FieldAccessor(ITypeLambda typeLambda)
		{
			this.typeLambda = typeLambda;
		}

		#region Support for IAccessorLambda
		public IAccessorLambda Public		{ get { this.FieldAttribute |= AttributeConstants.FieldAttribute.Public; return this; } }
		public IAccessorLambda Internal		{ get { this.FieldAttribute |= AttributeConstants.FieldAttribute.Internal; return this; } }
		public IAccessorLambda Protected	{ get { this.FieldAttribute |= AttributeConstants.FieldAttribute.Protected; return this; } }
		public IAccessorLambda Private		{ get { this.FieldAttribute |= AttributeConstants.FieldAttribute.Private; return this; } }
		public IAccessorLambda Static		{ get { this.FieldAttribute |= AttributeConstants.FieldAttribute.Static; return this; } }
		public IAccessorLambda ReadOnly		{ get { this.FieldAttribute |= AttributeConstants.FieldAttribute.ReadOnly; return this; } }
		#endregion

		#region NotSupport for IAccessorLambda
		public IAccessorLambda Abstract		{ get { this.FieldAttribute |= 0; return this; } } 
		public IAccessorLambda Sealed		{ get { this.FieldAttribute |= 0; return this; } } 
		public IAccessorLambda Override		{ get { this.FieldAttribute |= 0; return this; } } 
		public IAccessorLambda Virtual		{ get { this.FieldAttribute |= 0; return this; } }
		#endregion

		#region IAccessorConfirmLambda
		public bool IsPublic	{ get { return (this.FieldAttribute & AttributeConstants.FieldAttribute.Public) == AttributeConstants.FieldAttribute.Public; } }
		public bool IsInternal	{ get { return (this.FieldAttribute & AttributeConstants.FieldAttribute.Internal) == AttributeConstants.FieldAttribute.Internal; } }
		public bool IsStatic	{ get { return (this.FieldAttribute & AttributeConstants.FieldAttribute.Static) == AttributeConstants.FieldAttribute.Static; } }
		public bool IsProtected { get { return (this.FieldAttribute & AttributeConstants.FieldAttribute.Protected) == AttributeConstants.FieldAttribute.Protected; } }
		public bool IsPrivate	{ get { return (this.FieldAttribute & AttributeConstants.FieldAttribute.Private) == AttributeConstants.FieldAttribute.Private; } }
		public bool IsReadOnly	{ get { return (this.FieldAttribute & AttributeConstants.FieldAttribute.ReadOnly) == AttributeConstants.FieldAttribute.ReadOnly; } }

		public bool IsSealed	{ get { return true; } }
		public bool IsOverride	{ get { return true; } }
		public bool IsAbstract	{ get { return true; } }
		public bool IsVirtual	{ get { return true; } }
		#endregion
	}
}
