using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Umc.Core.Dynamic.Proxy
{
	/// <summary>
	/// <para>동적 개체의 프락시를 제공하기 위해 이 클래스를 상속해야 합니다.</para>
	/// </summary>
	/// <typeparam name="T"></typeparam>
    public abstract class DynamicProxyBase<T> : IDynamicProxy<T>
		, ISupportable 
		where T : class
    {
		protected DynamicAssemblyContext<T> AssemblyContext { get; set; }
		protected DynamicModuleContext ModuleContext { get; set; }

		private Guid assemblyGuid = Guid.NewGuid();
		private Guid moduleGuid = Guid.NewGuid();

		internal virtual string AssemblyName
		{
			get
			{
				return String.Concat("DynamicProxyAssembly_", assemblyGuid.ToString("N"));
			}
		}

		internal virtual string ModuleName
		{
			get
			{
				return String.Concat("DynamicProxyModule_", typeof(T).Name, "_", moduleGuid.ToString("N"));
			}
		}


		protected void Save()
		{
			if( this.AssemblyContext == null )
				throw new NullReferenceException(this.AssemblyContext.GetType().Name);

			if (this.AssemblyContext.CanSave == false)
				throw new DynamicProxyException(String.Format(ExceptionRS.O_동적프락시_어셈블리는_저장할수없습니다, this.AssemblyName));

			this.AssemblyContext.Save();
		}



		#region IDynamicProxy

		public T CreateProxy()
		{
			return CreateProxy(null);
		}

		public T CreateProxy(params object[] @object)
		{
			throw new NotImplementedException();
		} 
		#endregion

		public abstract bool IsSupport { get; }
	}
}