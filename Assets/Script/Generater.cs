using System;
using System.Collections;
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

	public ArrayList Generate()
    {
		ArrayList arrList = new ArrayList ();

        foreach (Type Td in listEventType)
        {

            MethodInfo mthInfo = Td.GetMethod("PreCondition");
            bool isValid = (bool)mthInfo.Invoke(null, null);
            if (!isValid)
            {
                continue;
            }

			arrList.Add(Activator.CreateInstance(Td));
        }

		return arrList;
    }

    private List<Type> listEventType;
}