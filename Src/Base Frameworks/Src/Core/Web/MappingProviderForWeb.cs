using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using Umc.Core.Mapping;

namespace Umc.Core.Games.Web
{
	public class MappingProviderForWeb : MappingProvider<string, string>
	{
		private readonly HttpRequest request;

		private IDictionary<string, string> FormDataDictinoary = new Dictionary<string, string>();

		public MappingProviderForWeb(HttpRequest request)
		{
			this.request = request;

			this.Initialize();
		}

		#region Overrides of MappingProvider<string,string>

		/// <summary>	
		/// 	초기화 작업을 수행합니다.
		/// </summary>
		public override void Initialize()
		{
			foreach (var key in request.QueryString.AllKeys)
			{
				FormDataDictinoary.Add(key, request.QueryString.Get(key));
			}

			foreach (var key in request.Form.AllKeys)
			{
				FormDataDictinoary.Add(key, request.Form.Get(key));
			}
		}

		/// <summary>
		///		매핑 프로바이더의 매핑 데이터소스를 변경합니다.
		/// </summary>
		/// <param name="object">데이터소스로 변경할 객체입니다.</param>
		public override void SetObject(object @object)
		{
			throw new NotSupportedException();
		}

		/// <summary>
		///		매핑 프로바이더의 매핑 데이터소스를 반환합니다.
		/// </summary>
		/// <returns>매핑 프로바이더와 연결된 객체를 반환합니다.</returns>
		public override object GetObject()
		{
			return this.request;
		}

		/// <summary>
		///		매핑에 사용할 수 있는 키를 반환합니다.
		/// </summary>
		public override IEnumerable<object> MappingKeys
		{
			get { return FormDataDictinoary.Keys.Cast<object>(); }
		}

		/// <summary>	
		/// 	새로운 객체를 생성합니다.
		/// </summary>
		/// <returns>생성된 새로운 개체를 반환합니다.</returns>
		public override object CreateNewInstance()
		{
			throw new NotSupportedException();
		}

		/// <summary>	
		/// 	개체가 특정 객체를 반환할 수 있는지 여부 입니다.
		/// </summary>
		/// <param name="input">객체를 반환할 때 필요한 매개 변수입니다. </param>
		/// <returns>특정 객체를 반환할 수 있으면 True, 그렇지 않으면 False 입니다.</returns>
		public override object Getter(object input)
		{
			if (input == null) throw new ArgumentNullException("input");

			if (this.IsMatches(input.ToString()))
			{
				var value = this.FormDataDictinoary[input.ToString()];
				switch (value)
				{
					case "on": return true;
					case "off": return false;
				}

				return value;
			}

			return null;
		}

		/// <summary>	
		/// 	개체에 값을 설정합니다.
		/// </summary>
		/// <param name="input">객체를 설정할 때 필요한 매개 변수 입니다.</param>
		/// <param name="arg">객체에 설정하는 값입니다.</param>
		public override void Setter(object input, object arg)
		{
			throw new NotSupportedException();
		}

		/// <summary>	
		/// 	객체가 조건에 만족하는지 여부를 반환합니다.
		/// </summary>
		/// <param name="input">조건으로 사용하는 매개 변수 입니다.</param>
		/// <returns>조건에 만족하면 True, 그렇지 않으면 False 를 반환합니다.</returns>
		public override bool IsMatches(string input)
		{
			return this.FormDataDictinoary.ContainsKey(input);
		}

		/// <summary>
		/// 스트림을 다음으로 이동합니다.
		/// </summary>
		public override bool MoveNext() { return false; }

		#endregion
	}
}
