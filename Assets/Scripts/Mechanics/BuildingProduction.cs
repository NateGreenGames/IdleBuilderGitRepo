using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingProduction : MonoBehaviour
{
    public SObuiding whatBuildingAmI;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GenerationRoutine());
    }

    public IEnumerator GenerationRoutine()
    {
        GameManager.gm.canvasMan.UpdateCoinCount(whatBuildingAmI.buildingGenerationPerSecond);
        yield return new WaitForSeconds(1);
        StartCoroutine(GenerationRoutine());
    }
}
