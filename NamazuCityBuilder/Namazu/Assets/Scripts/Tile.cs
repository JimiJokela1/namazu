using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IPointerDownHandler
{
    public Building currentBuildingObject = null;
    public BuildingType TileBuilding;
    public TileType TileType;

    public Vector2Int positionOnGrid;
    public bool shuffled = false; //Used for the catfish

    public void OnPointerDown(PointerEventData eventData)
    {
        if (TileBuilding != null)
            return;

        if (GameManager.Instance.SelectedBuildingType != null)
        {
            if (GameManager.Instance.CanAffordResourceCost(GameManager.Instance.SelectedBuildingType.BuildingCosts)
                && TileType != TileType.Blocked)
            {
                if (GameManager.Instance.SelectedBuildingType.RequiredTileType != TileType
                    && GameManager.Instance.SelectedBuildingType.RequiredTileType != TileType.Any)
                {
                    Debug.Log("Can't build on that tile.");
                    return;
                }
                GameManager.Instance.RemoveResources(GameManager.Instance.SelectedBuildingType.BuildingCosts);
                TileBuilding = GameManager.Instance.SelectedBuildingType;
                GameObject building = Instantiate(TileBuilding.BuildingPrefab, GameManager.Instance.BuildingsParent);
                building.GetComponent<Building>().BuildingType = TileBuilding;
                GameManager.Instance.BuiltBuildings.Add(building.GetComponent<Building>());
                building.transform.position = transform.position;
                building.transform.position += Vector3.up;
            }
            else
            {
                Debug.Log("Can't afford building");
            }
        }
    }

    public void RepositionBuilding()
    {
        if (currentBuildingObject != null)
        {
            currentBuildingObject.transform.position = transform.position;
            currentBuildingObject.transform.position += Vector3.up;
        }
    }
}

public enum TileType
{
    Ore,
    Stone,
    Crystal,
    Blocked,
    Any
}