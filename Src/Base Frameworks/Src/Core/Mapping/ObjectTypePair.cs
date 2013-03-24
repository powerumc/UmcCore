using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core
{
	public class ObjectTypePair
	{
		public ObjectTypePair() { }

		public ObjectTypePair(object obj, Type type)
		{
			this.Object = obj;
			this.Type = type;
		}

		public object Object { get; set; }
		public Type Type { get; set; }
	}
}
