using System.Collections.Generic;
using UnityEngine;

public class ObjectTracker : MonoBehaviour, IObjectTracker
{
    public List<GameObject> MyObjectList { get; } = new List<GameObject>();

    public void AddObjectToList(GameObject NewObject)
    {
        if (!MyObjectList.Contains(NewObject))
        {
            MyObjectList.Add(NewObject);
        }
    }

    public void RemoveObjectFromList(GameObject obj)
    {
        if (MyObjectList.Contains(obj)) MyObjectList.Remove(obj);
    }

    public List<GameObject> GetObjectList() { return MyObjectList; }
}
