using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Blank Building", menuName = "New Building Object")]
public class SObuiding : ScriptableObject
{
    public string buildingName;
    public int buildingCost;
    public int buildingGenerationPerSecond;
    public Sprite buildingSprite;

    public GameObject previewPrefab;
    public GameObject buildingPrefab;
    public GameObject cantPlacePrefab;

    public Vector3 placementOffset;
    public Vector3 modelVolume;
}
