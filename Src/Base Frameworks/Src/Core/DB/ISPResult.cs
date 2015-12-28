using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.Games.DB
{
	public interface ISPResult
	{
		int SPErrorCode { get; set; }
		string SPErrorMessage { get; set; }
		int SPReturnValue { get; set; }
	}
}
