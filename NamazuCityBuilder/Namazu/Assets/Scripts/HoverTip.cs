using UnityEngine;
using UnityEngine.EventSystems;

public class HoverTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string tip;

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
    }

    void Update()
    {
        if (hovering && !showing)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                HoverTipManager.ShowTip(tip);
                showing = true;
            }
        }
    }
}
