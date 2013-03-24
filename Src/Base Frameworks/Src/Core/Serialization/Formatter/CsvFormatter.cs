using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace System
{
	public class CsvFormatter : IFormatter
	{
		#region IFormatter 멤버

		public SerializationBinder Binder { get; set; }
		public ISurrogateSelector SurrogateSelector { get; set; }
		public StreamingContext Context { get; set; }

		public object Deserialize(IO.Stream serializationStream)
		{
			throw new NotImplementedException();
		}

		public void Serialize(IO.Stream serializationStream, object graph)
		{
			throw new NotImplementedException();
		}


		#endregion
	}
}
