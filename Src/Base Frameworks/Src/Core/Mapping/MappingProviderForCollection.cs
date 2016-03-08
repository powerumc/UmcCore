using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Umc.Core.Mapping
{
	public class MappingProviderForCollection<TModel> : MappingProvider<int, TModel>, IMappingCollectionProvider
	{
		private IList<TModel> list = null;
		private IEnumerable<object> innerMappingKeys = null;
		private MappingProviderForProperty innerMapper = null;
		int currentRow = 0;
		int maxRow;

		public MappingProviderForCollection(IList<TModel> list)
		{
			this.list   = list;
			this.maxRow = list.Count;

			innerMapper = new MappingProviderForProperty(this.CreateNewInstance(), StringComparer.OrdinalIgnoreCase);

			this.Initialize();
		}

		public override void SetObject(object @object)
		{
			this.list = @object as IList<TModel>;
		}

		public override object GetObject()
		{
			return this.list;
		}

		public override IEnumerable<object> MappingKeys
		{
			get { return Enumerable.Range(0, this.list.Count).Cast<object>(); }
		}

		public override object CreateNewInstance()
		{
			return Activator.CreateInstance<TModel>();
		}

		public override bool CanGetter(object input)
		{
			return currentRow++ < maxRow;
		}

		public override object Getter(object input)
		{
			int index = (int)input;

			return list[index];
		}

		public override void Setter(object input, object arg)
		{
			if ( ( arg is TModel ) == false ) throw new InvalidCastException("input type is not " + typeof(TModel).Name);

			this.list.Add((TModel)arg);
		}

		public override void Initialize()
		{
			if ( this.innerMappingKeys != null ) return;

			this.innerMappingKeys = innerMapper.MappingKeys.ToList().AsEnumerable();

			var entityType = typeof(TModel);
			innerMapper.SetObject(Activator.CreateInstance(entityType));
		}

		public IEnumerable<KeyValuePair<object, object>> GetValues(object input)
		{
			if ( input == null ) throw new ArgumentNullException("input");

			this.innerMapper.SetObject(input);

			foreach ( var innerKey in this.innerMappingKeys )
			{
				yield return new KeyValuePair<object, object>(innerKey, this.innerMapper.Getter(innerKey));
			}
		}

		public void SetValues(object input, IEnumerable<KeyValuePair<object, object>> args)
		{
			if ( input == null ) throw new ArgumentNullException("input");
			if ( ( input is TModel ) == false ) throw new InvalidCastException("input can not casting " + input.GetType());

			innerMapper.SetObject(input);
			foreach ( var keyvalue in args)
			{
				innerMapper.Setter(keyvalue.Key, keyvalue.Value);
			}
			this.Setter(input, innerMapper.GetObject());
		}

		public override bool MoveNext()
		{
			return true;
		}

		public override bool IsMatches(int input)
		{
			return true;
		}
	}
}