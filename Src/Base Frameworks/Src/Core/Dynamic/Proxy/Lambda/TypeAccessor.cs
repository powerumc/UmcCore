using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Umc.Core.Reflection;
using System.Reflection;

namespace Umc.Core.Dynamic.Proxy.Lambda
{
	/// <summary>
	/// <para>동적 타입에 포함되는 엑세스 한정자의 제한을 기술하는 <see cref="IAccessorLambda{TReturn}"/> 를 구현한 클래스 입니다.</para>
	/// </summary>
	public class TypeAccessor : IAccessorLambda,
								IAccessorConfirmLambda
	{
		private ITypeLambda typeLambda;

		public TypeAttributes TypeAttributes { get; internal set; }

		public TypeAccessor(ITypeLambda typeLambda)
		{
			this.typeLambda = typeLambda;
		}

		#region Support for IAccessorLambda
		public IAccessorLambda Public		{ get { this.TypeAttributes |= AttributeConstants.TypeAttribute.Public; return this; } } 
		public IAccessorLambda Internal		{ get { this.TypeAttributes |= AttributeConstants.TypeAttribute.Internal; return this; } }
		public IAccessorLambda Static		{ get { this.TypeAttributes |= AttributeConstants.TypeAttribute.Static; return this; } } 
		public IAccessorLambda Abstract		{ get { this.TypeAttributes |= AttributeConstants.TypeAttribute.Abstract; return this; } } 
		public IAccessorLambda Sealed		{ get { this.TypeAttributes |= AttributeConstants.TypeAttribute.Selaed; return this; } } 
		#endregion

		#region NotSupport for IAccessorLambda
		public IAccessorLambda Protected	{ get { this.TypeAttributes |= 0; return this; } } 
		public IAccessorLambda Private		{ get { this.TypeAttributes |= 0; return this; } } 
		public IAccessorLambda Override		{ get { this.TypeAttributes |= 0; return this; } } 
		public IAccessorLambda Virtual		{ get { this.TypeAttributes |= 0; return this; } } 
		public IAccessorLambda ReadOnly		{ get { this.TypeAttributes |= 0; return this; } }
		#endregion

		#region IAccessorConfirmLambda
		public bool IsPublic	{ get { return (this.TypeAttributes & AttributeConstants.TypeAttribute.Public) == AttributeConstants.TypeAttribute.Public; } }
		public bool IsInternal	{ get { return (this.TypeAttributes & AttributeConstants.TypeAttribute.Internal) == AttributeConstants.TypeAttribute.Internal; } }
		public bool IsStatic	{ get { return (this.TypeAttributes & AttributeConstants.TypeAttribute.Static) == AttributeConstants.TypeAttribute.Static; } }
		public bool IsAbstract	{ get { return (this.TypeAttributes & AttributeConstants.TypeAttribute.Abstract) == AttributeConstants.TypeAttribute.Abstract; } }
		public bool IsSealed	{ get { return (this.TypeAttributes & AttributeConstants.TypeAttribute.Selaed) == AttributeConstants.TypeAttribute.Selaed; } }

		public bool IsReadOnly	{ get { return true; } }
		public bool IsProtected { get { return true; } }
		public bool IsPrivate	{ get { return true; } }
		public bool IsOverride	{ get { return true; } }
		public bool IsVirtual	{ get { return true; } }
		#endregion
	}
}
