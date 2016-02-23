using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace Umc.Core.Testing.UnitTest
{
	public class ShowObject
	{
		protected object Object { get; set; }
		protected TextWriter Writer { get; set; }
		protected int Indent = 0;

		public ShowObject(object @object) : this(@object, Console.Out)
		{
		}

		public ShowObject(object @object, TextWriter writer)
			: this(@object, writer, 0)
		{
		}

		internal ShowObject(object @object, TextWriter writer, int indent)
		{
			this.Object = @object;
			this.Writer = writer;
			this.Indent = indent;
		}

		public void Show()
		{
			if (this.Object == null)
				return;
			this.VisitType(this.Object.GetType());
		}

		protected void VisitType(Type type)
		{
			List<Action> actions = new List<Action>();
			foreach (var property in type.GetProperties())
			{
				if (/*Type.GetTypeCode(property.PropertyType) == TypeCode.Object &&*/
					property.GetIndexParameters().Count() == 0)
				{
					this.WriteIndent();
					object propertyGetValue = property.GetValue(this.Object, null);
					this.Writer.WriteLine("P_{0} : {1}", property.Name, propertyGetValue ?? "NULL");

					if (propertyGetValue is IEnumerable)
					{
						var ienumberableObject = ((IEnumerable)propertyGetValue).GetEnumerator();

						while(ienumberableObject.MoveNext())
						{
							var currentobject = ienumberableObject.Current;

							ShowObject show = new ShowObject(currentobject, this.Writer, this.Indent + 1);
							actions.Add(new Action(show.Show));
						}
					}

					if (propertyGetValue != null &&
						property.PropertyType.FullName != typeof(object).FullName &&
						Type.GetTypeCode(property.PropertyType) == TypeCode.Object)
					{
						ShowObject show = new ShowObject(propertyGetValue, this.Writer, this.Indent + 1);
						actions.Add(new Action(show.Show));
					}
				}
			}

			actions.ForEach(action => action());
		}

		private void WriteIndent()
		{
			for (int i = 0; i < this.Indent; i++)
			{
				this.Writer.Write("\t");
			}
		}

		private void VisitValueType(TypeCode typecode)
		{
		}
	}
}
