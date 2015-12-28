using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.UI;

namespace Umc.Core.Web
{
	/// <summary> <see cref="WebForm{TModel}"/> 객체의 확장 메서드 모음 클래스 입니다. </summary>
	public static class WebFormExtensions
	{
		#region Input Text
		/// <summary> <see cref="WebForm{TModel}"/> 객체를 이용하여 HTML input type='text' 문자열을 생성합니다. 
		/// 	<example><code><![CDATA[
		///		var WebForm = new WebForm<Person>(model);
		///		WebForm.InputTextFor(o => o.Name, new { id="txtName" });
		///		]]></code></example>
		/// </summary>
		/// <remarks> Umc, 11/17/2015. </remarks>
		/// <typeparam name="T">	Generic type parameter. </typeparam>
		/// <param name="form">				<see cref="WebForm{TModel}"/> 객체 입니다. </param>
		/// <param name="value">			입력상자의 value로 사용하는 속성입니다. </param>
		/// <param name="htmlAttribute">	HTML 태그에 추가할 속성 객체 입니다. </param>
		/// <returns> 정형화된 HTML 을 반환합니다. </returns>
		public static WebFormHtmlString InputTextFor<T>(this WebForm<T> form, Expression<Func<T, object>> value, object htmlAttribute = null)
		{
			return Tag(form, "input", form.Name(value), new { type = "text", value = value.Compile()(form.Model) }, htmlAttribute);
		}

		/// <summary> <see cref="WebForm{TModel}"/> 객체를 이용하여 HTML input type='text' 문자열을 생성합니다. 
		///		<example><code><![CDATA[
		///		var WebForm = new WebForm<Person>(model);
		///		WebForm.InputTextFor(o => o.Name, "hidden", new { id="txtName" });
		///		]]></code></example>
		/// </summary>
		/// <typeparam name="T">	데이터로 사용하는 모델 객체입니다. </typeparam>
		/// <param name="form">				<see cref="WebForm{TModel}"/> 객체 입니다. </param>
		/// <param name="type">				input 태그의 타입입니다. </param>
		/// <param name="value">			input 태그의 value로 사용하는 속성입니다. </param>
		/// <param name="htmlAttribute">	HTML 태그에 추가할 속성 객체 입니다. </param>
		/// <returns> 정형화된 HTML 을 반환합니다. </returns>
		public static WebFormHtmlString InputTextFor<T>(this WebForm<T> form, string type, Expression<Func<T, object>> value, object htmlAttribute = null)
		{
			return Tag(form, "input", form.Name(value), new { type, value = value.Compile()(form.Model) }, htmlAttribute);
		}
		#endregion

		#region Input Hidden
		/// <summary> <see cref="WebForm{TModel}"/> 객체를 이용하여 HTML input type='hidden' 문자열을 생성합니다. 
		/// 	<example><code><![CDATA[
		///		var WebForm = new WebForm<Person>(model);
		///		WebForm.InputHiddenFor(o => o.Name, new { id="txtName" });
		///		]]></code></example>
		/// </summary>
		/// <typeparam name="T">	데이터로 사용하는 모델 객체입니다. </typeparam>
		/// <param name="form">				<see cref="WebForm{TModel}"/> 객체 입니다. </param>
		/// <param name="value">			숨겨진 입력상자의 value로 사용하는 속성입니다. </param>
		/// <param name="htmlAttribute">	HTML 태그에 추가할 속성 객체 입니다. </param>
		/// <returns> 정형화된 HTML 을 반환합니다. </returns>
		public static WebFormHtmlString InputHiddenFor<T>(this WebForm<T> form, Expression<Func<T, object>> value, object htmlAttribute = null)
		{
			return Tag(form, "input", form.Name(value), new { type = "hidden", value = value.Compile()(form.Model) }, htmlAttribute);
		}

		/// <summary> <see cref="WebForm{TModel}"/> 객체를 이용하여 HTML input type='hidden' 문자열을 생성합니다.
		/// 	<example><code><![CDATA[
		///		var WebForm = new WebForm<Person>(model);
		///		WebForm.InputHiddenFor(o => o.Name, new { id="txtName" });
		///		]]></code></example>
		/// </summary>
		/// <typeparam name="T">	데이터로 사용하는 모델 객체입니다. </typeparam>
		/// <param name="form">				<see cref="WebForm{TModel}"/> 객체 입니다. </param>
		/// <param name="name">				HTML 태그의 name" 속성입니다. </param>
		/// <param name="value">			input의 value로 사용하는 속성입니다. </param>
		/// <param name="htmlAttribute">	HTML 태그에 추가할 속성 객체 입니다. </param>
		/// <returns> 정형화된 HTML 을 반환합니다. </returns>
		public static WebFormHtmlString InputHiddenFor<T>(this WebForm<T> form, Expression<Func<T, object>> name, string value, object htmlAttribute = null)
		{
			return Tag(form, "input", form.Name(name), new { type = "hidden", value }, htmlAttribute);
		} 
		#endregion

		#region Input Submit
		/// <summary> <see cref="WebForm{TModel}"/> 객체를 이용하여 HTML의 input type='submit' 태그를 생성합니다.
		/// 	<example><code><![CDATA[
		///		var WebForm = new WebForm<Person>(model);
		///		WebForm.InputSubmit(new { id="form1" });
		///		]]></code></example>
		/// </summary>
		/// <typeparam name="T">	Generic type parameter. </typeparam>
		/// <param name="form">				<see cref="WebForm{TModel}"/> 객체 입니다. </param>
		/// <param name="htmlAttribute">	HTML 태그에 추가할 속성 객체 입니다. </param>
		/// <returns> 정형화된 HTML 을 반환합니다. </returns>
		public static WebFormHtmlString InputSubmit<T>(this WebForm<T> form, object htmlAttribute = null)
		{
			return InputSubmit(form, null, htmlAttribute);
		}
		///
		/// <summary> <see cref="WebForm{TModel}"/> 객체를 이용하여 HTML의 input type='submit' 태그를 생성합니다. 
		///		<example><code><![CDATA[
		///		var WebForm = new WebForm<Person>(model);
		///		WebForm.InputSubmit("Submit Button", new { id="form1" });
		///		]]></code></example>
		/// </summary>
		/// <typeparam name="T">	데이터로 사용하는 모델 객체입니다. </typeparam>
		/// <param name="form">				<see cref="WebForm{TModel}"/> 객체 입니다. </param>
		/// <param name="value">			input의 value로 사용하는 속성입니다. </param>
		/// <param name="htmlAttribute">	HTML 태그에 추가할 속성 객체 입니다. </param>
		/// <returns> 정형화된 HTML 을 반환합니다. </returns>
		public static WebFormHtmlString InputSubmit<T>(this WebForm<T> form, string value, object htmlAttribute = null)
		{
			return InputSubmit(form, null, value, htmlAttribute);
		}

		/// <summary> <see cref="WebForm{TModel}"/> 객체를 이용하여 HTML의 input type='submit' 태그를 생성합니다.
		///		<example><code><![CDATA[
		///		var WebForm = new WebForm<Person>(model);
		///		WebForm.InputSubmit("form1", "Submit Button", new { id="form1" });
		///		]]></code></example>
		/// </summary>
		/// <typeparam name="T">	데이터로 사용하는 모델 객체입니다. </typeparam>
		/// <param name="form">				<see cref="WebForm{TModel}"/> 객체 입니다. </param>
		/// <param name="name">				HTML 태그의 name" 속성입니다. </param>
		/// <param name="value">			input의 value로 사용하는 속성입니다. </param>
		/// <param name="htmlAttribute">	HTML 태그에 추가할 속성 객체 입니다. </param>
		/// <returns> 정형화된 HTML 을 반환합니다. </returns>
		public static WebFormHtmlString InputSubmit<T>(this WebForm<T> form, string name, string value, object htmlAttribute = null)
		{
			return Tag(form, "input", name, new { type = "submit", value}, htmlAttribute);
		} 
		#endregion

		#region Input Image
		/// <summary>  <see cref="WebForm{TModel}"/> 객체를 이용하여 img 태그를 생성합니다. 
		///		<example><code><![CDATA[
		///		var WebForm = new WebForm<Person>(model);
		///		WebForm.InputImage(new { src="http://Umc.Core/image1.jpg" });
		///		]]></code></example>
		/// </summary>
		/// <typeparam name="T">	데이터로 사용하는 모델 객체입니다. </typeparam>
		/// <param name="form">				<see cref="WebForm{TModel}"/> 객체 입니다. </param>
		/// <param name="htmlAttribute">	HTML 태그에 추가할 속성 객체 입니다. </param>
		/// <returns> 정형화된 HTML 을 반환합니다. </returns>
		public static WebFormHtmlString InputImage<T>(this WebForm<T> form, object htmlAttribute = null)
		{
			return InputImage(form, null, htmlAttribute);
		}

		/// <summary> <see cref="WebForm{TModel}"/> 객체를 이용하여 img 태그를 생성합니다. 
		///		<example><code><![CDATA[
		///		var WebForm = new WebForm<Person>(model);
		///		WebForm.InputImage("http://Umc.Core/image1.jpg", new { @class="inputimage" });
		///		]]></code></example>
		/// </summary>
		/// <typeparam name="T">	데이터로 사용하는 모델 객체입니다. </typeparam>
		/// <param name="form">				<see cref="WebForm{TModel}"/> 객체 입니다. </param>
		/// <param name="src">				img 태그의 src 속성입니다. </param>
		/// <param name="htmlAttribute">	HTML 태그에 추가할 속성 객체 입니다. </param>
		/// <returns> 정형화된 HTML 을 반환합니다. </returns>
		public static WebFormHtmlString InputImage<T>(this WebForm<T> form, string src, object htmlAttribute = null)
		{
			return InputImage(form, null, src, htmlAttribute);
		}

		/// <summary> <see cref="WebForm{TModel}"/> 객체를 이용하여 img 태그를 생성합니다. 
		///		<example><code><![CDATA[
		///		var WebForm = new WebForm<Person>(model);
		///		WebForm.InputImage("img1", "http://Umc.Core/image1.jpg", new { @class="inputimage" });
		///		]]></code></example>
		/// </summary>
		/// <typeparam name="T">	데이터로 사용하는 모델 객체입니다. </typeparam>
		/// <param name="form">				<see cref="WebForm{TModel}"/> 객체 입니다. </param>
		/// <param name="name">				HTML 태그의 name" 속성입니다. </param>
		/// <param name="src">				img 태그의 src 속성입니다. </param>
		/// <param name="htmlAttribute">	HTML 태그에 추가할 속성 객체 입니다. </param>
		/// <returns> 정형화된 HTML 을 반환합니다. </returns>
		public static WebFormHtmlString InputImage<T>(this WebForm<T> form, string name, string src, object htmlAttribute = null)
		{
			return Tag(form, "input", name, new { type = "image", src }, htmlAttribute);
		} 
		#endregion

		#region Input CheckBox
		/// <summary> <see cref="WebForm{TModel}"/> 객체를 이용하여 HTML 체크박스를 생성합니다. 
		///		<example><code><![CDATA[
		///		var WebForm = new WebForm<Person>(model);
		///		WebForm.InputCheckBoxFor(o => o.IsSuccess, new { id = "checkbox1" });
		///		]]></code></example>
		/// </summary>
		/// <typeparam name="T">	데이터로 사용하는 모델 객체입니다. </typeparam>
		/// <param name="form">				<see cref="WebForm{TModel}"/> 객체 입니다. </param>
		/// <param name="value">			체크박스의 name과 value로 사용하는 속성입니다. </param>
		/// <param name="htmlAttribute">	HTML 태그에 추가할 속성 객체 입니다. </param>
		/// <returns> 정형화된 HTML 을 반환합니다. </returns>
		public static WebFormHtmlString InputCheckBoxFor<T>(this WebForm<T> form, Expression<Func<T, object>> value, object htmlAttribute = null)
		{
			var @checked = value.Compile()(form.Model);
			if (@checked != null && (@checked.ToString().ToLower() == "true" || @checked.ToString() == "1"))
				return Tag(form, "input", form.Name(value), new { type = "checkbox" }, htmlAttribute, new {@checked="checked"});

			return Tag(form, "input", form.Name(value), new { type = "checkbox" }, htmlAttribute);
		} 

		#endregion

		/// <summary> <see cref="WebForm{TModel}"/> 객체를 이용하여 HTML 태그 문자열을 생성합니다. 
		/// 	<example><code><![CDATA[
		///		var WebForm = new WebForm<Person>(model);
		///		WebForm.Tag("input", "txtName", o => o.Name, new { id = "checkbox1" });
		///		]]></code></example>
		/// </summary>
		/// <typeparam name="T">	데이터로 사용하는 모델 객체입니다. </typeparam>
		/// <param name="form">			 	<see cref="WebForm{TModel}"/> 객체 입니다. </param>
		/// <param name="tag">			 	HTML 태그 이름 입니다. </param>
		/// <param name="name">			 	HTML 태그의 name" 속성입니다. </param>
		/// <param name="htmlAttributes">	HTML 태그에 추가할 속성 객체 입니다. </param>
		/// <returns> 정형화된 HTML 을 반환합니다. </returns>
		public static WebFormHtmlString Tag<T>(this WebForm<T> form, string tag, string name, params object[] htmlAttributes)
		{
			var sw = new StringWriter();
			var html = new HtmlTextWriter(sw);

			html.WriteBeginTag(tag);
			{
				if (name.IsNotNullOrEmpty()) html.WriteAttribute("name", name);
			}
			html.Write(" ");
			foreach (var attr in htmlAttributes.Where(attr => htmlAttributes != null)) {
				html.Write(GetKeyValueString(attr));
			}
			html.Write(HtmlTextWriter.TagRightChar);
			html.WriteEndTag(tag);
			html.Close();
			
			return sw.ToString();
		}

		internal static string GetKeyValueString(object obj)
		{
			if (obj == null) return "";

			var sb = new StringBuilder();
			var properties = obj.GetType().GetProperties();
			var index = 0;
			foreach (var attr in properties)
			{
				if (++index != properties.Length - 1) sb.Append(" ");

				sb.AppendFormat("{0}=\"{1}\"", attr.Name, attr.GetValue(obj, null));
			}

			return sb.ToString();
		}
	}
}
