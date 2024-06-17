using System.Collections.Generic;
using UnityEngine;

public interface IObjectTracker
{
    public List<GameObject> MyObjectList { get; }

    void AddObjectToList(GameObject NewObject);
    List<GameObject> GetObjectList();
}
