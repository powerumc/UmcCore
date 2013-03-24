using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.Reflection.Emit
{
	public abstract class EmitProviderAbstract : IEmitProvider
	{
		public abstract void OnCreateAssembly(params object[] @object);
		public abstract void OnCreateModule(params object[] @object);
		public abstract void OnCreateType(string TypeQualifiedName);
		public abstract void OnSetParent(Type parentType);
		public abstract void OnSetInterfaces(params Type[] interfaces);
		public abstract void OnSetConstructors();
		public abstract void OnReleaseType(params object[] @object);
	}
}
