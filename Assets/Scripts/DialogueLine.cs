using System;
using UnityEngine;

[Serializable]
public class DialogueLine
{
    public string Context;
    public float Delay;
    public string Line;

    public override bool Equals(object other)
    {
        if (null != other && other.GetType() == GetType())
        {
            var temp = (DialogueLine) other;
            if (temp.Context == Context && temp.Line == Line && Mathf.Abs(temp.Delay - Delay) < 0.00001)
                return true;
        }

        return false;
    }
}