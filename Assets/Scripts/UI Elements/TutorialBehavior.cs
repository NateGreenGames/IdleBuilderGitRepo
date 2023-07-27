using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialBehavior : MonoBehaviour
{

    public string[] tutorialStrings;

    public GameObject destroyOnCompletion;
    private TextMeshProUGUI uiReference;
    private int currentIndex;
    // Start is called before the first frame update
    void Start()
    {
        uiReference = gameObject.GetComponent<TextMeshProUGUI>();
        uiReference.text = tutorialStrings[0];
    }

    public void ProgressToNextStep()
    {
        GameManager.gm.audioMan.primarySource.PlayOneShot(GameManager.gm.buildingPlacer.placementSFX, GameManager.gm.buildingPlacer.sfxVolume);
        currentIndex++;
        if(currentIndex < tutorialStrings.Length)
        {
            uiReference.text = tutorialStrings[currentIndex];
        }
        else
        {
            Destroy(destroyOnCompletion);
        }
    }
}
