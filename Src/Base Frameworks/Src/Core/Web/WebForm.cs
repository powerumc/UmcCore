using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using Umc.Core.Interfaces;

namespace Umc.Core.Web
{
	/// <summary>
	///		aspx 페이지에서 사용하는 Html Helper 모음 클래스 입니다.
	///		
	///		<para>다음은 속성의 이름을 가져오는 예제입니다.</para>
	///		<example><code><![CDATA[
	///		public class Person
	///		{
	///			public string Name { get; set; }
	///			public int Age { get; set; }
	///		}
	///
	///		var model = new Person() { Name = "umc", Age = 100 };
	///		WebForm.Value<Person>(person, p => p.Name);  // "umc" 출력
	///		]]></code></example>
	/// 
	///		<para>다음은 속성의 이름을 가져오는 예제입니다.</para>
	///		<example><code><![CDATA[
	///		public class Person
	///		{
	///			public string Name { get; set; }
	///			public int Age { get; set; }
	///		}
	///
	///		var model = new Person() { Name = "umc", Age = 100 };
	///		var WebForm = new WebForm<Person>(model);
	///		WebForm.Name(o => o.Name);         // "Name" 출력
	///		WebForm.Value( o => o.Age);        // 100 출력
	///		]]></code></example>
	/// </summary>
	public class WebForm
	{
		/// <summary>
		///		<para>표현식에서 속성의 값을 가져오는 static 함수 입니다. </para>
		///
		/// 	<para>다음은 속성의 이름을 가져오는 예제입니다.</para>
		///		<example><code><![CDATA[
		///		public class Person
		///		{
		///			public string Name { get; set; }
		///			public int Age { get; set; }
		///		}
		///
		///		var model = new Person() { Name = "umc", Age = 100 };
		///		WebForm.Value<Person>(person, p => p.Name);  // "umc" 출력
		///		]]></code></example>
		/// </summary>
		/// <remarks> Umc, 11/17/2015. </remarks>
		/// <typeparam name="T">	Generic type parameter. </typeparam>
		/// <param name="input">	The input. </param>
		/// <param name="func"> 	객체에서 속성의 값을 가져올 람다식입니다. </param>
		/// <returns> An object. </returns>
		public static object Value<T>(T input, Func<T, object> func)
		{
			if (input == null) return "";

			var value = func(input);
			return value ?? "";
		}
	}

	/// <summary> 
	/// 		  aspx 페이지에서 사용하는 Html Helper 모음 클래스 입니다. 
	/// 		  
	/// 	<para>다음은 속성의 이름을 가져오는 예제입니다.</para>
	///		<example><code><![CDATA[
	///		public class Person
	///		{
	///			public string Name { get; set; }
	///			public int Age { get; set; }
	///		}
	///
	///		var model = new Person() { Name = "umc", Age = 100 };
	///		WebForm.Value<Person>(person, p => p.Name);  // "umc" 출력
	///		]]></code></example>
	///
	///		<para>다음은 속성의 이름을 가져오는 예제입니다.</para>
	///		<example><code><![CDATA[
	///		public class Person
	///		{
	///			public string Name { get; set; }
	///			public int Age { get; set; }
	///		}
	///
	///		var model = new Person() { Name = "umc", Age = 100 };
	///		var WebForm = new WebForm<Person>(model);
	///		WebForm.Name(o => o.Name);         // "Name" 출력
	///		WebForm.Value( o => o.Age);        // 100 출력
	///		]]></code></example>
	/// </summary>
	/// <typeparam name="TModel">	데이터로 사용할 모델 타입입니다. </typeparam>
	public class WebForm<TModel>
	{
		/// <summary> 객체의 값을 설정하거나 가져옵니다. </summary>
		/// <value> The model. </value>
		public TModel Model { get; set; }

		/// <summary> aspx 페이지에서 사용하는 Html Helper 모음 클래스를 생성합니다. </summary>
		public WebForm() { }

		/// <summary> aspx 페이지에서 사용하는 Html Helper 모음 클래스를 생성합니다. </summary>
		/// <param name="model">	데이터로 사용할 모델 타입입니다. </param>
		public WebForm(TModel model)
		{
			this.Model = model;
		}

		/// <summary> 
		///		<para>표현식에서 속성의 이름을 가져옵니다. </para>
		/// 
		/// 	<para>다음은 속성의 이름을 가져오는 예제입니다.</para>
		///		<example><code><![CDATA[
		///		public class Person
		///		{
		///			public string Name { get; set; }
		///			public int Age { get; set; }
		///		}
		/// 
		///		var model = new Person() { Name = "umc", Age = 100 };
		///		var WebForm = new WebForm<Person>(model);
		///		WebForm.Name(o => o.Name);         // "Name" 출력
		///		WebForm.Value( o => o.Age);        // 100 출력
		///		]]></code></example>
		/// </summary>
		/// <param name="expression">	이름을 가져올 표현식 입니다. </param>
		/// <returns> 속성 이름입니다. </returns>
		public string Name(Expression<Func<TModel, object>>  expression)
		{
			switch (expression.Body.NodeType)
			{
				case ExpressionType.MemberAccess: return ((MemberExpression)expression.Body).Member.Name;
				case ExpressionType.Convert:
					var unary = ((UnaryExpression)expression.Body).Operand;
					var member = (MemberExpression)unary;
					return member.Member.Name;
				default:
					return "";
			}
		}

		/// <summary>
		///		<para>표현식에서 속성의 값을 가져옵니다. </para>
		///
		/// 	<para>다음은 속성의 이름을 가져오는 예제입니다.</para>
		///		<example><code><![CDATA[
		///		public class Person
		///		{
		///			public string Name { get; set; }
		///			public int Age { get; set; }
		///		}
		///
		///		var model = new Person() { Name = "umc", Age = 100 };
		///		var WebForm = new WebForm<Person>(model);
		///		WebForm.Name(o => o.Name);         // "Name" 출력
		///		WebForm.Value( o => o.Age);        // 100 출력
		///		]]></code></example>
		/// </summary>
		/// <param name="func">객체에서 속성의 값을 가져올 람다식입니다.</param>
		/// <returns>속성의 값</returns>
		public object Value(Func<TModel, object> func)
		{
			var value = func(this.Model);
			return value ?? "";
		}

		///  <summary>
		/// 		<para>표현식에서 속성의 값을 가져옵니다. </para>
		/// 
		///  	<para>다음은 속성의 이름을 가져오는 예제입니다.</para>
		/// 		<example><code><![CDATA[
		/// 		public class Person
		/// 		{
		/// 			public string Name { get; set; }
		/// 			public int Age { get; set; }
		/// 		}
		/// 
		/// 		var model = new Person() { Name = "umc", Age = 100 };
		/// 		var WebForm = new WebForm<Person>(model);
		/// 		WebForm.Name<Person>(o => o.Name);         // "Name" 출력
		/// 		WebForm.Value<Person>( o => o.Age);        // 100 출력
		/// 		]]></code></example>
		///  </summary>
		/// <param name="input">모델 객체입니다.</param>
		/// <param name="func">객체에서 속성의 값을 가져올 람다식입니다.</param>
		///  <returns>속성의 값</returns>
		public object Value<T>(T input, Func<T, object> func)
		{
			if (input == null) throw new ArgumentNullException("input");

			var value = func(input);
			return value ?? "";
		}
	}
}
