using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class DialogueBox : MonoBehaviour
{
    public static TextMeshProUGUI TextBox;

    private void Start()
    {
        TextBox = GetComponent<TextMeshProUGUI>();
    }
}