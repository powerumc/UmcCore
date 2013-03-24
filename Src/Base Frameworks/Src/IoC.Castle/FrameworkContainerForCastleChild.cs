using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Windsor;

namespace Umc.Core.IoC.Castle
{
	/// <summary>
	///		Castle Windsor의 Container 에 자식 컨테이너를 추가하기 위한 클래스 입니다.
	/// </summary>
	public class FrameworkContainerForCastleChild : FrameworkContainerForCastle
	{
		public FrameworkContainerForCastleChild(object key)
			: base(key)
		{
		}

		private FrameworkContainer<IWindsorContainer> parentContainer;
		public FrameworkContainerForCastleChild(object key, IFrameworkContainer parentContainer)
			: base(key, parentContainer)
		{
		}


		protected override IWindsorContainer CreateContainer(FrameworkContainer<IWindsorContainer> parentContainer)
		{
			IWindsorContainer childContainer = new WindsorContainer();
			parentContainer.ContainerObject.AddChildContainer(childContainer);

			return childContainer;
		}
	}
}
