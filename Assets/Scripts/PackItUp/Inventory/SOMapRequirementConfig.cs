using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PackItUp.Inventory
{
    [CreateAssetMenu(fileName = "MapRequirementConfig", menuName = "ScriptableObjects/MapRequirement", order = 0)]
    public class SOMapRequirementConfig: ScriptableObject
    {
        public List<MapRequirement> requirementList = new();

        public MapRequirement GetRequirement(int mapId)
        {
            return requirementList.FirstOrDefault(x => x.mapId == mapId);
        }
    }

    [Serializable]
    public class MapRequirement
    {
        public int mapId;
        public List<ItemData> requiredItemList = new();
    }
}