using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerController))]
public class PlayerControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var playerController = (PlayerController) target;
        if (GUILayout.Button("Switch Goblins")) playerController.SwitchPlayers();
    }
}