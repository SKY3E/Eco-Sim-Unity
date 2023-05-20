using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class TagManager : UnityEditor.Editor
{
    // On start call AddTag functions & apply properties
    static TagManager()
    {
        SerializedObject tagManager = new SerializedObject(
            AssetDatabase.LoadMainAssetAtPath("ProjectSettings/TagManager.asset")
        );

        SerializedProperty tagsProp = tagManager.FindProperty("tags");

        AddTag(tagsProp, "Plant");
        AddTag(tagsProp, "Herbivore");

        tagManager.ApplyModifiedProperties();
    }
    // Add tag if it doesn't exist
    private static void AddTag(SerializedProperty tagsProp, string newTag)
    {
        bool found = false;

        for (int i = 0; i < tagsProp.arraySize; i++)
        {
            SerializedProperty t = tagsProp.GetArrayElementAtIndex(i);
            if (t.stringValue.Equals(newTag))
            {
                found = true;
                break;
            }
        }

        if (!found)
        {
            tagsProp.InsertArrayElementAtIndex(0);
            SerializedProperty newTagProp = tagsProp.GetArrayElementAtIndex(0);
            newTagProp.stringValue = newTag;
        }
    }
}
