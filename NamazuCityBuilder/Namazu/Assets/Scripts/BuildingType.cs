using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingType", menuName = "Building Type")]
public class BuildingType : ScriptableObject
{
    public TileType RequiredTileType;
    public GameObject BuildingPrefab;
    public List<ResourceAmount> BuildingCosts;
    public List<ResourceAmount> ResourceProduction;
    public List<ResourceAmount> ResourceProductionCost;
    public int ProductionPerTicks = 10; // How many ticks must pass before production happens

    public int PopulationGain = 0;
    public int PowerProduction = 0;
    public int PowerRange = 0;
    public bool PowerNeed = false;

    public string Description;

    public Sprite BuildingSprite;
}

[System.Serializable]
public class ResourceAmount
{
    public ResourceType Resource;
    public int Amount;
}
