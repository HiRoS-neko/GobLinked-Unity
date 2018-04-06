using System;
using UnityEngine;

[Serializable]
public class DialogueLine
{
    public AudioClip Audio;
    public string Context;
    public float Delay = 3;
    public string Line;

    public override bool Equals(object other)
    {
        if (null != other && other.GetType() == GetType())
        {
            var temp = (DialogueLine) other;
            if (temp.Context == Context && temp.Line == Line && Mathf.Abs(temp.Delay - Delay) < 0.00001 &&
                Audio == temp.Audio)
                return true;
        }

        return false;
    }
}