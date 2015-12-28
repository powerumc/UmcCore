using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Diagnostics;
using Umc.Core.Mapping;

namespace System.Data
{
	public static class DataExtension
	{
		public static void CreateColumn(this DataTable dt, Type type)
		{
			foreach(var prop in type.GetProperties())
			{
				if ( dt.Columns.Contains(prop.Name) )
					dt.Columns.Remove(prop.Name);

				dt.Columns.Add(prop.Name, prop.PropertyType);
			}
		}

		public static IList<TModel> ToDataModel<TModel>(this DataTable dt)
		{
			var list = new List<TModel>();

			var source = new MappingProviderForDataTable(dt);
			var target = new MappingProviderForCollection<TModel>(list);
			source.AssignTo(target);

			return list;
		}

		public static DataTable ToDataTable<TModel>(this IList<TModel> list)
		{
			var dt = new DataTable();
			ToDataTable<TModel>(list, dt);
			
			return dt;
		}

		public static void ToDataTable<TModel>(this IList<TModel> list, DataTable dt)
		{
			var source = new MappingProviderForDataTable(dt);
			var target = new MappingProviderForCollection<TModel>(list);
			target.AssignTo(source);
		}
	}
}
