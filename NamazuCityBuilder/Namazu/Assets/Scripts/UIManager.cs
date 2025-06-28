using System;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance => FindAnyObjectByType<UIManager>();

    public List<TextMeshProUGUI> ResourceTexts;
    public Transform CostsParent;
    public GameObject CostPrefab;

    public Sprite CrystalIcon;
    public Sprite OreIcon;
    public Sprite StoneIcon;
    public Sprite FoodIcon;
    public Sprite MetalIcon;
    public Sprite PowerIcon;
    public Sprite NoPowerIcon;

    public void UpdateResourceTexts(Dictionary<ResourceType, int> resourceTypes)
    {
        int ndx = 0;
        foreach (KeyValuePair<ResourceType, int> resource in resourceTypes)
        {
            ResourceTexts[ndx++].text = Enum.GetName(typeof(ResourceType), resource.Key) + ": " + resource.Value;
        }

        // Disable extra texts
        for (; ndx < ResourceTexts.Count; ndx++)
        {
            ResourceTexts[ndx].text = "";
        }
    }

    public void UpdateCostText(List<ResourceAmount> cost)
    {
        foreach (Transform child in CostsParent)
        {
            Destroy(child.gameObject);
        }

        foreach (ResourceAmount resourceAmount in cost)
        {
            GameObject costObj = Instantiate(CostPrefab, CostsParent);
            costObj.GetComponentInChildren<TextMeshProUGUI>().text = resourceAmount.Amount.ToString();
            costObj.GetComponentInChildren<Image>().sprite =
                GetResourceSprite(resourceAmount.Resource);
        }
    }

    public Sprite GetResourceSprite(ResourceType resourceAmountResource)
    {
        switch (resourceAmountResource)
        {
            case ResourceType.Crystal:
                return CrystalIcon;
            case ResourceType.Ore:
                return OreIcon;
            case ResourceType.Metal:
                return MetalIcon;
            case ResourceType.Food:
                return FoodIcon;
            case ResourceType.Stone:
                return StoneIcon;
            default:
                Debug.LogError("No resource icon set: " + Enum.GetName(typeof(ResourceType), resourceAmountResource));
                return null;
        }
    }
}
