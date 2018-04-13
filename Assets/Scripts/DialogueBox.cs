using TMPro;
using UnityEngine;

public class DialogueBox : MonoBehaviour
{
    public static TextMeshProUGUI TextBox;

    [SerializeField] private TextMeshProUGUI _text;

    private void Start()
    {
        TextBox = _text;
    }
}