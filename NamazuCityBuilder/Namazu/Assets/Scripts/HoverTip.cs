using UnityEngine;
using UnityEngine.EventSystems;

public class HoverTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string tip;

    private Tile tile;

    private float timer = 1;
    bool hovering = false;
    bool showing = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovering = false;
        HoverTipManager.HideTip();
        timer = 1;
        showing = false;
    }

    void Start()
    {
        Building building = GetComponent<Building>();
        if (building)
        {
            tip = building.BuildingType.Description;
        }
        tile = GetComponent<Tile>();
    }

    void Update()
    {
        if (hovering && !showing && (tile == null || tile.currentBuildingObject != null))
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                if (tile != null)
                {
                    tip = tile.currentBuildingObject.BuildingType.Description;
                }
                HoverTipManager.ShowTip(tip);
                showing = true;
            }
        }
    }
}
