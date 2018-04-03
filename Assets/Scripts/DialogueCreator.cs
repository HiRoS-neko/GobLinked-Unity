using UnityEditor;
using UnityEngine;

public class DialogueCreator {
    [MenuItem("Assets/Create/Dialogue")]
    public static Dialogue  Create()
    {
        Dialogue asset = ScriptableObject.CreateInstance<Dialogue>();

        AssetDatabase.CreateAsset(asset, "Assets/Dialogue.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }
}