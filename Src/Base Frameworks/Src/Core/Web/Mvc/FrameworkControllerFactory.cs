using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using Umc.Core.IoC;

namespace Umc.Core.Web.Mvc
{
    /// <summary>
    ///     종속성 주입을 할 수 있는 컨트롤로 팩토리 클래스 입니다.
    ///     이 클래스는 global.asax.cs 에서 아래의 예제 코드 처럼 설정합니다.
    ///     설정을 완료하면 자동으로 모든 컨트롤로는 종속성 주입을 통해 객체를 반환합니다.
    /// </summary>
    /// <example>
    /// <code>
    ///     var container = ConfigureContainer();
    ///     ControllerBuilder.Current.SetControllerFactory(new FrameworkControllerFactory(container));
    /// </code>
    /// </example>
    /// <seealso cref="T:System.Web.Mvc.DefaultControllerFactory"/>
    public class FrameworkControllerFactory : DefaultControllerFactory
    {
        private readonly IFrameworkContainer container;

        public FrameworkControllerFactory(IFrameworkContainer container)
        {
            this.container = container;
        }

        #region Overrides of DefaultControllerFactory

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null) throw new ArgumentNullException(nameof(controllerType));

            var controller = container.Resolve(controllerType);
            return controller as IController;
        }

        #endregion
    }
}
