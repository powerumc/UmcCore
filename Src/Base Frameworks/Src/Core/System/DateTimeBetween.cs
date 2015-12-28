using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Umc.Core;

namespace System
{
	public class DateTimeBetween
	{
		public DateTime BeginDate { get; set; }
		public DateTime Now { get; set; }
		public DateTime EndDate { get; set; }
		private readonly int compareType = 0;

		public DateTimeBetween() { }
		public DateTimeBetween(DateTime begin, DateTime end)
		{
			this.BeginDate = begin;
			this.EndDate = end;
			this.compareType = 0;
		}

		public DateTimeBetween(DateTime begin, DateTime now, DateTime end)
		{
			this.BeginDate = begin;
			this.Now = now;
			this.EndDate = end;
			this.compareType = 1;
		}

		public bool IsBetween
		{
			get
			{
				if (this.compareType == 0) return this.BeginDate.Between(this.EndDate);
				return this.compareType == 1 && this.BeginDate.Between(this.Now, this.EndDate);
			}
		}

		public bool IsOver
		{
			get { 
				if (this.compareType == 1) return this.Now > this.EndDate; 
				throw new NullReferenceException("Now");
			}
		}

		public bool IsBetweenOrOver
		{
			get { return IsBetween || IsOver; }
		}
	}
}
