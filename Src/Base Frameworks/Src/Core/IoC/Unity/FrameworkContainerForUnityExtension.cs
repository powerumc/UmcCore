using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Umc.Core.IoC.Configuration;
using System.IO;

namespace Umc.Core.IoC.Unity
{
	/// <summary>
	///	 Unity Container 를 사용하는 IFrameworkContainer 를 외부 설정 파일에서 설정을 로드할 수 있는 확장 클래스 입니다.
	/// </summary>
	public static class FrameworkContainerForUnityExtension
	{

		/// <summary>	
		/// 	구성 파일에서 <see cref="IFrameworkContainer"/> 로 구성을 합니다.
		/// </summary>
		/// <remarks>	
		/// 	Umc, 2011-01-13. 
		/// </remarks>
		/// <param name="container">	The container to act on. </param>
		/// <param name="path">	Full pathname of the file. </param>
		public static void Load(this IFrameworkContainer container, string path)
		{
			container = SetupFrameworkContainer(path, ref container);

			var xs = new XmlSerializer(typeof(UmcCoreIoCElement));
            var containerElement = (UmcCoreIoCElement)xs.Deserialize(File.OpenRead(path));

			containerElement.Verify();

			IFrameworkComposable resolver = new FrameworkCompositionResolverForUnity((FrameworkContainerForUnity)container, containerElement);
			resolver.Compose();
		}

		private static IFrameworkContainer SetupFrameworkContainer(string path, ref IFrameworkContainer container)
		{
			if (File.Exists(path) == false)
				throw new FileNotFoundException(path);

			if (container == null)
				container = new FrameworkContainerForUnity();

			return container;
		}
	}
}
