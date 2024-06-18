using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using PackItUp.MapGenerator;
using UnityEngine;

[CustomEditor(typeof(AbstractMapGenerator),true)]
public class RandomMapGeneratorEditor : Editor
{
    AbstractMapGenerator generator;

    private void Awake()
    {
        generator = (AbstractMapGenerator)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Create Map"))
        {
            generator.GenerateMap();
        }
    }
}
