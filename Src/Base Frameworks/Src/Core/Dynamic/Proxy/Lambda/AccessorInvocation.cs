using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.Dynamic.Proxy.Lambda
{
	/// <summary>
	///		<para>각 동적 개체에 대한 엑세스 한정자의 제한을 관리하는 클래스 입니다.</para>
	///		<para>이 클래스는 다음의 엑세스 한정자의 제한을 기술하는 클래스를 서술합니다.</para>
	///		<list type="bullet">
	///			<item><description><see cref="TypeAccessor"/></description></item>
	///			<item><description><see cref="MethodAccessor"/></description></item>
	///			<item><description><see cref="FieldAccessor"/></description></item>
	///		</list>
	/// </summary>
	/// <example>
	///		<code>
	///			AccessorInvocation accessorInvocation = new AccessorInvocation(this.typeLambda, typeAccessor, methodAccessor, fieldAccessor);
	///			var mockTypeLambda = accessorInvocation.Public;
	///			mockTypeLambda = accessorInvocation.Virtual;
	///		</code>
	/// </example>
	public class AccessorInvocation : IAccessorLambda
	{
		private IEnumerable<IAccessorLambda> accessors;
		private ITypeLambda typeLambda;

		private Func<IEnumerable<IAccessorLambda>, IAccessorLambda> func = (accessor) =>
		{
			IAccessorLambda typeLambda = null;
			accessor.All(o => { typeLambda = o.Public; return true; });
			
			return typeLambda;
		};


		public AccessorInvocation(ITypeLambda typeLambda, params IAccessorLambda[] accessors)
			: this(typeLambda, accessors.AsEnumerable())
		{
		}

		public AccessorInvocation(ITypeLambda typeLambda, IEnumerable<IAccessorLambda> accessors)
		{
			this.typeLambda = typeLambda;
			this.accessors = accessors;
		}

		private IAccessorLambda InvokeAccessor(System.Linq.Expressions.Expression<Func<IAccessorLambda, IAccessorLambda>> invokeExpression)
		{
			accessors.All(o => { invokeExpression.Compile()(o); return true; });

			//return this.typeLambda;
			return this;
		}

		#region IAccessorLambda Interface
		public IAccessorLambda Public		{ get { return this.InvokeAccessor(o => o.Public); } } 
		public IAccessorLambda Internal		{ get { return this.InvokeAccessor(o => o.Internal); } }
		public IAccessorLambda Protected	{ get { return this.InvokeAccessor(o => o.Protected); } }
		public IAccessorLambda Private		{ get { return this.InvokeAccessor(o => o.Private); } }
		public IAccessorLambda Static		{ get { return this.InvokeAccessor(o => o.Static); } }
		public IAccessorLambda ReadOnly		{ get { return this.InvokeAccessor(o => o.ReadOnly); } }
		public IAccessorLambda Abstract		{ get { return this.InvokeAccessor(o => o.Abstract); } }
		public IAccessorLambda Sealed		{ get { return this.InvokeAccessor(o => o.Sealed); } } 
		public IAccessorLambda Override		{ get { return this.InvokeAccessor(o => o.Override); } }
		public IAccessorLambda Virtual		{ get { return this.InvokeAccessor(o => o.Virtual); } }
		#endregion
	}
}
