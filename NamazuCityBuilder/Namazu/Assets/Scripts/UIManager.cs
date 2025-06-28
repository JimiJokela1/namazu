using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance => FindAnyObjectByType<UIManager>();

    public List<TextMeshProUGUI> ResourceTexts;
    public TextMeshProUGUI CostText;

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
        string costText = "Cost: ";

        foreach (ResourceAmount resource in cost)
        {
            costText += resource.Amount + " " + Enum.GetName(typeof(ResourceType), resource.Resource);
        }

        CostText.text = costText;
    }
}
