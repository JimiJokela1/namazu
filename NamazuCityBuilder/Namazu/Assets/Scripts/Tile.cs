using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IPointerDownHandler
{
    public BuildingType TileBuilding;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (TileBuilding != null)
            return;

        if (GameManager.Instance.SelectedBuildingType != null)
        {
            if (GameManager.Instance.CanAffordResourceCost(GameManager.Instance.SelectedBuildingType.ResourceCosts()))
            {
                GameManager.Instance.RemoveResources(GameManager.Instance.SelectedBuildingType.ResourceCosts());
                TileBuilding = GameManager.Instance.SelectedBuildingType;
                GameObject building = Instantiate(TileBuilding.BuildingPrefab, GameManager.Instance.BuildingsParent);

                building.transform.position = transform.position;
                building.transform.position += Vector3.up;
            }
        }
    }
}
