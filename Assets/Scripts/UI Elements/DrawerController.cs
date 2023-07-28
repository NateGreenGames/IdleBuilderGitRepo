using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerController : MonoBehaviour
{

    [SerializeField] GameObject drawerParent;
    [SerializeField] Vector2 startingPositiong, endingPosition;
    [SerializeField] AnimationCurve animationCurve;

    private RectTransform m_RT;
    private float endTime;
    private bool isOpen = false;

    private void Start()
    {
        FindFurthestKeyFrame();
        m_RT = gameObject.GetComponent<RectTransform>();
        startingPositiong = m_RT.anchoredPosition;
    }

    public void ToggleDrawer()
    {
        GameManager.gm.audioMan.primarySource.PlayOneShot(GameManager.gm.buildingPlacer.placementSFX, GameManager.gm.buildingPlacer.sfxVolume);
        if (isOpen)
        {
            StartCoroutine(CloseDrawerRoutine());
        }
        else
        {
            StartCoroutine(OpenDrawerRoutine());
        }
    }
    private IEnumerator OpenDrawerRoutine()
    {
        float elapsedTime = 0;
        while (elapsedTime < endTime)
        {
            m_RT.anchoredPosition = Vector3.Lerp(startingPositiong, endingPosition, animationCurve.Evaluate(elapsedTime));
            yield return new WaitForEndOfFrame();
            elapsedTime += Time.deltaTime;
        }
        isOpen = true;
    }

    private IEnumerator CloseDrawerRoutine()
    {
        float elapsedTime = 0;
        while (elapsedTime < endTime)
        {
            m_RT.anchoredPosition = Vector3.Lerp(endingPosition, startingPositiong, animationCurve.Evaluate(elapsedTime));
            yield return new WaitForEndOfFrame();
            elapsedTime += Time.deltaTime;
        }
        isOpen = false;
    }


    private void FindFurthestKeyFrame()
    {
        float maxTime = Mathf.NegativeInfinity;
        foreach (Keyframe key in animationCurve.keys)
        {
            if(key.time > maxTime)
            {
                maxTime = key.time;
            }
        }
        endTime = maxTime;
    }
}
