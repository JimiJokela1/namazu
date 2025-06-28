using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IPointerDownHandler
{
    public BuildingType TileBuilding;
    public TileType TileType;

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
}

public enum TileType
{
    Ore,
    Stone,
    Crystal,
    Blocked,
    Any
}