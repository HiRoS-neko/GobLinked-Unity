using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Dialogue))]
public class DialogueEditor : Editor
{
    private Dialogue _dialogueLine;


    public override void OnInspectorGUI()
    {
        _dialogueLine = (Dialogue) target;


        if (_dialogueLine.Lines.Count != 0)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Context");
            EditorGUILayout.LabelField("Dialogue Line");
            EditorGUILayout.LabelField("Delay");
            EditorGUILayout.EndHorizontal();
        }

        foreach (var line in _dialogueLine.Lines)
        {
            if (line == null)
            {
                Debug.LogError("Dialogue line null during display");
                break;
            }

            EditorGUILayout.BeginHorizontal();
            line.Context =
                EditorGUILayout.TextField(line.Context, GUILayout.MaxWidth(100), GUILayout.ExpandWidth(true));
            line.Line = EditorGUILayout.TextField(line.Line, GUILayout.ExpandWidth(true));
            line.Delay =
                EditorGUILayout.FloatField(line.Delay, GUILayout.MaxWidth(30), GUILayout.ExpandWidth(false));

            if (GUILayout.Button("-", GUILayout.ExpandWidth(false))) DeleteLine(line);

            EditorGUILayout.EndHorizontal();
        }

        if (GUILayout.Button("Add Line")) _dialogueLine.Lines.Add(new DialogueLine());

        if (GUILayout.Button("Save Dialogue"))
        {
            EditorUtility.SetDirty(_dialogueLine);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }

    private void DeleteLine(DialogueLine line)
    {
        _dialogueLine.Lines.Remove(line);
    }
}