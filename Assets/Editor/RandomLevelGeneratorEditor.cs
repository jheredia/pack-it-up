using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AbstractLevelGenerator),true)]
public class RandomLevelGeneratorEditor : Editor
{
    AbstractLevelGenerator generator;

    private void Awake()
    {
        generator = (AbstractLevelGenerator)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Create Level"))
        {
            generator.GenerateLevel();
        }
    }
}
