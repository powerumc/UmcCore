using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Umc.Core.Dynamic.Proxy
{
    public delegate void DynamicAssemblyDelegate(Assembly assembly);
    public delegate void DynamicModuleDelegate(Module module);
    public delegate void DynamicTypeDelegate(Type type);

}
