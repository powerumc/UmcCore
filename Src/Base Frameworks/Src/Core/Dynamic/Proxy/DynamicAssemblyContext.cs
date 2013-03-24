using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;

namespace Umc.Core.Dynamic.Proxy
{

    /// <summary>	
    /// 	<para>동적 프락시를 생성하기 위한 동적 어셈블리를 관리하는 컨텍스트 클래스 입니다.</para>
    /// </summary>
    public class DynamicAssemblyContext<T> : DynamicContext, ILoadable, ISaveable, IResolvable<IEnumerable<DynamicModuleContext>>
		where T : class
	{
		private DynamicProxyBase<T> proxy;

		public DynamicAssemblyContext(DynamicProxyBase<T> proxy)
		{
			this.proxy = proxy;
		}

		public bool CanLoad { get { return false; } }

		public void Load()
		{
			if (this.CanLoad == false)
				throw new DynamicProxyException(ExceptionRS.ILoadable_인터페이스는_해당_데이터소스를_로드할수없습니다);

			this.OnLoad();
		}

		protected virtual void OnLoad()
		{
			throw new NotSupportedException("OnLoad");
		}

		public bool CanSave { get { return true; } } 

		public void Save()
		{
			if (this.CanSave == false)
				throw new DynamicProxyException(String.Format(ExceptionRS.O_동적프락시_어셈블리는_저장할수없습니다, proxy.AssemblyName));

			this.OnSave();
		}

		protected virtual void OnSave()
		{
			throw new NotImplementedException();
		}

		public bool IsDirty
		{
			get { return false; }
		}

		public IEnumerable<DynamicModuleContext> Resolve<TInput>(params TInput[] inputs)
		{
			throw new NotImplementedException();
		}
	}
}