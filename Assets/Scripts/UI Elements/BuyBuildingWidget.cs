using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BuyBuildingWidget : MonoBehaviour
{
    public Image buildingSprite;
    public TextMeshProUGUI buildingName;
    public TextMeshProUGUI buildingCost;
    public TextMeshProUGUI buildingGeneration;
    SObuiding associatedSO;


    public void UpdateButton(SObuiding _soBuilding)
    {
        associatedSO = _soBuilding;
        buildingName.text = associatedSO.buildingName;
        buildingCost.text = $"Cost: {associatedSO.buildingCost}";
        buildingGeneration.text = $"{associatedSO.buildingGenerationPerSecond}/sec";
        buildingSprite.sprite = associatedSO.buildingSprite;
    }

    public void SetChosenBuilding()
    {
        GameManager.gm.buildingPlacer.currentlyChosenBuilding = associatedSO;
    }
}
