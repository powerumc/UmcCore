using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace Umc.Core.IoC.Catalog
{

    /// <summary>	
    /// 	파일 시스템의 디렉토리 수준에서 관리 대상의 목록을 관리하는 클래스 입니다.
    /// </summary>
    public class FrameworkDirectoryCatalog : FrameworkCatalog
    {
        public string Path { get; private set; }
        public string SearchPattern { get; private set; }

        public FrameworkDirectoryCatalog(string path)
            : this(path, "*.dll")
        {
        }

        public FrameworkDirectoryCatalog(string path, string searchPattern)
        {
            this.Path          = path;
            this.SearchPattern = searchPattern;
        }


        /// <summary>	
        /// 	관리되는 대상 목록의 조건에 만족하는 타입을 반환합니다. 
        /// </summary>
        /// <returns>	
        /// 	조건에 만족하는 타입 목록입니다. 
        /// </returns>
        /// <exception cref="FrameworkDependencyException">어셈블리를 찾을 수 없는 경우 발생하는 예외 입니다.</exception>
        public override IEnumerable<Type> GetMatchingTypes()
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
            AppDomain.CurrentDomain.AssemblyLoad    += CurrentDomain_AssemblyLoad;

            try
            {
                var files = Directory.GetFiles(this.Path, this.SearchPattern);

                foreach (var file in files)
                {
                    if (File.Exists(file) == false)
                    {
                        throw new FileNotFoundException(file);
                    }

                    var assemblyBytes = File.ReadAllBytes(file);
                    Assembly assembly = null; 
                    try
                    {
                        assembly = Assembly.Load(assemblyBytes);
                    }
                    catch ( BadImageFormatException ex )
                    {
                        Debug.WriteLine(ex.FileName);
                        continue;
                    }
                    
                    var catalog       = new FrameworkAssemblyCatalog(assembly);
                    var types         = catalog.GetMatchingTypes();

                    foreach (var type in types)
                    {
                        yield return type;
                    }
                }

            }
            finally
            {
                AppDomain.CurrentDomain.AssemblyLoad    -= CurrentDomain_AssemblyLoad;
                AppDomain.CurrentDomain.AssemblyResolve -= CurrentDomain_AssemblyResolve;
            }
        }
        
        void CurrentDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            Trace.WriteLine("FrameworkDirectoryCatalog : " + args.LoadedAssembly.FullName + " 어셈블리를 로드합니다.");
        }

        Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            Trace.WriteLine("FrameworkDirectoryCatalog : " + args.Name + " 어셈블리를 찾을 수 없습니다");
            
            throw new FrameworkDependencyException(ExceptionRS.O_을_찾을_수_없습니다, args.Name + " "+MessageRS.어셈블리);
        }
    }
}