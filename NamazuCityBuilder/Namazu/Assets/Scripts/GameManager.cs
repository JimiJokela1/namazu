using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    public BuildingType SelectedBuildingType;
    public Transform BuildingsParent;
    public List<BuildingType> BuildingTypes;
    public List<GameObject> TilePrefabs;
    public Transform TilesParent;
    public Tile[,] Tiles;
    public int TileCountY;
    public int TileCountX;

    public GameObject OreTilePrefab;
    public GameObject CrystalTilePrefab;
    public GameObject StoneTilePrefab;
    public GameObject NormalTilePrefab;

    public GameObject SceneryPrefab;
    public Transform SceneryParent;
    public Sprite MountainSprite;
    public Sprite ForestSprite;
    public Sprite HillSprite;
    public Sprite Hill2Sprite;
    public Sprite BambooSprite;
    public Sprite Bamboo2Sprite;

    public float OreChancePercent = 20;
    public float CrystalChancePercent = 20;
    public float StoneChancePercent = 20;

    public float MountainChance = 5;
    public float HillChance = 5;
    public float Hill2Chance = 5;
    public float ForestChance = 5;
    public float BambooChance = 5;
    public float Bamboo2Chance = 5;

    private int _totalPopulation = 0;
    public int TotalPopulation
    {
        get
        {
            return _totalPopulation;
        }
        set
        {
            _totalPopulation = value;
            UIManager.Instance.UpdatePopulationText();
        }
    }

    public List<Building> BuiltBuildings = new List<Building>();

    public int InitialOre = 0;
    public int InitialMetal = 0;
    public int InitialStone = 0;
    public int InitialCrystal = 0;
    public int InitialFood = 0;

    public Dictionary<ResourceType, int> PlayerResources = new Dictionary<ResourceType, int>();

    void Awake()
    {
        Instance = FindAnyObjectByType<GameManager>();
    }

    void Start()
    {
        PlayerResources.Add(ResourceType.Ore, InitialOre);
        PlayerResources.Add(ResourceType.Metal, InitialMetal);
        PlayerResources.Add(ResourceType.Stone, InitialStone);
        PlayerResources.Add(ResourceType.Crystal, InitialCrystal);
        PlayerResources.Add(ResourceType.Food, InitialFood);

        UIManager.Instance.UpdateResourceTexts(PlayerResources);

        GenerateTiles();

        StartCoroutine(TickLogic());
    }

    public void GenerateTiles()
    {
        // Generate background tiles
        for (int y = 0; y < TileCountY; y++)
        {
            for (int x = 0; x < TileCountX; x++)
            {
                GameObject tileGo = Instantiate(NormalTilePrefab, TilesParent);
                tileGo.transform.localPosition = new Vector3(x * 32, y * 32, -1);
            }
        }

        Tiles = new Tile[TileCountX, TileCountY];
        for (int y = 0; y < TileCountY; y++)
        {
            for (int x = 0; x < TileCountX; x++)
            {
                GameObject tileGo = null;
                float totalChance = OreChancePercent + CrystalChancePercent + StoneChancePercent;
                float choice = Random.Range(0f, 100f);
                if (choice < totalChance && choice >= totalChance - OreChancePercent)
                {
                    tileGo = Instantiate(OreTilePrefab, TilesParent);
                    tileGo.transform.localPosition = new Vector3(x * 32, y * 32, 0);
                    Tiles[x, y] = tileGo.GetComponent<Tile>();
                    Tiles[x, y].positionOnGrid = new Vector2Int(x, y);
                    continue;
                }

                totalChance -= OreChancePercent;
                if (choice < totalChance && choice >= totalChance - CrystalChancePercent)
                {
                    tileGo = Instantiate(CrystalTilePrefab, TilesParent);
                    tileGo.transform.localPosition = new Vector3(x * 32, y * 32, 0);
                    Tiles[x, y] = tileGo.GetComponent<Tile>();
                    Tiles[x, y].positionOnGrid = new Vector2Int(x, y);
                    continue;
                }

                totalChance -= CrystalChancePercent;
                if (choice < totalChance)
                {
                    tileGo = Instantiate(StoneTilePrefab, TilesParent);
                    tileGo.transform.localPosition = new Vector3(x * 32, y * 32, 0);
                    Tiles[x, y] = tileGo.GetComponent<Tile>();
                    Tiles[x, y].positionOnGrid = new Vector2Int(x, y);
                    continue;
                }

                tileGo = Instantiate(NormalTilePrefab, TilesParent);
                tileGo.transform.localPosition = new Vector3(x * 32, y * 32, 0);
                Tiles[x, y] = tileGo.GetComponent<Tile>();
                Tiles[x, y].positionOnGrid = new Vector2Int(x, y);
            }
        }

        for (int y = 0; y < TileCountY; y++)
        {
            for (int x = 0; x < TileCountX; x++)
            {
                if (Random.Range(0f, 100f) < MountainChance)
                {
                    GameObject scenObj = Instantiate(SceneryPrefab, SceneryParent);
                    scenObj.GetComponentInChildren<Image>().sprite = MountainSprite;
                    scenObj.GetComponentInChildren<Image>().rectTransform.sizeDelta = new Vector2(64, 64);
                    scenObj.transform.localPosition = new Vector3(x * 32, y * 32, 1);
                    Tiles[x, y].TileType = TileType.Blocked;
                    if (Tiles.GetLength(0) > x + 1)
                        Tiles[x + 1, y].TileType = TileType.Blocked;
                    if (Tiles.GetLength(1) > y + 1)
                        Tiles[x, y + 1].TileType = TileType.Blocked;
                    if (Tiles.GetLength(0) > x + 1 && Tiles.GetLength(1) > y + 1)
                        Tiles[x + 1, y + 1].TileType = TileType.Blocked;
                }
                else if (Random.Range(0f, 100f) < HillChance)
                {
                    GameObject scenObj = Instantiate(SceneryPrefab, SceneryParent);
                    scenObj.GetComponentInChildren<Image>().sprite = HillSprite;
                    scenObj.transform.localPosition = new Vector3(x * 32, y * 32, 1);
                    Tiles[x, y].TileType = TileType.Blocked;
                }
                else if (Random.Range(0f, 100f) < Hill2Chance)
                {
                    GameObject scenObj = Instantiate(SceneryPrefab, SceneryParent);
                    scenObj.GetComponentInChildren<Image>().sprite = Hill2Sprite;
                    scenObj.transform.localPosition = new Vector3(x * 32, y * 32, 1);
                    Tiles[x, y].TileType = TileType.Blocked;
                }
                else if (Random.Range(0f, 100f) < ForestChance)
                {
                    GameObject scenObj = Instantiate(SceneryPrefab, SceneryParent);
                    scenObj.GetComponentInChildren<Image>().sprite = ForestSprite;
                    scenObj.transform.localPosition = new Vector3(x * 32, y * 32, 1);
                    Tiles[x, y].TileType = TileType.Blocked;
                }
                else if (Random.Range(0f, 100f) < BambooChance)
                {
                    GameObject scenObj = Instantiate(SceneryPrefab, SceneryParent);
                    scenObj.GetComponentInChildren<Image>().sprite = BambooSprite;
                    scenObj.transform.localPosition = new Vector3(x * 32, y * 32, 1);
                    Tiles[x, y].TileType = TileType.Blocked;
                }
                else if (Random.Range(0f, 100f) < Bamboo2Chance)
                {
                    GameObject scenObj = Instantiate(SceneryPrefab, SceneryParent);
                    scenObj.GetComponentInChildren<Image>().sprite = Bamboo2Sprite;
                    scenObj.transform.localPosition = new Vector3(x * 32, y * 32, 1);
                    Tiles[x, y].TileType = TileType.Blocked;
                }
            }
        }
    }

    public void SwitchTiles(Vector2Int a, Vector2Int b)
    {
        Tile temp = Tiles[a.x, a.y];
        Tiles[a.x, a.y] = Tiles[b.x, b.y];
        Tiles[b.x, b.y] = temp;

        Tiles[a.x, a.y].transform.localPosition = new Vector3(a.x * 32, a.y * 32, 0);
        Tiles[a.x, a.y].positionOnGrid = new Vector2Int(a.x, a.y);
        Tiles[a.x, a.y].RepositionBuilding();

        Tiles[b.x, b.y].transform.localPosition = new Vector3(b.x * 32, b.y * 32, 0);
        Tiles[b.x, b.y].positionOnGrid = new Vector2Int(b.x, b.y);
        Tiles[b.x, b.y].RepositionBuilding();
    }

    public IEnumerator TickLogic()
    {
        int currentTick = 0;
        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            foreach (var building in BuiltBuildings)
            {
                if (currentTick % building.BuildingType.ProductionPerTicks == 0)
                {
                    if (!CanAffordResourceCost(building.BuildingType.ResourceProductionCost))
                    {
                        if (building.BuildingType.PopulationGain > 0)
                        {
                            // People are starving!
                            UIManager.Instance.UpdateStarvingText(true);
                        }
                        continue;
                    }

                    foreach (ResourceAmount resourceAmount in building.BuildingType.ResourceProductionCost)
                    {
                        UpdateResource(resourceAmount.Resource, -resourceAmount.Amount);
                    }

                    foreach (ResourceAmount production in building.BuildingType.ResourceProduction)
                    {
                        UpdateResource(production.Resource, production.Amount);
                    }
                }
            }

            currentTick++;
        }
    }

    public void UpdateResource(ResourceType type, int change)
    {
        PlayerResources[type] += change;

        UIManager.Instance.UpdateResourceTexts(PlayerResources);
    }

    public void RemoveResources(List<ResourceAmount> resources)
    {
        foreach (var cost in resources)
        {
            UpdateResource(cost.Resource, -cost.Amount);
        }
    }

    public bool CanAffordResourceCost(List<ResourceAmount> resourceCosts)
    {
        foreach (var resourceCost in resourceCosts)
        {
            if (PlayerResources[resourceCost.Resource] < resourceCost.Amount)
                return false;
        }

        return true;
    }

    public void SelectOreMine()
    {
        SelectedBuildingType = BuildingTypes.Find(b => b.name == "OreMine");
        UIManager.Instance.UpdateCostText(SelectedBuildingType.BuildingCosts);
    }

    public void SelectStoneMine()
    {
        SelectedBuildingType = BuildingTypes.Find(b => b.name == "StoneMine");
        UIManager.Instance.UpdateCostText(SelectedBuildingType.BuildingCosts);
    }

    public void SelectCrystalMine()
    {
        SelectedBuildingType = BuildingTypes.Find(b => b.name == "CrystalMine");
        UIManager.Instance.UpdateCostText(SelectedBuildingType.BuildingCosts);
    }

    public void SelectFactory()
    {
        SelectedBuildingType = BuildingTypes.Find(b => b.name == "Factory");
        UIManager.Instance.UpdateCostText(SelectedBuildingType.BuildingCosts);
    }

    public void SelectFarm()
    {
        SelectedBuildingType = BuildingTypes.Find(b => b.name == "Farm");
        UIManager.Instance.UpdateCostText(SelectedBuildingType.BuildingCosts);
    }

    public void SelectResidential()
    {
        SelectedBuildingType = BuildingTypes.Find(b => b.name == "Residential");
        UIManager.Instance.UpdateCostText(SelectedBuildingType.BuildingCosts);
    }

    public void SelectPowerProduction()
    {
        SelectedBuildingType = BuildingTypes.Find(b => b.name == "PowerProduction");
        UIManager.Instance.UpdateCostText(SelectedBuildingType.BuildingCosts);
    }

    public void SelectPowerPylon()
    {
        SelectedBuildingType = BuildingTypes.Find(b => b.name == "PowerPylon");
        UIManager.Instance.UpdateCostText(SelectedBuildingType.BuildingCosts);
    }

    public List<Building> GetNeighbouringBuildings(Tile tile)
    {
        List<Building> buildings = new List<Building>();

        for (int y = tile.positionOnGrid.y - 1; y < tile.positionOnGrid.y + 1; y++)
        {
            for (int x = tile.positionOnGrid.x - 1; y < tile.positionOnGrid.x + 1; x++)
            {
                if (Tiles[x, y].TileBuilding != null)
                {
                    buildings.Add(Tiles[x,y].currentBuildingObject);
                }
            }
        }

        return buildings;
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
