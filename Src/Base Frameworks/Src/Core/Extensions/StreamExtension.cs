using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace System
{
	public static class StreamExtension
	{

		public static byte[] ReadAllBytes(this Stream stream)
		{
			var arrBytes = new Byte[stream.Length];

			stream.Read(arrBytes, 0, arrBytes.Length);

			return arrBytes;
		}

		public static object FromXmlSerialize(this Stream stream, Type type)
		{
			if (stream == null) throw new ArgumentNullException("stream");

			XmlSerializer xs = new XmlSerializer(type);

			var obj = xs.Deserialize(stream);

			return obj;
		}

		public static T FromXmlSerialize<T>(this Stream stream)
		{
			if (stream == null) throw new ArgumentNullException("stream");

			var obj = FromXmlSerialize(stream, typeof(T));
			if ((obj is T) == false) throw new InvalidCastException(string.Format("obj type is {0}, T type is {1}", obj.GetType(), typeof(T)));

			return (T) obj;
		}
	}
}
