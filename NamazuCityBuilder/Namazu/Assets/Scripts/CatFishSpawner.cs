using UnityEngine;

public class CatFishSpawner : MonoBehaviour
{
    public GameObject CatfishPrefab;

    void Start()
    {
        InvokeRepeating("SpawnCatfish", 30f, 30f);
    }

    void SpawnCatfish()
    {
        GameObject gameObj = Instantiate(CatfishPrefab, transform);
    }
}
