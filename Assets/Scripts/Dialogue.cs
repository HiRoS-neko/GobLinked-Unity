using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Dialogue : ScriptableObject
{
    public List<DialogueLine> Lines = new List<DialogueLine>();
}