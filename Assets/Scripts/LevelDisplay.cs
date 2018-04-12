using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelDisplay : MonoBehaviour
{
    private TextMeshProUGUI _textMesh;

    private void Start()
    {
        _textMesh = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        _textMesh.text = "Current Level : " + Goblin.Level.ToString() + "\n" +
                         "Current Experience : " + Goblin.Exp.ToString();
    }
}