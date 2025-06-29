using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance = null;

    public List<TextMeshProUGUI> ResourceTexts;
    public Transform ResourceTextsParent;
    public Transform CostsParent;
    public GameObject CostPrefab;
    public TextMeshProUGUI PopulationText;
    public TextMeshProUGUI StarvingText;
    public TextMeshProUGUI DescriptionText;

    public Sprite CrystalIcon;
    public Sprite OreIcon;
    public Sprite StoneIcon;
    public Sprite FoodIcon;
    public Sprite MetalIcon;
    public Sprite PowerIcon;
    public Sprite NoPowerIcon;

    void Awake()
    {
        Instance = FindAnyObjectByType<UIManager>();
    }

    public void UpdateResourceTexts(Dictionary<ResourceType, int> resourceTypes)
    {
        foreach (Transform child in ResourceTextsParent)
        {
            Destroy(child.gameObject);
        }

        foreach (var resource in resourceTypes)
        {
            GameObject resourceObj = Instantiate(CostPrefab, ResourceTextsParent);
            resourceObj.GetComponentInChildren<TextMeshProUGUI>().text = Enum.GetName(typeof(ResourceType), resource.Key) + ": " + resource.Value.ToString();
            resourceObj.GetComponentInChildren<Image>().sprite = GetResourceSprite(resource.Key);
        }
    }

    public void UpdatePopulationText()
    {
        PopulationText.text = "Population: " + GameManager.Instance.TotalPopulation + " / 200";
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

    public void UpdateStarvingText(bool starving)
    {
        if (starving)
        {
            StarvingText.text = "People are starving!!!";
        }
        else
        {
            StarvingText.text = "";
        }
    }

    public void UpdateDescriptionText(string text)
    {
        DescriptionText.text = "Description: " + text;
    }
}
