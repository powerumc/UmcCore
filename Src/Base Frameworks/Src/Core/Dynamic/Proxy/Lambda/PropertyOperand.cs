using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;

namespace Umc.Core.Dynamic.Proxy.Lambda
{
	public class PropertyOperand : Operand,
									IValuable<PropertyBuilder>
	{
		private ITypeLambda typeLambda;
		private ICriteriaMetadataInfo criteriaMetadataInfo;
		private PropertyBuilder propertyBuilder;

		public PropertyOperand(ITypeLambda typeLambda, ILGenerator ilGenerator, ICriteriaMetadataInfo criteriaMetadataInfo)
			: base(typeLambda, ilGenerator)
		{
			this.typeLambda           = typeLambda;
			this.criteriaMetadataInfo = criteriaMetadataInfo;
		}

		public override void WriteEmit(ICodeLambda codeLambda)
		{
			base.WriteEmit(codeLambda);
		}

		public override void WriteEmit(ICodeLambda codeLambda, Operand operand)
		{
			base.WriteEmit(codeLambda, operand);
		}

		#region IValuable<PropertyBuilder> 멤버

		public PropertyBuilder Value
		{
			get { return this.propertyBuilder; }
		}

		#endregion
	}
}
