using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using DevKit.Core.Extensions;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

[CustomEditor(typeof(TypeSelector), true)]
public class TypePropertyDrawer : Editor
{
    // TODO ensure var values persist
    private string[] _assemblyNames;

    private Assembly[] _assemblies;

    private string _selectedAssemblyName;

    private Assembly _selectedAssembly;

    private int _selectedAssemblyIndex;

    protected override void OnHeaderGUI()
    {
        Debug.LogWarning("base.OnHeaderGUI();");
        base.OnHeaderGUI();
    }

    public override void OnInspectorGUI()
    {
        Debug.LogWarning("base.OnInspectorGUI();");

        EditorGUILayout.Foldout(true, "Type Mapping", true);
        EditorGUILayout.BeginVertical();

        if (_assemblyNames.IsNullOrEmpty())
        {
            _assemblies = AppDomain.CurrentDomain.GetAssemblies();
            _assemblyNames = new string[_assemblies.Length];
            for (var i = 0; i < _assemblies.Length; i++)
            {
                _assemblyNames[i] = _assemblies[i].GetName().Name;
            }
            if (!_assemblies.IsNullOrEmpty())
            {
                Array.Sort(_assemblyNames);
            }
        }

        if (!_assemblyNames.IsNullOrEmpty())
        {
            _selectedAssemblyIndex = EditorGUILayout.Popup("Assembly ", _selectedAssemblyIndex, _assemblyNames);
            if(_selectedAssemblyIndex >= 0 && _selectedAssemblyIndex < _assemblyNames.Length)
            {
                _selectedAssembly = _assemblies[_selectedAssemblyIndex];
                _selectedAssemblyName = _assemblyNames[_selectedAssemblyIndex];
            }
        }

        if (_selectedAssembly != null)
        {
            var types = _selectedAssembly.GetTypes();
            var typeNames = new string[types.Length];
            for (var i = 0; i < types.Length; i++)
            {
                typeNames[i] = types[i].FullName;
            }
            if (!typeNames.IsNullOrEmpty())
            {
                Array.Sort(typeNames);
            }

            EditorGUILayout.Popup("Type ", 0, typeNames);
        }

        EditorGUILayout.EndVertical();

        base.OnInspectorGUI();
    }


}
