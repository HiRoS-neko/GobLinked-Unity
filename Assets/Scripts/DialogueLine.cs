using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public string Context;
    public string Line;
    public float Delay;

    public override bool Equals(object other)
    {
        if (null != other && other.GetType() == this.GetType())
        {
            var temp = (DialogueLine) other;
            if (temp.Context == Context && temp.Line == Line && Mathf.Abs(temp.Delay - Delay) < 0.00001)
                return true;
        }
        return false;
    }
}