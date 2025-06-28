using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingType", menuName = "Building Type")]
public class BuildingType : ScriptableObject
{
    public GameObject BuildingPrefab;
    public List<BuildingCost> BuildingCosts;

    public Dictionary<ResourceType, int> ResourceCosts()
    {
        Dictionary<ResourceType, int> resourceTypes = new Dictionary<ResourceType, int>();
        foreach (BuildingCost buildingCost in BuildingCosts)
        {
            resourceTypes.Add(buildingCost.Resource, buildingCost.Cost);
        }
        return resourceTypes;
    }
}

[System.Serializable]
public class BuildingCost
{
    public ResourceType Resource;
    public int Cost;
}
