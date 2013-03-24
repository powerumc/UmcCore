using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Umc.Core.IoC.Configuration;

namespace Umc.Core.Dynamic
{
	public class DynamicAttribute : Attribute, IDynamicAttribute
	{
		public DynamicAttribute()
		{
			this.Lifetime = LifetimeFlagType.PerCall;
		}

		public DynamicAttribute(LifetimeFlagType lifetime)
		{
			this.Lifetime = lifetime;
		}

		#region IDynamicAttribute 멤버

		public string Type { get; set; }

		public IoC.Configuration.LifetimeFlagType Lifetime { get; set; }

		#endregion
	}
}
