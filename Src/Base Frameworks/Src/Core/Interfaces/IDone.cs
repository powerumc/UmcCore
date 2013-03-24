using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core
{
	public interface IDone<TReturn>
	{
		TReturn Done();
	}

	public interface IDone
	{
		object Done();
	}
}
