using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tile : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Building currentBuildingObject = null;
    public BuildingType TileBuilding;
    public TileType TileType;

    public GameObject Grid;

    public Vector2Int positionOnGrid;
    public bool shuffled = false; //Used for the catfish

    public void OnPointerEnter(PointerEventData eventData)
    {
        Grid.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Grid.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (TileBuilding != null)
        {
            //print("aksfhnoainfoasncfosa");
            if (GameManager.Instance.SelectedBuildingType.Deconstruct)
            {
                //print("aksfhnoainfoasncfosa");
                DeconstructBuilding();
            }
            return;
        }



        if (GameManager.Instance.SelectedBuildingType != null && !GameManager.Instance.SelectedBuildingType.Deconstruct)
        {

            if (GameManager.Instance.CanAffordResourceCost(GameManager.Instance.SelectedBuildingType.BuildingCosts)
                && TileType != TileType.Blocked)
            {
                if (GameManager.Instance.SelectedBuildingType.RequiredTileType != TileType
                    && GameManager.Instance.SelectedBuildingType.RequiredTileType != TileType.Any)
                {
                    MessageBox.Instance.ShowMessage("Can't build that on that tile!");
                    Debug.Log("Can't build on that tile.");
                    return;
                }

                BuildBuilding();
            }
            else
            {
                MessageBox.Instance.ShowMessage("Can't afford building!");
                Debug.Log("Can't afford building");
            }
        }
    }

    public void BuildBuilding()
    {
        GameManager.Instance.RemoveResources(GameManager.Instance.SelectedBuildingType.BuildingCosts);
        TileBuilding = GameManager.Instance.SelectedBuildingType;
        GameObject building = Instantiate(TileBuilding.BuildingPrefab, GameManager.Instance.BuildingsParent);
        building.GetComponent<Building>().BuildingType = TileBuilding;
        GameManager.Instance.BuiltBuildings.Add(building.GetComponent<Building>());
        building.transform.position = transform.position;
        building.transform.position += Vector3.up;

        currentBuildingObject = building.GetComponent<Building>();
        building.GetComponentInChildren<Image>().sprite = TileBuilding.BuildingSprite;
        currentBuildingObject.Tile = this;

        AudioSource.PlayClipAtPoint(GameManager.Instance.BuildAudio, Vector3.zero, 0.3f);

        GameManager.Instance.TotalPopulation += TileBuilding.PopulationGain;
    }

    public void DestroyBuilding()
    {
        GameManager.Instance.BuiltBuildings.Remove(currentBuildingObject);
        Destroy(currentBuildingObject.gameObject);
        currentBuildingObject = null;

        GameManager.Instance.TotalPopulation -= TileBuilding.PopulationGain;

        AudioSource.PlayClipAtPoint(GameManager.Instance.DestroyAudio, Vector3.zero, 0.3f);
    }

    public void DeconstructBuilding()
    {
        foreach (ResourceAmount resource in TileBuilding.BuildingCosts)
        {
            GameManager.Instance.UpdateResource(resource.Resource, resource.Amount);
        }

        DestroyBuilding();
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