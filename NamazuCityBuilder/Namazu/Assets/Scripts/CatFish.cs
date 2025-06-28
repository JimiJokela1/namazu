using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CatFish : MonoBehaviour
{
    private RectTransform rectTransform;

    public float range = 5;

    private List<Tile> coveringTiles = new List<Tile>();

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void FixedUpdate()
    {
        GetTilesInRange();
        if (coveringTiles.Count >= 2)
        {
            ShuffleTiles();
        }
    }

    void ShuffleTiles()
    {
        List<Tile> tiles = coveringTiles.OrderBy(x => Random.Range(1f, 1000f)).ToList();

        for (int i = 0; i < tiles.Count - 1; i += 2)
        {
            GameManager.Instance.SwitchTiles(tiles[i].positionOnGrid, tiles[i + 1].positionOnGrid);
            tiles[i].shuffled = true;
            tiles[i + 1].shuffled = true;
        }
    }

    void GetTilesInRange()
    {
        Vector2 normalizedPos = new Vector2(transform.localPosition.x / 32, transform.localPosition.y / 32);
        for (int y = 0; y < GameManager.Instance.TileCountY; y++)
        {
            for (int x = 0; x < GameManager.Instance.TileCountX; x++)
            {
                if ((normalizedPos - new Vector2(x, y)).sqrMagnitude < range * range)
                {
                    if (!coveringTiles.Contains(GameManager.Instance.Tiles[x, y]) && !GameManager.Instance.Tiles[x, y].shuffled)
                    {
                        coveringTiles.Add(GameManager.Instance.Tiles[x, y]);
                    }
                    if (coveringTiles.Contains(GameManager.Instance.Tiles[x, y]) && GameManager.Instance.Tiles[x, y].shuffled)
                    {
                        coveringTiles.Remove(GameManager.Instance.Tiles[x, y]);
                    }
                }
                else if (coveringTiles.Contains(GameManager.Instance.Tiles[x, y]))
                {
                    coveringTiles.Remove(GameManager.Instance.Tiles[x, y]);
                    GameManager.Instance.Tiles[x, y].shuffled = false;
                }
                else
                {
                    GameManager.Instance.Tiles[x, y].shuffled = false;
                }
            }
        }
    }
}
