using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    public GameObject creditsWidget;
    public GameObject buildingButtonWidget;
    public GameObject activeCanvas;
    public AnimationClip fadeOut;
    public Animator m_anim;

    public int startingCoinCount;
    public int currentCoinCount;

    private GameObject buildingLayoutGroup;
    private TextMeshProUGUI coinCountText;
    private GameObject activeCreditsWidget;
    private GameManager gm;

    public void Init()
    {
        if (!gm) gm = GameManager.gm;
        activeCanvas = GameObject.Find("Canvas");
        m_anim = activeCanvas.GetComponent<Animator>();
        if(gm.activeSceneIndex != 0)
        {
            m_anim.SetTrigger("fadeIn");
            coinCountText = GameObject.FindGameObjectWithTag("Coin Counter").GetComponent<TextMeshProUGUI>();
            currentCoinCount = startingCoinCount;
            UpdateCoinCount(0);
            buildingLayoutGroup = GameObject.Find("BuildingLayoutGroup");
            CreateBuildingButtons();
        }

    }

    public void ShowCreditsWidget()
    {
        activeCreditsWidget = Instantiate(creditsWidget, activeCanvas.transform);
    }

    public void CloseCreditsWidget()
    {
        Destroy(activeCreditsWidget);
    }

    public void PlayClickEffect()
    {
        gm.audioMan.primarySource.PlayOneShot(gm.gameObject.GetComponent<BuildingPlacement>().placementSFX, 0.3f);
    }

    public void UpdateCoinCount(int _newCoinAmount)
    {
        coinCountText.text = $"{currentCoinCount + _newCoinAmount}";
        currentCoinCount += _newCoinAmount;
    }


    private void CreateBuildingButtons()
    {
        foreach (SObuiding building in GameManager.gm.buildingList)
        {
            Instantiate(buildingButtonWidget, buildingLayoutGroup.transform).GetComponent<BuyBuildingWidget>().UpdateButton(building);
        }
    }
}
