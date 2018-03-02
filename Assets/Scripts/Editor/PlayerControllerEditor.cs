using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(PlayerController))]
public class PlayerControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        PlayerController playerController = (PlayerController) target;
        if (GUILayout.Button("Switch Goblins"))
        {
            playerController.SwitchPlayers();
        }
    }
}