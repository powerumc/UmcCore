using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core
{
	public interface IMappingCollectionProvider
	{
		IEnumerable<KeyValuePair<object, object>> GetValues(object input);
		void SetValues(object input, IEnumerable<KeyValuePair<object, object>> args);
		bool MoveNext();
	}
}
