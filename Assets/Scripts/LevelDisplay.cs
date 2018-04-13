using TMPro;
using UnityEngine;

public class LevelDisplay : MonoBehaviour
{
    private TextMeshProUGUI _textMesh;

    private void Start()
    {
        _textMesh = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        _textMesh.text = "Current Level : " + Goblin.Level + "\n" +
                         "Current Experience : " + Goblin.Exp;
    }
}