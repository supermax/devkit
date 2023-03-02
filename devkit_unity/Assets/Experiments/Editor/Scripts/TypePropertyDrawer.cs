using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

[CustomPropertyDrawer(typeof(TypeSelector), true)]
public class TypePropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        // Begin a horizontal group
        //EditorGUILayout.BeginHorizontal();

        // Draw the object field
        var objectType = fieldInfo.FieldType;

        var obj = fieldInfo.GetValue(property.serializedObject.targetObject);
        Debug.LogWarningFormat("obj: {0}", obj);

        var value = obj as Object;

        var newObj = EditorGUI.ObjectField(position, "Type", value, typeof(AssemblyDefinitionAsset), false);

        // var ass = AppDomain.CurrentDomain.GetAssemblies()[0];
        //
        // var types = ass.ExportedTypes.ToArray();
        // var content = new string[types.Length];
        //
        // for (int i = 0; i < types.Length; i++)
        // {
        //     content[i] = types[i].FullName;
        // }
        //
        // Array.Sort(content);
        //
        // EditorGUI.Popup(position, "Type: ", 0, content);

        // Update the value if it has changed
        if (!Equals(newObj, obj))
        {
            fieldInfo.SetValue(property.serializedObject.targetObject, newObj);
        }

        // End the horizontal group
        //EditorGUILayout.EndHorizontal();

        EditorGUI.EndProperty();
    }
}
