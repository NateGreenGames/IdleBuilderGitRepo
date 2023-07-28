using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacement : MonoBehaviour
{
    public AudioClip placementSFX;
    public AudioClip cantPlaceSFX;
    public float sfxVolume = 0.3f;

    public GameObject placementPS;
    public GameObject objectOnCursor;
    public SObuiding currentlyChosenBuilding;
    public LayerMask buildingPlacementMask;

    private bool isPreviewOnTopOfGround;
    private GameManager gm;


    private void Start()
    {
        gm = GameManager.gm;
        currentlyChosenBuilding = GameManager.gm.buildingList[0];
    }
    private void Update()
    {
        PreviewBuilding(); //Update location and model of the building preview every frame. Note: Can we change this to only update on building change/mouse cursor location change?

        if (Input.GetMouseButtonDown(0)) //Checks if the left mouse button was pressed this frame. If so, do the folllowing.
        {
            PlaceBuilding(); 
        }

        if (Input.GetMouseButtonDown(1)) //Checks if the right mouse button was pressed this frame. If so, do the folllowing.
        {
            DemolishBuilding();
        }
    }






    private void DemolishBuilding()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //Create a ray going from the location of the cursor going into the scene.
        if (Physics.Raycast(ray, out hit, Mathf.Infinity)) //Check to see if the ray hit anything.
        {
            if(hit.collider.tag == "Building") //If the ray hit an object that is tagged as a building, do the following...
            {
                //Get a reference to the clicked building's generation script so we can access it's volume settings.
                BuildingProduction buildingReference = hit.collider.gameObject.GetComponent<BuildingProduction>();


                //Spawn our placement particle effects where the object was and scale it up based on the models volume.
                GameObject placementEffect = Instantiate(placementPS, hit.collider.gameObject.transform.position, Quaternion.Euler(90, 0, 0));
                placementEffect.transform.localScale = buildingReference.whatBuildingAmI.modelVolume * 2;

                
                gm.audioMan.primarySource.PlayOneShot(placementSFX, sfxVolume); //Play the placement sound effects.
                gm.cameraDirector.ScreenShake(0.1f); //Add screen shake.
                Destroy(hit.collider.gameObject); //Destroy the clicked object.
            }
        }
    }
    private void PlaceBuilding()
    {
            if (currentlyChosenBuilding.buildingCost > gm.canvasMan.currentCoinCount && CheckIfPositionIsValid() && isPreviewOnTopOfGround || !CheckIfPositionIsValid() && isPreviewOnTopOfGround)
            {
                gm.audioMan.primarySource.PlayOneShot(cantPlaceSFX, sfxVolume);
            }
            if (objectOnCursor && CheckIfPositionIsValid() && currentlyChosenBuilding.buildingCost <= GameManager.gm.canvasMan.currentCoinCount)
            {
                Vector3 pointToPlace = GetCursorPositionInWorld();

                Destroy(objectOnCursor);
                objectOnCursor = null;

                GameObject newBuilding = Instantiate(currentlyChosenBuilding.buildingPrefab, pointToPlace + currentlyChosenBuilding.placementOffset, currentlyChosenBuilding.buildingPrefab.transform.rotation);
                newBuilding.AddComponent<BuildingProduction>().whatBuildingAmI = currentlyChosenBuilding;

                GameObject placementEffect = Instantiate(placementPS, pointToPlace, Quaternion.Euler(90, 0, 0));
                placementEffect.transform.localScale = currentlyChosenBuilding.modelVolume * 2;
                gm.audioMan.primarySource.PlayOneShot(placementSFX, sfxVolume);
                gm.cameraDirector.ScreenShake(0.1f);
                gm.canvasMan.UpdateCoinCount(-currentlyChosenBuilding.buildingCost);
            }
    }

    private void PreviewBuilding()
    {
        Vector3 pointToPlace = GetCursorPositionInWorld();
        if (currentlyChosenBuilding != null)
        {
            if (CheckIfPositionIsValid() && currentlyChosenBuilding.buildingCost <= gm.canvasMan.currentCoinCount)
            {
                if (objectOnCursor != currentlyChosenBuilding.previewPrefab)
                {
                    Destroy(objectOnCursor);
                    objectOnCursor = Instantiate(currentlyChosenBuilding.previewPrefab, pointToPlace, currentlyChosenBuilding.previewPrefab.transform.rotation);
                }
                else
                {
                        objectOnCursor.transform.position = pointToPlace + currentlyChosenBuilding.placementOffset;
                }
            }
            else
            {
                if (objectOnCursor != currentlyChosenBuilding.cantPlacePrefab)
                {
                    Destroy(objectOnCursor);
                    objectOnCursor = Instantiate(currentlyChosenBuilding.cantPlacePrefab, pointToPlace, currentlyChosenBuilding.cantPlacePrefab.transform.rotation);
                }
                else
                {
                        objectOnCursor.transform.position = pointToPlace + currentlyChosenBuilding.placementOffset;
                }
            }
        }
    }



    private bool CheckIfPositionIsValid()
    {
        Vector3 pointToPlace = GetCursorPositionInWorld();

        bool isValid = true;

        if (isPreviewOnTopOfGround)
        {
            Collider[] overlappingCollides = Physics.OverlapBox(pointToPlace + currentlyChosenBuilding.placementOffset, currentlyChosenBuilding.modelVolume);
            foreach (Collider hitCollider in overlappingCollides)
            {
                if (hitCollider.tag != "Placeable Ground" && hitCollider.gameObject != objectOnCursor)
                {
                    isValid = false;
                }
            }
            return isValid;

        }
        else
        {
            return false;
        }
    }


    private Vector3 GetCursorPositionInWorld()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, buildingPlacementMask) && hit.normal == hit.collider.gameObject.transform.up)
        {
            isPreviewOnTopOfGround = true;
            return hit.point;
        }
        else
        {
            isPreviewOnTopOfGround = false;
            return new Vector3(1000, -1000, 1000);
        }
    }
}
