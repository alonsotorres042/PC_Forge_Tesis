using UnityEngine;
using System;

public class SocketOpenCondition : ISlotCondition
{
    private readonly Func<bool> isOpenFunc; //Reminder: Make a complete framework based on this guy

    public SocketOpenCondition(Func<bool> isOpenFunc) //Useful and reusable in many projects, Dattebayo!
    {
        this.isOpenFunc = isOpenFunc;
    }

    public bool IsMet(GameObject component)
    {
        return isOpenFunc();
    }
}