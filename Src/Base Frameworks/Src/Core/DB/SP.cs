using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Umc.Core.Data;
using Umc.Core.Dynamic;
using Umc.Core.IoC;
using Umc.Core.Logger;
using Umc.Core.Mapping;

namespace Umc.Core.Games.DB
{
	/// <summary>
	/// <para>
	///		SP 에서 사용하는 데이터 모델 인터페이스를 관리하는 클래스 입니다.		
	/// </para>
	/// </summary>
	/// <example><code><![CDATA[
	///		var model = SP.Resolve<IPersonModel>();
	///		model.Name = "Nexon";
	///		model.Age = 36;
	/// ]]></code></example>
	public abstract class SP
	{
		protected SqlCommand SqlCommand { get; set; }
		static readonly object _lock = new object();

		/// <summary>
		/// SP 에서 사용하는 인터페이스를 관리하는 컨테이너입니다.
		/// </summary>
		protected static IFrameworkContainer frameworkContainer = new FrameworkContainerForUnity();

		/// <summary>	SP 에서 사용하는 인터페이스의 객체를 생성하여 반환합니다.	
		/// 			<example><code><![CDATA[
		///				var model = SP.Resolve<IPersonModel>();
		///				model.Name = "POWERUMC";
		///				model.Age = 36;
		///				]]></code></example>
		/// 			</summary>
		/// <typeparam name="T">	인터페이스 타입입니다.. </typeparam>
		public static T Resolve<T>() where T : class
		{
			return GetOrRegister<T>(new[] { typeof(T) });
		}

		/// <summary>	<see cref="SP.HasSpResult"/> 를 false 로 설정하여 SP 클래스의 객체를 생성합니다.	</summary>
		protected SP()
		{
		}

		protected static TInput GetOrRegister<TInput>(IEnumerable<Type> interfaceTypes)
		{
			lock (_lock)
			{
				if (frameworkContainer.IsRegisted(typeof (TInput))) return frameworkContainer.Resolve<TInput>();

				Type type;
				if (typeof(TInput).IsInterface)
				{
					type = DynamicObject.InterfaceImplementationType(interfaceTypes.ToArray());
					frameworkContainer.RegisterType(typeof(TInput), type, LifetimeFlag.PerCall);
				}
				else
				{
					type = DynamicObject.InterfaceImplementationType(new[] { typeof(ISPResult) }, typeof(TInput), null);
					frameworkContainer.RegisterType(typeof(TInput), type, LifetimeFlag.PerCall);
				}

				if (typeof(TInput) != type)
				{
					RegistorTypeDescriptor(type, typeof(TInput));
				}

				return frameworkContainer.Resolve<TInput>();
			}
		}

		protected static void RegistorTypeDescriptor(Type classType, Type metadataType)
		{
			TypeDescriptor.AddProvider(new AssociatedMetadataTypeTypeDescriptionProvider(classType, metadataType), classType);

			foreach (var interaceType in metadataType.GetInterfaces())
			{
				TypeDescriptor.AddProvider(new AssociatedMetadataTypeTypeDescriptionProvider(classType, interaceType), classType);
			}

		}
	}

	///  <summary>
	///  <para>SP 호출을 합니다.</para>
	///  
	///  <para>
	///  다음은 가장 간단한 방법으로 SP를 호출하는 방법을 보여줍니다.
	///  
	/// 		<example><code><![CDATA[
	/// 		var sp = new SP<ISP, IModelInterface>(new KartConnectionStringProvider().GetConnectionString());
	/// 		var result = sp.ExecuteList();
	///			]]></code></example>
	///  </para>
	///  
	///  <para>
	///  다음은 SqlParameter에 매개변수를 전달하는 방법을 보여줍니다.
	/// 		<example><code><![CDATA[
	/// 		var sp = new SP<ISP, IModelInterface>("ktp_Event_LeagueEvolution_MatchPrediction_GetInfo",new KartConnectionStringProvider().GetConnectionString());
	/// 		sp.AddParameter(_ => _.n8NexonSN, 123);		// cmd.Parameters.AddWithValue("@n8NexonSN", 123); 코드와 동일
	/// 		var result = sp.ExecuteList();
	/// 		]]></code></example>
	///  </para>
	///  
	///  </summary>
	///  <typeparam name="TSP">SP 에 매개변수를 전달할 인터페이스 타입 입니다.</typeparam>
	///  <typeparam name="TModel">SP 호출한 데이터를 받을 모델 클래스 타입 입니다.</typeparam>
	/// 
	public class SP<TSP, TModel> : SP where TSP : class
								      where TModel : class
	{
		private static readonly IFrameworkLogger logger = FrameworkLogger.GetLogger(typeof (SP<TSP, TModel>));
		
		private SPResultList<TModel> result;
		private static TypeMap typeMap = new TypeMap();
		protected StringBuilder sbDebug = new StringBuilder(1000);

		private readonly List<SPEntry> entryList = new List<SPEntry>();
		private readonly TSP input;


		/// <summary> 실행할 SP 이름을 가져오거나 설정합니다.. </summary>
		public string SpName { get; set; }

		/// <summary>	SP를 실행할 데이터베이스 연결 문자열을 가져오거나 설정합니다.	</summary>
		public string ConnectionString { get; set; }

		/// <summary>	게임의 서비스 코드를 가져오거나 설정합니다.	</summary>
		public ServiceCode ServiceCode { get; set; }

		/// <summary>	
		///		SP 의 파라메터에 다음의 파라메터 이름이 있으면 true, 그렇지 않으면 false 를 설정합니다.
		///		
		///		<list type="bullet">
		///			<item><description>@frk_n4ErrorCode</description></item>
		///			<item><description>@frk_strErrorText</description></item>
		///			<item><description>@frk_isRequiresNewTransaction</description></item>
		///		</list>
		/// </summary>
		public bool HasSpResult { protected get; set; }

		/// <summary>	SP 호출하기 위한 클래스 객체를 생성합니다. </summary>
		public SP()
		{
			this.HasSpResult = true;
		}

		/// <summary> SP 호출하기 위한 클래스 객체를 생성합니다. </summary>
		/// <param name="connectionString">	데이터베이스 연결 문자열입니다. </param>
		public SP(string connectionString) : this((TSP)null, connectionString) { }

		/// <summary> SP 호출하기 위한 클래스 객체를 생성합니다. </summary>
		/// <param name="spName">				   	SP 이름입니다. </param>
		/// <param name="connectionStringProvider">	데이터베이스 연결 문자열을 구현하는 인터페이스 입니다. </param>
		public SP(string spName, ISQLConnectionStringProvider connectionStringProvider) : this((TSP)null, connectionStringProvider) { }

		/// <summary> SP 호출하기 위한 클래스 객체를 생성합니다. </summary>
		/// <param name="spName">		   	SP 이름입니다. </param>
		/// <param name="connectionString">	데이터베이스 연결 문자열입니다. </param>
		public SP(string spName, string connectionString) : this(null, spName, connectionString) { }

		/// <summary> SP 호출하기 위한 클래스 객체를 생성합니다. </summary>
		/// <param name="input">		   	SP 매개변수에 전달할 인터페이스 객체입니다. </param>
		/// <param name="connectionString">	데이터베이스 연결 문자열입니다. </param>
		public SP(TSP input, string connectionString) : this()
		{
			this.input = input;
			this.ConnectionString = connectionString;
			this.SpName = GetSpName();
		}

		/// <summary>  SP 호출하기 위한 클래스 객체를 생성합니다. </summary>
		/// <param name="input">				   	SP 매개변수에 전달할 인터페이스 객체입니다. </param>
		/// <param name="connectionStringProvider">	데이터베이스 연결 문자열을 구현하는 인터페이스 입니다. </param>
		public SP(TSP input, ISQLConnectionStringProvider connectionStringProvider) : this(input, connectionStringProvider.GetConnectionString()) { }

		/// <summary> SP 호출하기 위한 클래스 객체를 생성합니다. </summary>
		/// <param name="input">		   	SP 매개변수에 전달할 인터페이스 객체입니다. </param>
		/// <param name="spName">		   	SP 이름입니다. </param>
		/// <param name="connectionString">	데이터베이스 연결 문자열입니다. </param>
		public SP(TSP input, string spName, string connectionString)
		{
			this.input = input;
			this.SpName = spName;
			this.ConnectionString = connectionString;
		}

		/// <summary> SP 호출하기 위한 클래스 객체를 생성합니다. </summary>
		/// <param name="input">				   	SP 매개변수에 전달할 인터페이스 객체입니다. </param>
		/// <param name="spName">				   	SP 이름입니다. </param>
		/// <param name="connectionStringProvider">	데이터베이스 연결 문자열을 구현하는 인터페이스 입니다. </param>
		public SP(TSP input, string spName, ISQLConnectionStringProvider connectionStringProvider) : this(input, spName, connectionStringProvider.GetConnectionString()) { }

		/// <summary> SP 호출하기 위한 클래스 객체를 생성합니다. </summary>
		/// <param name="input">		   	SP 매개변수에 전달할 인터페이스 객체입니다. </param>
		/// <param name="spName">		   	SP 이름입니다. </param>
		/// <param name="connectionString">	데이터베이스 연결 문자열입니다. </param>
		/// <param name="serviceCode">	   	게임의 서비스 코드입니다. </param>
		public SP(TSP input, string spName, string connectionString, ServiceCode serviceCode) : this(input, spName, connectionString)
		{
			this.ServiceCode = serviceCode;
		}

		public virtual IEnumerable<Type> InterfaceTypes
		{
			get
			{
				yield return typeof(TModel); 
			}
		}

		private static string GetSpName()
		{
			var nameAttribute = typeof (TSP).GetCustomAttributeEx<NameAttribute>();
			if (nameAttribute == null)
				throw new NullReferenceException("NameAttribute");

			return nameAttribute.Name;
		}

		public Type GetActivatedObjectType()
		{
			return typeof(TModel);
		}

		/// <summary> SP 호출 결과 데이터의 오류 정보를 반환합니다. </summary>
		/// <value> The sp result. </value>
		public ISPResult SpResult { get { return result as ISPResult;} }

		/// <summary> 쿼리를 실행하고 쿼리에서 반환된 결과 집합의 첫 번째 행의 첫 번째 열을 반환합니다. 다른 모든 열과 행은 무시됩니다. </summary>
		/// <exception cref="ArgumentNullException">	<see cref="SP.SpName"/> 또는 <see cref="SP.ConnectionString"/> 이 null 인 경우 발생하는 예외입니다. </exception>
		/// <returns> SP 호출한 결과 데이터 객체입니다. </returns>
		public object ExecuteScalar()
		{
			try
			{
				if (this.SpName == null) throw new ArgumentNullException("SpName");
				if (this.ConnectionString == null) throw new ArgumentNullException("ConnectionString");

				object executeScalar = null;
				using (var connection = new SqlConnection(this.ConnectionString))
				{
					using (this.SqlCommand = GetSqlCommand(this.SpName, connection, CommandType.StoredProcedure))
					{
						initSp();
						mapSpParam();

						connection.Open();
						executeScalar = this.SqlCommand.ExecuteScalar();
						connection.Close();
					}
				}

				return executeScalar;
			}
			finally
			{
				logger.Info(sbDebug.ToString());
			}
		}

		/// <summary> 연결 개체에 대해 SQL 문을 실행합니다. </summary>
		/// <exception cref="ArgumentNullException">	<see cref="SP.SpName"/> 또는 <see cref="SP.ConnectionString"/> 이 null 인 경우 발생하는 예외입니다. </exception>
		/// <returns> SP 호출한 결과 데이터 객체입니다. </returns>
		public TModel Execute()
		{
			try
			{
				if (this.SpName == null) throw new ArgumentNullException("SpName");
				if (this.ConnectionString == null) throw new ArgumentNullException("ConnectionString");

				using (var connection = new SqlConnection(this.ConnectionString))
				{
					using (this.SqlCommand = GetSqlCommand(this.SpName, connection, CommandType.StoredProcedure))
					{
						initSp();
						mapSpParam();

						TModel model = null;
						connection.Open();
						sbDebug.AppendLine("SQL Execute: " + this.SpName);
						var reader = this.SqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
						while (reader.Read())
						{
							model = mapValues(reader, new[] { typeof(TModel), typeof(ISPResult) });
						}

						reader.Close();
						connection.Close();

						mapOutput(this.SqlCommand, this.input);
						return model;
					}
				}
			}
			finally
			{
				logger.Info(sbDebug.ToString());
			}
		}

		/// <summary> 연결 개체에 대해 SQL 문을 실행한 <see cref="List{T}"/> 를 반환합니다.</summary>
		/// <exception cref="ArgumentNullException">	<see cref="SP.SpName"/> 또는 <see cref="SP.ConnectionString"/> 이 null 인 경우 발생하는 예외입니다. </exception>
		/// <returns> SP 호출한 결과 데이터 객체입니다. </returns>
		public SPResultList<TModel> ExecuteList()
		{
			try
			{
				if (this.SpName == null) throw new ArgumentNullException("SpName");
				if (this.ConnectionString == null) throw new ArgumentNullException("ConnectionString");

				using (var connection = new SqlConnection(this.ConnectionString))
				{
					using (this.SqlCommand = GetSqlCommand(this.SpName, connection, CommandType.StoredProcedure))
					{
						initSp();
						mapSpParam();

						result = GetOrRegister<SPResultList<TModel>>(this.InterfaceTypes);
						connection.Open();
						var reader = this.SqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
						do
						{
							while (reader.Read())
							{
								var value = mapValues(reader, this.InterfaceTypes);
								result.Add(value);
							}

						} while (reader.NextResult());

						reader.Close();
						connection.Close();

						return result;
					}
				}
			}
			finally
			{
				logger.Info(sbDebug.ToString());
			}
		}

		private void initSp()
		{
			if (!this.HasSpResult) return;

			this.SqlCommand.Parameters.Add("@frk_n4ErrorCode", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
			this.SqlCommand.Parameters.Add("@frk_strErrorText", SqlDbType.NVarChar, 100).Direction = ParameterDirection.Output;
			this.SqlCommand.Parameters.Add("@frk_isRequiresNewTransaction", SqlDbType.TinyInt, 1);
			this.SqlCommand.Parameters["@frk_isRequiresNewTransaction"].Value = 1;

			#region Logging

			sbDebug.AppendLine("initSp");
			sbDebug.AppendLine("@frk_n4ErrorCode");
			sbDebug.AppendLine("@frk_strErrorText");
			sbDebug.AppendLine("@frk_isRequiresNewTransaction");

			#endregion
		}

		private void mapSpParam()
		{
			var dicProp = typeof (TSP).GetProperties().ToDictionary(o => o.Name, StringComparer.OrdinalIgnoreCase);

			sbDebug.AppendLine("mapSpParam");
			foreach (var entry in entryList)
			{
				var name = entry.Expression.Member.Name;
				var paramName = "@" + entry.Expression.Member.Name;
				var paramType = ((PropertyInfo) entry.Expression.Member).PropertyType;
				var parameter = new SqlParameter() {
					                                   ParameterName = paramName,
					                                   SqlDbType = typeMap.GetMappingValue(paramType)
				                                   };

				if (entry.Size != null)
				{
					parameter.Size = entry.Size.GetValueOrDefault();
				}

				if (dicProp.Keys.Any(o => o == name))
				{
					var prop = dicProp[name];
					var inputAttribute = prop.GetCustomAttributes(typeof (InputAttribute), true);
					var outputAttributes = prop.GetCustomAttributes(typeof (OutputAttribute), true);

					if (inputAttribute.Length > 0 && outputAttributes.Length > 0) parameter.Direction = ParameterDirection.InputOutput;
					else if (inputAttribute.Length > 0 && outputAttributes.Length == 0) parameter.Direction = ParameterDirection.Input;
					else if (inputAttribute.Length == 0	&& outputAttributes.Length > 0) parameter.Direction = ParameterDirection.Output;
				}

				if (entry.HasDefaultValue)
					parameter.Value = entry.DefaultValue ?? DBNull.Value;

				this.SqlCommand.Parameters.Add(parameter);

				#region Logging

				sbDebug.AppendLine(string.Format("{0}	{1} = {2}", paramName, typeMap.GetMappingValue(paramType), entry.HasDefaultValue ? entry.DefaultValue ?? "NULL(hasDefaultValue)": parameter.Value ?? "NULL"));

				#endregion
			}

			sbDebug.AppendLine(sbDebug.ToString());
			//Umc.Core.Log.ErrorLog.CreateErrorLog(ServiceCode.kartweb, 100, "powerumc", "mapSpParam", sbDebug.ToString());
		}

		private TModel mapValues(IDataRecord reader, IEnumerable<Type> interfaceTypes)
		{
			var data = GetOrRegister<TModel>(interfaceTypes);

			var sbDebug = new StringBuilder(1000);
			sbDebug.AppendLine("mapValues");
			var mapping = new MappingProviderForProperty(data);
			for (var i = 0; i < reader.FieldCount; i++)
			{
				var fieldName = reader.GetName(i);
				var fieldValue = reader.GetValue(i);

				sbDebug.Append(fieldName);

				if (mapping.IsMatches(fieldName))
				{
					mapping.Setter(fieldName, fieldValue);
					sbDebug.Append("\t = " + fieldValue);
				}

				sbDebug.AppendLine();
			}

			sbDebug.AppendLine(sbDebug.ToString());

			return data;
		}

		private void mapOutput(SqlCommand cmd, TSP data)
		{
			if (data == null) return;

			var mapping = new MappingProviderForProperty(data);
			var names = getOutputParameterDirection(cmd.Parameters).Union(getOutputAttributes(typeof (TModel)));

			var sbDebug = new StringBuilder(1000);
			sbDebug.AppendLine("mapOutput");
			foreach (var name in names)
			{
				var p = cmd.Parameters["@" + name];
				sbDebug.AppendLine(name + " OUTPUT " + p.Value);

				mapping.Setter(name, p.Value);
			}

			sbDebug.AppendLine(sbDebug.ToString());
		}

		private SqlCommand GetSqlCommand(string commandText, SqlConnection connection, CommandType commandType)
		{
			this.SqlCommand = new SqlCommand(commandText, connection) {CommandType = commandType};
			return this.SqlCommand;
		}

		/// <summary> SP 호출 시 매개변수를 추가합니다. </summary>
		/// <param name="expression">	객체에서 전달할 속성입니다. </param>
		/// <returns> A SP&lt;TSP,TModel&gt; </returns>
		public SP<TSP, TModel> AddParameter(Expression<Func<TSP, object>> expression)
		{
			return AddParameter(expression, null);
		}

		/// <summary> SP 호출 시 매개변수를 추가합니다. </summary>
		/// <param name="expression">	객체에서 전달할 속성입니다. </param>
		/// <param name="value">	 	기본값으로 설정합니다. </param>
		/// <returns> A SP&lt;TSP,TModel&gt; </returns>
		public SP<TSP, TModel> AddParameter(Expression<Func<TSP, object>> expression, object value)
		{
			return AddParameter(expression, value, null);
		}

		/// <summary> SP 호출 시 매개변수를 추가합니다. </summary>
		/// <remarks> Umc, 11/17/2015. </remarks>
		/// <param name="expression">	객체에서 전달할 속성입니다. </param>
		/// <param name="value">	 	기본값으로 설정합니다. </param>
		/// <param name="size">		 	매개변수의 SQL 컬럼의 크기입니다. </param>
		/// <returns> A SP&lt;TSP,TModel&gt; </returns>
		public SP<TSP, TModel> AddParameter(Expression<Func<TSP, object>> expression, object value, int? size)
		{
			switch (expression.Body.NodeType)
			{
				case ExpressionType.MemberAccess: this.entryList.Add(new SPEntry((MemberExpression)expression.Body, value, size)); break;
				case ExpressionType.Convert:
					var unary = ((UnaryExpression)expression.Body).Operand;
					var member = (MemberExpression)unary;
					this.entryList.Add(new SPEntry(member, value, size));
					break;
			}

			return this;
		}

		private static IEnumerable<string> getOutputAttributes(Type type)
		{
			return from p in type.GetProperties() let attr = p.GetCustomAttributes(typeof(OutputAttribute), true) where attr.Length > 0 select p.Name;
		}

		private static IEnumerable<string> getOutputParameterDirection(SqlParameterCollection collection)
		{
			return from SqlParameter p in collection where p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Output select p.ParameterName.Remove(0,1);
		}

		public class SPEntry
		{
			private object _defaultValue;
			private bool _hasDefaultValue = false;

			public SPEntry() { }
			public SPEntry(MemberExpression expression, object defaultValue) : this(expression, defaultValue, null)
			{
			}

			public SPEntry(MemberExpression expression, object defaultValue, int? size)
			{
				this.Expression = expression;
				this.DefaultValue = defaultValue;
				this.Size = size;
			}

			public MemberExpression Expression { get; set; }
			public int? Size { get; set; }

			public object DefaultValue
			{
				get { return _defaultValue; }
				set
				{
					_defaultValue = value;
					_hasDefaultValue = true;
				}
			}

			public bool HasDefaultValue
			{
				get { return _hasDefaultValue; }
				set { _hasDefaultValue = value; }
			}
		}
	}

	public enum ServiceCode {}

	public class ISQLConnectionStringProvider {
		public string GetConnectionString()
		{
			return string.Empty;
		}
	}

	///  <summary>
	///  <para>SP 호출을 합니다.</para>
	///  
	///  <para>
	///  다음은 가장 간단한 방법으로 SP를 호출하는 방법을 보여줍니다.
	///  
	/// 		<example><code><![CDATA[
	/// 		var sp = new SP<MatchPrediction_GetAvg_Result>("ktp_Event_LeagueEvolution_MatchPrediction_GetAvg", new KartConnectionStringProvider().GetConnectionString());
	/// 		var result = sp.ExecuteList();
	///			]]></code></example>
	///  </para>
	///  
	///  <para>
	///  다음은 SqlParameter에 매개변수를 전달하는 방법을 보여줍니다.
	/// 		<example><code><![CDATA[
	/// 		var sp = new SP<ktp_Event_LeagueEvolution_MatchPrediction_GetInfo_Result>("ktp_Event_LeagueEvolution_MatchPrediction_GetInfo",new KartConnectionStringProvider().GetConnectionString());
	/// 		sp.AddParameter(_ => _.n8NexonSN, 123);		// cmd.Parameters.AddWithValue("@n8NexonSN", 123); 코드와 동일
	/// 		var result = sp.ExecuteList();
	/// 		]]></code></example>
	///  </para>
	///  
	///  </summary>
	///  <typeparam name="TSP">SP 에 매개변수를 전달할 인터페이스 타입 입니다.</typeparam>
	/// <typeparam name="TModel">SP 호출한 데이터를 받을 모델 클래스 타입 입니다.</typeparam>
	/// 
	public class SP<TSP> : SP<TSP, TSP> where TSP : class
	{
		public SP() : base() { }
		public SP(string spName, string connectionString) : this(null, spName, connectionString) { }
		public SP(string connectionString) : this(null, null, connectionString) { }
		public SP(TSP input, string	connectionString) : base(input, connectionString) { }
		public SP(ISQLConnectionStringProvider connectionStringProvider) : base((TSP)null, connectionStringProvider.GetConnectionString()) { }
		public SP(TSP input, ISQLConnectionStringProvider connectionStringProvider) : base(input, connectionStringProvider.GetConnectionString()) { }
		public SP(TSP input, string spName, string connectionString) : base(input, spName, connectionString) { }
		public SP(TSP input, string spName, string connectionString, ServiceCode serviceCode) : base(input, spName, connectionString, serviceCode) { }

		public override IEnumerable<Type> InterfaceTypes
		{
			get
			{
				yield return typeof(TSP);
				yield return typeof (ISPResult);
			}
		}
	}


	public class SPResultList<T> : List<T> where T : class
	{
		public SPResultList()
		{
		}
	}
}
