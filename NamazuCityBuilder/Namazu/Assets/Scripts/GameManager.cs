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

    void Start()
    {
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

    public void SelectBuildingType1()
    {
        SelectedBuildingType = BuildingTypes[0];
    }

}
