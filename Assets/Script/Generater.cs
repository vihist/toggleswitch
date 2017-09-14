using System;
using System.Collections.Generic;
using System.Reflection;

public class Generater
{
    public Generater()
    {
        listEventType = new List<Type>();
    }

    public void Register(Type Td)
    {
        listEventType.Add(Td);
    }

    public Object Generate()
    {
        foreach (Type Td in listEventType)
        {

            MethodInfo mthInfo = Td.GetMethod("PreCondition");
            bool isValid = (bool)mthInfo.Invoke(null, null);
            if (!isValid)
            {
                continue;
            }

            return Activator.CreateInstance(Td);
        }

        return default(Type);
    }

    private List<Type> listEventType;
}