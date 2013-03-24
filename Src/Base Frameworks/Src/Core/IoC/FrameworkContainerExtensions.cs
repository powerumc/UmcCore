using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Configuration;
using System.Xml.Linq;
using System.IO;
#if NET40
using System.Collections.Concurrent;
#endif

namespace Umc.Core.IoC
{

	/// <summary>
	///		<para><see cref="IFrameworkContainer"/> 인터페이스의 확장 메서드입니다.</para>
	/// </summary>
	public static class FrameworkContainerExtensions
	{
#if NET40
		/// <summary>
		///		<para><see cref="IFrameworkContainer"/> 컨테이너에 등록할 개체를 Task Parallel Library 를 이용하여 병렬로 처리합니다.</para>
		///		<para>단, 컨테이너에 개체를 등록할 때 CPU Process 의 개수를 이용하여 등록합니다.</para>
		///		<para>단, 오버헤드가 높을 수 있는 작업이므로 <see cref="IFrameworkContainer"/> 의 내부적인 모든 작업을 병렬화 합니다.</para>
		///		<para>단, 병렬 작업은 .NET Framework 4.0 빌드에서만 동작합니다.</para>
		/// </summary>
		/// <param name="container"></param>
		/// <param name="action"></param>
		public static void RegisterTypeAsParallel(this IFrameworkContainer container, IEnumerable<Action> action)
		{
			ConcurrentQueue<Exception> exceptions = null;
			try
			{
				exceptions = new ConcurrentQueue<Exception>();

				try
				{
					action.AsParallel()
							.WithDegreeOfParallelism(Environment.ProcessorCount)
							.WithExecutionMode(ParallelExecutionMode.ForceParallelism)
							.ForAll( o => o());
					
				}
				catch (Exception ex)
				{
					exceptions.Enqueue(ex);
				}
			}
			catch (Exception)
			{
				if( exceptions != null )
					exceptions.ToList().ForEach( o => Trace.WriteLine( o.Message ));
				
				throw;
			}
		}

#endif

		public static void LoadConfigurationFile(string path)
		{
			throw new NotImplementedException("LoadConfigurationFile");
		}

	}
}
