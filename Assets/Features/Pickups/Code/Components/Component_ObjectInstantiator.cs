using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Component_ObjectInstantiator : MonoBehaviour
{
    public SO_GameObjectIntPairList list;

    public GameObject ParentObject;
    public Transform spawnPoint;

    public GameObjectUnityEvent onObjectInstantiatedObjectEvent;

    public void BeginInstantiation()
    {
        foreach (SO_GameObjectIntPair pair in list.list) ProcessSO_Pair(pair);
    }

    private void ProcessSO_Pair(SO_GameObjectIntPair pair)
    {
        for (int i = 0; i < pair.pair.number; i++) InstantiateObjectAtPoint(pair.pair.prefab);
    }

    private void InstantiateObjectAtPoint(GameObject Prefab)
    {
        GameObject NewObject = Instantiate(Prefab, spawnPoint.position, Quaternion.identity);
        NewObject.transform.SetParent(ParentObject.transform);
        ObjectInstantiationOverride(NewObject);
        if (NewObject != null) { onObjectInstantiatedObjectEvent.Invoke(NewObject); }
    }

    public virtual void ObjectInstantiationOverride(GameObject NewObject) { }
}
