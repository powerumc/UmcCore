using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Umc.Core.Dynamic.Proxy
{
    public class DynamicAssemblyEventArgs : EventArgs
    {
        public Assembly Assembly { get; private set; }

        public DynamicAssemblyEventArgs(Assembly assembly)
        {
            this.Assembly = assembly;
        }
    }

    public class DynamicModuleEventArgs : EventArgs
    {
        public Module Module { get; private set; }

        public DynamicModuleEventArgs(Module module)
        {
            this.Module = module;
        }
    }

    public class DynamicTypeEventArgs : EventArgs
    {
        public Type Type { get; private set; }

        public DynamicTypeEventArgs(Type type)
        {

        }
    }

    public class DynamicConstructorEventArgs : EventArgs
    {
        public ConstructorInfo Constructor { get; private set; }

        public DynamicConstructorEventArgs(ConstructorInfo constructor)
        {
            this.Constructor = constructor;
        }
    }

    public class DynamicMethodEventArgs : EventArgs
    {
        public MethodBase Method { get; private set; }

        public DynamicMethodEventArgs(MethodBase method)
        {
            this.Method = method;
        }
    }

    public class DynamicPropertyEventArgs : EventArgs
    {
        public PropertyInfo Property { get; private set; }

        public DynamicPropertyEventArgs(PropertyInfo property)
        {
            this.Property = property;
        }
    }

    public class DynamicFieldEventArgs : EventArgs
    {
        public FieldInfo Field { get; private set; }

        public DynamicFieldEventArgs(FieldInfo field)
        {
            this.Field = field;
        }
    }

    public class DynamicLocalFieldEventArgs : EventArgs
    {
        public DynamicLocalFieldEventArgs()
        {
        }
    }

    public class DynamicParameterEventArgs : EventArgs
    {
        public DynamicParameterEventArgs()
        {
        }
    }
}
