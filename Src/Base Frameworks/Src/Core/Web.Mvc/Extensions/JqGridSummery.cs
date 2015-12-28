using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Collections;

namespace Umc.Core.Web.Mvc
{
	[DataContract]
	[XmlRoot(ElementName="rows")]
	public class JqGridSummery
	{
		private IEnumerable list;

		public JqGridSummery() {}

		public JqGridSummery(int page, int total, int records)
		{
			this.page = page;
			this.total = total;
			this.records = records;
		}

		public JqGridSummery(int page, int total, int records, IEnumerable list) 
			: this(page, total, records)
		{
			this.list = list;
		}

		[DataMember(Name = "total", Order = 0)]
		[XmlAttribute(AttributeName = "total")]
		public int total { get; set; }

		[DataMember(Name="page", Order=1)]
		[XmlElement(ElementName="page")]
		public int page { get; set; }
		
		[DataMember(Name="records", Order=2)]
		[XmlElement(ElementName="records")]
		public int records { get; set; }
		
		[DataMember(Name="rows", Order=3)]
		[XmlArrayItem(ElementName="row")]
		public IEnumerable<object> rows
		{
			get
			{
				var ienum = list.GetEnumerator();
				int index = 0;
				while ( ienum.MoveNext() )
				{
					yield return new JqGridSummeryRows() { id = index++, cell = ienum.Current.GetType().GetProperties().Select(o => o.GetValue(ienum.Current, null)).ToArray() };
				}

				this.total = index + 1;
			}
		}

		public class JqGridSummeryRows
		{
			public int id { get; set; }
			public object[] cell { get; set; }
		}
	}
}