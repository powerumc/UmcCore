using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.Reflection.Emit
{
	public sealed class EmbbedProxyProvider : EmitProviderAbstract
	{
		public override void OnCreateAssembly(params object[] @object)
		{
			throw new NotImplementedException();
		}

		public override void OnCreateModule(params object[] @object)
		{
			throw new NotImplementedException();
		}

		public override void OnCreateType(string TypeQualifiedName)
		{
			throw new NotImplementedException();
		}

		public override void OnSetParent(Type parentType)
		{
			throw new NotImplementedException();
		}

		public override void OnSetInterfaces(params Type[] interfaces)
		{
			throw new NotImplementedException();
		}

		public override void OnSetConstructors()
		{
			throw new NotImplementedException();
		}

		public override void OnReleaseType(params object[] @object)
		{
			throw new NotImplementedException();
		}
	}
}
