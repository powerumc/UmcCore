using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace System
{
	public delegate void ToBytesCallback(byte[] bytes);

	public static class SerializationExtension
	{
		#region ToBytes
		public static byte[] ToBinaryBytes(this object @objec)
		{
			using (var ms = new MemoryStream())
			{
				var formatter = new BinaryFormatter();
				formatter.Serialize(ms, @objec);
				ms.Position = 0;

				int read = 0;
				byte[] bData = new byte[ms.Length];
				do
				{
					read = ms.Read(bData, 0, bData.Length);

				} while (read > 0);

				return bData;
			}
		}

		public static void ToBinaryBytes(this object @object, int bufferSize)
		{
			ToBinaryBytes(@object, bufferSize, null);
		}

		public static void ToBinaryBytes(this object @object, ToBytesCallback callback)
		{
			ToBinaryBytes(@object, 1024, callback);
		}

		public static void ToBinaryBytes(this object @object, int bufferSize, ToBytesCallback callback)
		{
			using (var ms = new MemoryStream())
			{
				var formatter = new BinaryFormatter();
				formatter.Serialize(ms, @object);
				ms.Position = 0;

				int read = 0;
				byte[] bData = new byte[bufferSize];
				do
				{
					read = ms.Read(bData, 0, bData.Length);

					if (callback == null) continue;

					callback(bData);

				} while (read > 0);
			}
		} 
		#endregion

		public static object ToObject(this byte[] bytes)
		{
			return ToObject<object>(bytes);
		}

		public static T ToObject<T>(this byte[] bytes)
		{
			var ms = new MemoryStream();
			ms.Write(bytes, 0, bytes.Length);
			ms.Position = 0;

			var formatter = new BinaryFormatter();
			var obj = formatter.Deserialize(ms);

			if ((obj is T) == false) return default(T);

			return (T)obj;
		}
	}
}
