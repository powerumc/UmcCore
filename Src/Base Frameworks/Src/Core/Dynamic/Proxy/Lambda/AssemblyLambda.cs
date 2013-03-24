using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;
using System.Reflection;

namespace Umc.Core.Dynamic.Proxy.Lambda
{
	public class AssemblyLambda : IAssemblyLambda
	{
		public AssemblyBuilder AssemblyBuilder { get; private set; }
		public DynamicProxyToken Token { get; private set; }
		public string Name { get; private set; }
		public string AssemblyLambdaQualifiedName { get; private set; }

		public IModuleLambda Assembly()
		{
			return this.Assembly(null);
		}

		public IModuleLambda Assembly(string assemblyName)
		{
			this.Name = assemblyName;

			this.Token = new DynamicProxyToken();
			return this.Assembly(this.Name ?? this.Token.Token.ToString("N"), this.Token);
		}

		public IModuleLambda Assembly(string assemblyName, DynamicProxyToken token)
		{
			this.AssemblyLambdaQualifiedName = AssemblyLambdaQualifiedNamePolicy.CreateNewFileName(this);
			
			this.Token = token;

			var name = new AssemblyName(assemblyName);
			this.AssemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(name, AssemblyBuilderAccess.RunAndSave);

			return new ModuleLambda(this);
		}

		#region ITypeLambda

		public TypeBuilder TypeBuilder { get { throw new NotImplementedException(); } }
		public TypeAccessor TypeAccessor { get { throw new NotImplementedException(); } }
		public FieldAccessor FieldAccessor { get { throw new NotImplementedException(); } } 
		public MethodAccessor MethodAccessor { get { throw new NotImplementedException(); } }

		public ITypeLambda Attribute(Type attributeType, params object[] param)
		{
			throw new NotImplementedException();
		}
		public ITypeLambda SetParent(string name) 
		{
			throw new NotImplementedException();
		}

		public ITypeLambda AddInterface(string name)
		{
			throw new NotImplementedException();
		}

		public IAssemblyLambda Attribute()
		{
			throw new NotImplementedException();
		}

		public IAssemblyLambda Attribute(params object[] @object)
		{
			throw new NotImplementedException();
		}

		public Operand Field(Type returnType, string name)
		{
			throw new NotImplementedException();
		}

		public IPropertyLambda Property(Type returnType, string name)
		{
			throw new NotImplementedException();
		}

		public ICodeLambda Method(string name)
		{
			throw new NotImplementedException();
		}

		public ICodeLambda Method(Type returnType, string name, params Type[] argumentsTypes)
		{
			throw new NotImplementedException();
		}

		public ICodeLambda Method(Type returnType, string name, Type[] argumentsTypes, MethodInfo parentMethodInfo)
		{
			throw new NotImplementedException();
		}

		public ICodeLambda Constructor()
		{
			throw new NotImplementedException();
		}

		public ICodeLambda Constructor(params Type[] argumentsTypes)
		{
			throw new NotImplementedException();
		}

		public ICodeLambda Constructor(IEnumerable<ParameterCriteriaMetadataInfo> parameterCriteriaMetadataInfos)
		{
			throw new NotImplementedException();
		}

		public ITypeLambda Class(string name)
		{
			var typeLambda = this.Assembly().Module().Class(name);

			return typeLambda;
		}

		public ITypeLambda Class(string name, Type parent, Type[] interfaces)
		{
			var typeLambda = this.Assembly().Module().Class(name, parent, interfaces);

			return typeLambda;
		}

		public ITypeLambda Struct(string name)
		{
			throw new NotImplementedException();
		}

		public ITypeLambda Interface(string name)
		{
			throw new NotImplementedException();
		}

		public IEnumLambda Enum(string name)
		{
			throw new NotImplementedException();
		}

		public IEnumLambda Enum(string name, Type underlyingType)
		{
			throw new NotImplementedException();
		}

		public ITypeLambda Delegate(Type returnType, string name, params Type[] argumentsTypes)
		{
			throw new NotImplementedException();
		}

		public ITypeLambda Event(Type delegateType, string name)
		{
			throw new NotImplementedException();
		} 
		#endregion

		#region IAccessorLambda


		/// <summary>	
		/// 	이 속성은 사용하지 않습니다.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
		[Obsolete("이 속성은 사용하지 않습니다.")]
		public ITypeLambda Public
		{
			get { throw new NotImplementedException(); }
		}

		/// <summary>	
		/// 	이 속성은 사용하지 않습니다.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
		[Obsolete("이 속성은 사용하지 않습니다.")]
		public ITypeLambda Internal
		{
			get { throw new NotImplementedException(); }
		}

		/// <summary>	
		/// 	이 속성은 사용하지 않습니다.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
		[Obsolete("이 속성은 사용하지 않습니다.")]
		public ITypeLambda Protected
		{
			get { throw new NotImplementedException(); }
		}

		/// <summary>	
		/// 	이 속성은 사용하지 않습니다.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
		[Obsolete("이 속성은 사용하지 않습니다.")]
		public ITypeLambda Private
		{
			get { throw new NotImplementedException(); }
		}

		/// <summary>	
		/// 	이 속성은 사용하지 않습니다.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
		[Obsolete("이 속성은 사용하지 않습니다.")]
		public ITypeLambda Static
		{
			get { throw new NotImplementedException(); }
		}

		/// <summary>	
		/// 	이 속성은 사용하지 않습니다.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
		[Obsolete("이 속성은 사용하지 않습니다.")]
		public ITypeLambda ReadOnly
		{
			get { throw new NotImplementedException(); }
		}

		/// <summary>	
		/// 	이 속성은 사용하지 않습니다.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
		[Obsolete("이 속성은 사용하지 않습니다.")]
		public ITypeLambda Abstract
		{
			get { throw new NotImplementedException(); }
		}

		/// <summary>	
		/// 	이 속성은 사용하지 않습니다.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
		[Obsolete("이 속성은 사용하지 않습니다.")]
		public ITypeLambda Sealed
		{
			get { throw new NotImplementedException(); }
		}

		/// <summary>	
		/// 	이 속성은 사용하지 않습니다.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
		[Obsolete("이 속성은 사용하지 않습니다.")]
		public ITypeLambda Override
		{
			get { throw new NotImplementedException(); }
		}

		/// <summary>	
		/// 	이 속성은 사용하지 않습니다.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
		[Obsolete("이 속성은 사용하지 않습니다.")]
		public ITypeLambda Virtual
		{
			get { throw new NotImplementedException(); }
		} 
		#endregion

		#region IReleaseType
		public Type ReleaseType() { throw new NotSupportedException("ReleaseType"); } 
		#endregion

		#region ISaveable
		public bool CanSave { get { return true; } }

		public void Save()
		{
			this.AssemblyBuilder.Save(this.AssemblyLambdaQualifiedName);
		} 
		#endregion
	}
}
