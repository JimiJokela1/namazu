using UnityEngine;

public class Building : MonoBehaviour
{
    public BuildingType BuildingType;
    public Tile Tile;

    public void SetNoPowerIcon(bool iconOn)
    {
        gameObject.transform.Find("NoPowerIcon").gameObject.SetActive(iconOn);
    }
}
