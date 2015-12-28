using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Umc.Core.Mapping
{
	public class MappingProviderForDataTable : MappingProvider<int, DataRow>, IMappingCollectionProvider
	{
		protected DataTable dt                        = null;
		private MappingProviderForDataRow innerMapper = null;
		private IEnumerable<object> innerMappingKey   = null;
		int currentRow = 0;
		int maxRow;
		bool hasColumns = false;

		public MappingProviderForDataTable(DataTable dt)
		{
			this.dt          = dt;
			this.maxRow      = dt.Rows.Count;
			this.innerMapper = new MappingProviderForDataRow(this.dt);

			this.Initialize();
		}

		public override void SetObject(object @object)
		{
			this.dt = @object as DataTable;

			if ( this.dt == null )
				throw new MappingException("object is null");
		}

		public override object GetObject()
		{
			return this.dt;
		}

		public override IEnumerable<object> MappingKeys
		{
			get { return Enumerable.Range(0, this.dt.Rows.Count).Cast<object>(); }
		}

		public override object CreateNewInstance()
		{
			return this.dt.NewRow();
		}

		public override bool CanGetter(object input)
		{
			return currentRow++ < maxRow;
		}

		public override object Getter(object input)
		{
			return this.dt.Rows[(int)input];
		}

		public override void Setter(object input, object arg)
		{
			this.dt.Rows.Add((DataRow)input);
		}

		public IEnumerable<KeyValuePair<object, object>> GetValues(object input)
		{
			var datarow = input as DataRow;
			if ( datarow == null ) throw new ArgumentNullException("input");

			foreach ( var key in this.innerMappingKey )
			{
				yield return new KeyValuePair<object, object>(key, datarow[key.ToString()]);
			}
		}

		public void SetValues(object input, IEnumerable<KeyValuePair<object, object>> args)
		{
			var datarow = input as DataRow;
			if ( datarow == null ) throw new ArgumentNullException("input");

			if ( hasColumns == false )
			{
				foreach ( var keyvalue in args )
				{
					if ( this.dt.Columns.Contains(keyvalue.Key.ToString()) ) continue;
					this.dt.Columns.Add(keyvalue.Key.ToString());
				}
				this.hasColumns = true;
			}

			foreach ( var keyvalue in args )
			{
				datarow[keyvalue.Key.ToString()] = keyvalue.Value;
			}
			this.Setter(input, datarow);
		}

		public override void Initialize()
		{
			if ( this.innerMappingKey != null ) return;

			this.innerMappingKey = innerMapper.MappingKeys.ToList().AsEnumerable();
		}

		public override bool IsMatches(int input)
		{
			return true;
		}

		public override bool MoveNext()
		{
			return true;
		}
	}
}
