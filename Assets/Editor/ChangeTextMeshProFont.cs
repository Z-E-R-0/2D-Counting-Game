using UnityEditor;
using UnityEngine;
using TMPro;

public class ChangeTextMeshProFont : EditorWindow
{
    public TMP_FontAsset newFont;

    [MenuItem("Tools/Change TextMeshPro Font")]
    public static void ShowWindow()
    {
        GetWindow<ChangeTextMeshProFont>("Change TMP Font");
    }

    [System.Obsolete]
    private void OnGUI()
    {
        GUILayout.Label("Change TextMeshPro Font", EditorStyles.boldLabel);

        newFont = (TMP_FontAsset)EditorGUILayout.ObjectField("New Font", newFont, typeof(TMP_FontAsset), false);

        if (GUILayout.Button("Apply Font to All TMP Texts"))
        {
            if (newFont == null)
            {
                EditorUtility.DisplayDialog("Error", "Please assign a font before applying.", "OK");
                return;
            }

            ApplyFontToAllTMPTexts();
        }
    }

    [System.Obsolete]
    private void ApplyFontToAllTMPTexts()
    {
        TextMeshProUGUI[] texts = FindObjectsOfType<TextMeshProUGUI>(true); // Include inactive objects
        int count = 0;

        foreach (var tmp in texts)
        {
            Undo.RecordObject(tmp, "Change TMP Font");
            tmp.font = newFont;
            EditorUtility.SetDirty(tmp);
            count++;
        }

        Debug.Log($"Successfully updated {count} TextMeshPro components.");
        EditorUtility.DisplayDialog("Success", $"Updated {count} TextMeshPro components.", "OK");
    }
}
