//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Reflection;
//using Umc.Core.IoC.Dependency;

//namespace Umc.Core.IoC
//{
//    /// <summary>
//    /// <para>IoC 컨테이너의 종속성을 만족하는 여부에 따른 결과를 처리하는 클래스입니다.</para>
//    /// </summary>
//    public static class DependencyThen
//    {
//        public static void MappingDependencyContractOfType(this ContractMap map, Type typeOfDependency, DependencyContractAttribute attribute)
//        {
//            ContractMapValue cmv = new ContractMapValue(attribute.TypeOfContract, attribute.LifetimeFlag);
//            var dm = map.Pickup(cmv);
//            DependencyMapValueOfType dmv = new DependencyMapValueOfType(typeOfDependency, attribute.Key, attribute.LifetimeFlag, cmv);
//            dm.Add(dmv);
//        }

//        public static void MappingDependencyInjectionOnProperty(this ContractMap map, PropertyInfo property, DependencyInjectionAttribute attribute)
//        {
//            var typeOfDependency = property.DeclaringType;
//            var typeOfContract = property.DeclaringType.GetDependencyContractsOfType();

//            foreach (var contract in typeOfContract)
//            {
//                ContractMapValue cmv = new ContractMapValue(contract.TypeOfContract);
//                var dm = map.Pickup(cmv);

//                dm.Single( o => o.TypeOfDependency == typeOfDependency).DependencyInjectionValues.Add(new DependencyInjectionValueOnProperty(property, attribute));
//            }
//        }

//        public static void MappingDependencyInjectionOnConstructor(this ContractMap map, ConstructorInfo constructor, DependencyInjectionAttribute attribute)
//        {
//            var typeOfDependency = constructor.DeclaringType;
//            var typeOfContract = constructor.DeclaringType.GetDependencyContractsOfType();

//            foreach (var contract in typeOfContract)
//            {
//                ContractMapValue cmv = new ContractMapValue(contract.TypeOfContract);
//                var dm = map.Pickup(cmv);

//                dm.Single( o => o.TypeOfDependency == typeOfDependency).DependencyInjectionValues.Add(new DependencyInjectionValueOnConstructor(constructor, attribute));
//            }
//        }

//        public static void MappingDependencyInjectionOnMethodParameter(this ContractMap map, ParameterInfo parameter, DependencyInjectionAttribute attribute)
//        {
//            var typeOfDependency = ((MethodInfo)parameter.Member).DeclaringType;
//            var typeOfContract = typeOfDependency.GetDependencyContractsOfType();

//            foreach (var contract in typeOfContract)
//            {
//                ContractMapValue cmv = new ContractMapValue(contract.TypeOfContract);
//                var dm = map.Pickup(cmv);

//                dm.Single( o => o.TypeOfDependency == typeOfDependency).DependencyInjectionValues.Add(new DependencyInjectionValueOnMethodParameter(parameter, attribute));
//            }
//        }
//    }
//}