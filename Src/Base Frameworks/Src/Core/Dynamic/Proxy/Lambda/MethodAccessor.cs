using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Umc.Core.Reflection;
using System.Reflection;

namespace Umc.Core.Dynamic.Proxy.Lambda
{
	/// <summary>
	/// <para>동적 메서드에 포함되는 엑세스 한정자의 제한을 기술하는 <see cref="IAccessorLambda{TReturn}"/> 를 구현한 클래스 입니다.</para>
	/// </summary>
	public class MethodAccessor :	IAccessorLambda,
									IAccessorConfirmLambda
	{
		public MethodAttributes MethodAttribute { get; internal set; }

		private ITypeLambda typeLambda;

		public MethodAccessor(ITypeLambda typeLambda)
		{
			this.typeLambda = typeLambda;
		}

		public bool IsOverride { get; private set; }


		#region Support for IAccessorLambda
		public IAccessorLambda Public		{ get { this.MethodAttribute |= AttributeConstants.MethodAttribute.Public; return this; } } 
		public IAccessorLambda Internal		{ get { this.MethodAttribute |= AttributeConstants.MethodAttribute.Internal; return this; } } 
		public IAccessorLambda Protected	{ get { this.MethodAttribute |= AttributeConstants.MethodAttribute.Protected; return this; } } 
		public IAccessorLambda Private		{ get { this.MethodAttribute |= AttributeConstants.MethodAttribute.Private; return this; } } 
		public IAccessorLambda Static		{ get { this.MethodAttribute |= AttributeConstants.MethodAttribute.Static; return this; } } 
		public IAccessorLambda Abstract		{ get { this.MethodAttribute |= AttributeConstants.MethodAttribute.Abstract; return this; } } 
		public IAccessorLambda Virtual		{ get { this.MethodAttribute |= AttributeConstants.MethodAttribute.Virtual; return this; } }
		#endregion

		#region NotSupport for IAccessorLambda
		public IAccessorLambda Sealed		{ get { this.MethodAttribute |= 0; return this; } } 
		public IAccessorLambda ReadOnly		{ get { this.MethodAttribute |= 0; return this; } } 
		public IAccessorLambda Override		{ get { this.IsOverride = true; return this; } } 
		#endregion

		#region IAccessorConfirmLambda
		public bool IsPublic	{ get { return (this.MethodAttribute & AttributeConstants.MethodAttribute.Public) == AttributeConstants.MethodAttribute.Public; } }
		public bool IsInternal	{ get { return (this.MethodAttribute & AttributeConstants.MethodAttribute.Internal) == AttributeConstants.MethodAttribute.Internal; } }
		public bool IsStatic	{ get { return (this.MethodAttribute & AttributeConstants.MethodAttribute.Static) == AttributeConstants.MethodAttribute.Static; } }
		public bool IsAbstract	{ get { return (this.MethodAttribute & AttributeConstants.MethodAttribute.Abstract) == AttributeConstants.MethodAttribute.Abstract; } }
		public bool IsProtected { get { return (this.MethodAttribute & AttributeConstants.MethodAttribute.Protected) == AttributeConstants.MethodAttribute.Protected; } }
		public bool IsPrivate	{ get { return (this.MethodAttribute & AttributeConstants.MethodAttribute.Private) == AttributeConstants.MethodAttribute.Private; } }
		public bool IsVirtual	{ get { return (this.MethodAttribute & AttributeConstants.MethodAttribute.Virtual) == AttributeConstants.MethodAttribute.Virtual; } }

		public bool IsSealed	{ get { return true; } }
		public bool IsReadOnly	{ get { return true; } }
		#endregion
	}
}
