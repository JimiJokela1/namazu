using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance => FindAnyObjectByType<GameManager>();

    public BuildingType SelectedBuildingType;
    public Transform BuildingsParent;
    public List<BuildingType> BuildingTypes;

    public void SelectBuildingType1()
    {
        SelectedBuildingType = BuildingTypes[0];
    }

}
