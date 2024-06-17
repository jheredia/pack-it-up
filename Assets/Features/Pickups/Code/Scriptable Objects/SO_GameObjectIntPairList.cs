using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Data/General/GameObject Int Pair List", order = 1)]
public class SO_GameObjectIntPairList : ScriptableObject
{
    public List<SO_GameObjectIntPair> list;
}
