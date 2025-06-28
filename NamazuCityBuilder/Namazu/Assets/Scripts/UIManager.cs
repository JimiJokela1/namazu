using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance => FindAnyObjectByType<UIManager>();

    public List<TextMeshProUGUI> ResourceTexts;

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

}
