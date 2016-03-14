//using System;
//using System.Text;
//using System.Collections.Generic;
//using System.Linq;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.Reflection;
//using Umc.Core.Reflection;

//namespace Umc.Core.Reflection
//{
//	[TestClass]
//	public class AttributeConstants_Tests
//	{
//		[TestCategory("BVT Function"), TestMethod]
//		public void Single_Attribute_Of_Type_Test()
//		{
//			var attribute = AttributeConstants_Accessor.TypeAttribute.Public;
//			Assert.IsTrue((attribute & TypeAttributes.Public) == TypeAttributes.Public);

//		}

//		[TestCategory("BVT Function"), TestMethod]
//		public void Multi_Attribute_Of_Type_Test()
//		{
//			var attribute = AttributeConstants_Accessor.TypeAttribute.Public | AttributeConstants_Accessor.TypeAttribute.Static;

//			Assert.IsTrue((attribute & AttributeConstants_Accessor.TypeAttribute.Public) == AttributeConstants_Accessor.TypeAttribute.Public);
//			Assert.IsTrue((attribute & AttributeConstants_Accessor.TypeAttribute.Static) == AttributeConstants_Accessor.TypeAttribute.Static);
//		}



//		[TestCategory("BVT Function"), TestMethod]
//		public void Single_Attribute_Of_Method_Test()
//		{
//			var attribute = AttributeConstants_Accessor.MethodAttribute.Public;

//			Assert.IsTrue((attribute & MethodAttributes.Public) == MethodAttributes.Public);
//		}

//		[TestCategory("BVT Function"), TestMethod]
//		public void Multi_Attribute_Of_Method_Test()
//		{
//			var attribute = AttributeConstants_Accessor.MethodAttribute.Private | AttributeConstants_Accessor.MethodAttribute.Static;

//			Assert.IsTrue((attribute & AttributeConstants_Accessor.MethodAttribute.Private) == AttributeConstants_Accessor.MethodAttribute.Private);
//			Assert.IsTrue((attribute & AttributeConstants_Accessor.MethodAttribute.Static) == AttributeConstants_Accessor.MethodAttribute.Static);
//		}
//	}
//}
