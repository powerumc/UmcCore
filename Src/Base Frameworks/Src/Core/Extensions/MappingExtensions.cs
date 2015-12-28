using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Umc.Core.Mapping;

namespace System
{
	public static class MappingExtensions
	{
		public static void MapTo<TSource, TTarget>(this TSource source, TTarget dest)
		{
			var srcMapping = new MappingProviderForProperty(source);
			var destMapping = new MappingProviderForProperty(dest);

			srcMapping.AssignTo(destMapping);
		}
	}
}
