using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.DataModels
{
	public class ImpactDetailCollection : List<ImpactDetail>
	{
	}

	public class ImpactDetail
	{
		public string Description { get; set; }
		public string FaultCode { get; set; }
		public string Object { get; set; }
		public string Severity { get; set; }
	}
}
