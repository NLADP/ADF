using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Practices.ObjectBuilder;

namespace Adf.ObjectFactory.ObjectBuilder.BuilderPolicies
{
    public class CreationPolicy : ICreationPolicy
    {
        public ConstructorInfo SelectConstructor(IBuilderContext context, Type type, string id)
        {
        	if(type != null)
        	{
        		ConstructorInfo[] constructors = type.GetConstructors();

        		if (constructors.Length > 0)
        			return constructors[0];
			}
            return null;
        }

        public object[] GetParameters(IBuilderContext context, Type type, string id, ConstructorInfo constructor)
        {
            ParameterInfo[] parms = constructor.GetParameters();
            object[] parmsValueArray = new object[parms.Length];

            for (int i = 0; i < parms.Length; ++i)
            {
                if (typeof(IEnumerable).IsAssignableFrom(parms[i].ParameterType))
                {
                    Type genericList = typeof(List<>);
                    Type fullType = genericList.MakeGenericType(parms[i].ParameterType.GetGenericArguments()[0]);
                    parmsValueArray[i] = Activator.CreateInstance(fullType);

                    foreach (object o in Core.Objects.ObjectFactory.BuildAll(parms[i].ParameterType.GetGenericArguments()[0], true))
                    {
                        ((IList)parmsValueArray[i]).Add(o);
                    }
                }
                else
                {
                    parmsValueArray[i] = Core.Objects.ObjectFactory.BuildUp(parms[i].ParameterType);
                }
            }
            return parmsValueArray;
        }
    }
}
