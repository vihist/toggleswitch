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

    public Object GenerateEvent()
    {
        for(int i= iStatic; i< listEventType.Count; i++)
        {

            MethodInfo mthInfo = listEventType[i].GetMethod("PreCondition");
            bool isValid = (bool)mthInfo.Invoke(null, null);
            if (!isValid)
            {
                continue;
            }

            iStatic = i+1;
            return Activator.CreateInstance(listEventType[i]);
        }

        iStatic = 0;
        return null;
    }

    private List<Type> listEventType;
    private int iStatic = 0;
}