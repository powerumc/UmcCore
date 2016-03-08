using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Umc.Core.Mapping
{
	/// <summary>
	/// <para><see cref="DataRow"/> 데이터소스로부터 매핑을 지원할 수 있는 프로바이더 클래스 입니다.</para>
	/// </summary>
	public class MappingProviderForDataRow : MappingProvider<string, object>
	{
		private DataRow datarow;
		private DataTable datatable;

		public MappingProviderForDataRow() { }

		public MappingProviderForDataRow(DataTable datatable)
		{
			this.datatable = datatable;
		}

		public MappingProviderForDataRow(DataRow datarow)
		{
			if (datarow.Table == null)
				throw new NullReferenceException("DataRow.Table");

			this.datarow = datarow;
			this.datatable = datarow.Table;
		}

		public override void SetObject(object @object)
		{
			this.datarow = @object as DataRow;

			if ( this.datarow == null )
				throw new MappingException("object is null");
		}

		public override object GetObject()
		{
			var row = this.datarow;
			return row ?? this.datatable.NewRow();
		}

		public override IEnumerable<object> MappingKeys
		{
			get
			{
				foreach (DataColumn column in this.datatable.Columns)
				{
					yield return column.ColumnName;
				}
			}
		}

		public override object Getter(object input)
		{
			if (this.datarow == null) throw new NullReferenceException("datarow");

			if (this.IsMatches(input.ToString()))
				return this.datarow[input.ToString()];

			throw new KeyNotFoundException(input.ToString());
		}

		public override void Setter(object input, object arg)
		{
			if (this.datarow == null) throw new NullReferenceException("datarow");
			
			if (this.IsMatches(input.ToString()) == false)
				throw new KeyNotFoundException(input.ToString());

			this.datarow[input.ToString()] = arg;
		}

		public override void Initialize() { }

		public override bool IsMatches(string input)
		{
			if (this.datarow == null) throw new NullReferenceException("datarow");

			return this.datarow.Table.Columns.Contains(input);
		}

		public override object CreateNewInstance()
		{
			return this.datatable.NewRow();
		}

		public override void StartOfAssign(IMappingProvider sourceProvider, IMappingProvider targetProvider)
		{
			var datarow = this.datarow ?? this.CreateNewInstance();
			this.SetObject(datarow);
		}

		public override void EndOfAssign(IMappingProvider sourceProvider, IMappingProvider targetProvider)
		{
			this.datatable.Rows.Add(this.datarow);
		}

		public override bool MoveNext()
		{
			return false;
		}
	}
}
