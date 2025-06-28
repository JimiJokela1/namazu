using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance => FindAnyObjectByType<GameManager>();

    public BuildingType SelectedBuildingType;
    public Transform BuildingsParent;
    public List<BuildingType> BuildingTypes;
    public List<GameObject> TilePrefabs;
    public Transform TilesParent;
    public Tile[,] Tiles;
    public int TileCountY;
    public int TileCountX;

    public int InitialOre = 0;
    public int InitialMetal = 0;
    public int InitialStone = 100;
    public int InitialCrystal = 0;
    public int InitialFood = 0;

    public Dictionary<ResourceType, int> PlayerResources = new Dictionary<ResourceType, int>();

    void Start()
    {
        PlayerResources.Add(ResourceType.Ore, InitialOre);
        PlayerResources.Add(ResourceType.Metal, InitialMetal);
        PlayerResources.Add(ResourceType.Stone, InitialStone);
        PlayerResources.Add(ResourceType.Crystal, InitialCrystal);
        PlayerResources.Add(ResourceType.Food, InitialFood);

        UIManager.Instance.UpdateResourceTexts(PlayerResources);

        GenerateTiles();
    }

    public void GenerateTiles()
    {
        Tiles = new Tile[TileCountX, TileCountY];
        for (int y = 0; y < TileCountY; y++)
        {
            for (int x = 0; x < TileCountX; x++)
            {
                GameObject tileGo = Instantiate(TilePrefabs[Random.Range(0, TilePrefabs.Count)], TilesParent);
                tileGo.transform.localPosition = new Vector3(x * 32, y * 32, 0);
                Tiles[x, y] = tileGo.GetComponent<Tile>();
            }
        }
    }

    public void UpdateResource(ResourceType type, int change)
    {
        PlayerResources[type] += change;

        UIManager.Instance.UpdateResourceTexts(PlayerResources);
    }

    public void RemoveResources(Dictionary<ResourceType, int> costs)
    {
        foreach (var cost in costs)
        {
            UpdateResource(cost.Key, -cost.Value);
        }
    }

    public bool CanAffordResourceCost(Dictionary<ResourceType, int> resourceCosts)
    {
        foreach (var resourceCost in resourceCosts)
        {
            if (PlayerResources[resourceCost.Key] < resourceCost.Value)
                return false;
        }

        return true;
    }

    public void SelectBuildingType1()
    {
        SelectedBuildingType = BuildingTypes[0];
    }

}

public enum ResourceType
{
    Ore,
    Metal,
    Stone,
    Crystal,
    Food
}
