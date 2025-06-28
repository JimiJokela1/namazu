using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading;
using System.Drawing;

public class CatFish : MonoBehaviour
{
    private RectTransform rectTransform;

    public float range = 5;
    public float speed = 0.05f;

    private float timer = 0;
    public Spline spline;

    private List<Tile> coveringTiles = new List<Tile>();

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        Vector2 offset = new Vector2(GameManager.Instance.TileCountX, GameManager.Instance.TileCountY) * 32 / 2;
        spline = new Spline();
        List<Vector2> points = new List<Vector2>() { GetRandomDirection() * 700 + offset, GetRandomDirection() * 200 + offset, GetRandomDirection() * 700 + offset, };
        spline.SetPoints(points, new List<Vector2>() { GetRandomDirection() * 150, GetRandomDirection() * 150, GetRandomDirection() * 150, });
        foreach (Vector2 point in points)
        {
            print(point);
        }
    }

    void FixedUpdate()
    {
        Movement();
        GetTilesInRange();
        if (coveringTiles.Count >= 2)
        {
            ShuffleTiles();
        }
    }

    void Movement()
    {
        timer += speed * Time.fixedDeltaTime;
        transform.localPosition = spline.EvaluatePosition(timer);
        transform.localRotation = Quaternion.Euler(0, 0, spline.EvaluateAngle(timer));
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

    Vector2 GetRandomDirection()
    {
        float a = Random.Range(-Mathf.PI, Mathf.PI);
        return new Vector2(Mathf.Sin(a), Mathf.Cos(a));
    }
}




