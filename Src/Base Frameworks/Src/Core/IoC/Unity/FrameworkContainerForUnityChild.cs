using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;

namespace Umc.Core.IoC
{
	/// <summary>
	///		Unity Application Block 의 Container 에 자식 컨테이너를 추가하기 위한 클래스 입니다.
	/// </summary>
	public class FrameworkContainerForUnityChild : FrameworkContainerForUnity
	{
		private FrameworkContainer<IUnityContainer> parentContainer;

		public FrameworkContainerForUnityChild(object key)
			: base(key)
		{
		}

		public FrameworkContainerForUnityChild(object key, IFrameworkContainer parentContainer)
			: base(key, parentContainer)
		{
		}


		protected override IUnityContainer CreateContainer(FrameworkContainer<IUnityContainer> parentContainer)
		{
			return parentContainer.ContainerObject.CreateChildContainer();
		}
	}
}
