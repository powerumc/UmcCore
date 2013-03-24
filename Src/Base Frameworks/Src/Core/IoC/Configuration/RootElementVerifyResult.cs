using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umc.Core.IoC.Configuration
{
	public class RootElementVerifyResult
	{
		public bool Result { get; internal set; }
		public IList<RegisterElement> InvalidRegisterElement = new List<RegisterElement>();

		public RootElementVerifyResult(bool result)
		{
			this.Result = result;
		}

		internal void Add(RegisterElement element)
		{
			this.InvalidRegisterElement.Add(element);
		}

		public IEnumerable<RegisterElement> DuplicateRegisterElement()
		{
			return this.InvalidRegisterElement.AsEnumerable();
		}
	}
}
