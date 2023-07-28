using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingProduction : MonoBehaviour
{
    public SObuiding whatBuildingAmI;

    private void OnEnable()
    {
        GameTickManager.OnTick += Generate;
    }

    private void OnDisable()
    {
        GameTickManager.OnTick -= Generate;
    }

    private void Generate()
    {
        GameManager.gm.canvasMan.UpdateCoinCount(whatBuildingAmI.buildingGenerationPerSecond);
    }
}
